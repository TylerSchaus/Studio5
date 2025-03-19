using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private LivesCounterUI livesCounter; 

    private int currentBrickCount;
    private int totalBrickCount;

    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
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
