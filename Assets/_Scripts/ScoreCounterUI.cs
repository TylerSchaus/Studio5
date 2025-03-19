using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

using DG.Tweening;

public class ScoreCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI toUpdateScoreText;
    [SerializeField] private Transform scoreTextContainer;
    [SerializeField] private float duration;

    private float containerInitPosition;
    private float moveAmount;

     private void Awake()
    {
        // Capture the initial position of the container
        containerInitPosition = scoreTextContainer.localPosition.y;
        moveAmount = currentScoreText.rectTransform.rect.height;
        // Subscribe to sceneLoaded to reinitialize after a level load.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // When a scene loads, update the displayed score from the GameManager.
        if (GameManager.Instance != null)
        {
            int currentScore = GameManager.Instance.GetScore(); // Add a getter in your GameManager
            currentScoreText.SetText(currentScore.ToString());
            toUpdateScoreText.SetText(currentScore.ToString());
        }
    }


    public void UpdateScore(int score)
    {
        toUpdateScoreText.SetText($"{score}");
        scoreTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration);

        StartCoroutine(ResetScoreContainer(score));
    }

    private IEnumerator ResetScoreContainer(int score)
    {
        yield return new WaitForSeconds(duration);
        currentScoreText.SetText($"{score}");
        Vector3 localPosition = scoreTextContainer.localPosition;
        scoreTextContainer.localPosition = new Vector3(localPosition.x, containerInitPosition, localPosition.z);
    }
}

