using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private LivesCounterUI livesCounter; 
    [SerializeField] private ScoreCounterUI scoreCounter;

    private int currentBrickCount;
    private int totalBrickCount;
    private int score = 0;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    // Update Ball
    GameObject ballObj = GameObject.FindWithTag("Ball");
    if (ballObj != null)
    {
        ball = ballObj.GetComponent<Ball>();
        ball.ResetBall();
    }

    // Update bricks container
    GameObject bricksObj = GameObject.Find("BricksContainer");
    if (bricksObj != null)
    {
        bricksContainer = bricksObj.transform;
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }

    // Update LivesCounterUI
    GameObject livesPanelObj = GameObject.Find("LivesPanel");
    if (livesPanelObj != null)
    {
        livesCounter = livesPanelObj.GetComponent<LivesCounterUI>();
        livesCounter.UpdateLives(maxLives);
    }
    else
    {
        Debug.LogWarning("LivesPanel not found in scene: " + scene.name);
    }

    // Update ScoreCounterUI
    GameObject scorePanelObj = GameObject.Find("ScorePanel");
    if (scorePanelObj != null)
    {
        scoreCounter = scorePanelObj.GetComponent<ScoreCounterUI>();
        // Delay update to let the UI initialize
        StartCoroutine(DelayedScoreUpdate());
    }
    else
    {
        Debug.LogWarning("ScorePanel not found in scene: " + scene.name);
    }
}


    private void OnEnable()
{
    if (InputHandler.Instance != null)
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
    }
    
    if (ball != null)
    {
        ball.ResetBall();
    }
    
    if (bricksContainer != null)
    {
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }
}
private IEnumerator DelayedScoreUpdate()
{
    yield return null; // Wait one frame (or yield return new WaitForSeconds(0.1f); for a longer delay)
    scoreCounter.UpdateScore(score);
}

private void OnDisable()
{
    if (InputHandler.Instance != null)
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }
}

    private void FireBall()
    {
        ball.FireBall();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        // fire audio here
        // implement particle effect here
        // add camera shake here
        currentBrickCount--;
        score += 10;
        if (scoreCounter != null)
        {
            scoreCounter.UpdateScore(score);
        }
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        if (currentBrickCount == 0)
    {
        if (SceneHandler.Instance != null)
        {
            SceneHandler.Instance.LoadNextScene();
        }
        else
        {
            Debug.LogError("SceneHandler.Instance is null. Active scene: " + SceneManager.GetActiveScene().name);        }
    }
    }
    public int GetScore()
{
    return score;
}

    public void KillBall()
    {
        maxLives--;
        livesCounter.UpdateLives(maxLives);

        if (maxLives == 0) StartCoroutine(GameOver()); 
       
        ball.ResetBall();
    }

    private IEnumerator GameOver()
    {
        livesCounter.ShowGameOver();

        yield return new WaitForSeconds(2f);

        SceneHandler.Instance.LoadMenuScene();

    }

}
