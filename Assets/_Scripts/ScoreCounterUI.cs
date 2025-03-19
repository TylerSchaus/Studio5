using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI toUpdateScoreText;
    [SerializeField] private Transform scoreTextContainer;
    [SerializeField] private float duration;

    private float containerInitPosition;
    private float moveAmount;

    void Start()
    {
        Canvas.ForceUpdateCanvases();
        currentScoreText.SetText("0");
        toUpdateScoreText.SetText("0");
        containerInitPosition = scoreTextContainer.localPosition.y;
        moveAmount = currentScoreText.rectTransform.rect.height;
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

