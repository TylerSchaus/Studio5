using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class LivesCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private Transform livesTextContainer;
    [SerializeField] private GameObject gameOverDisplay; 
    [SerializeField] private float duration;

    private float containerInitPosition;
    private float moveAmount; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Canvas.ForceUpdateCanvases();
        current.SetText("3"); 
        toUpdate.SetText("0");
        containerInitPosition = livesTextContainer.localPosition.y;
        Debug.Log("Set containerInitPosition to: " + containerInitPosition);
        moveAmount = current.rectTransform.rect.height;
        Debug.Log("Set moveAmount to: " + moveAmount);
  

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int lives)
    {
        toUpdate.SetText($"{lives}");
        livesTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration);

        StartCoroutine(ResetLivesContainer(lives));
    }

    private IEnumerator ResetLivesContainer(int lives)
    {
        yield return new WaitForSeconds(duration);

        current.SetText($"{lives}");
        Vector3 localPosition = livesTextContainer.localPosition;
        livesTextContainer.localPosition = new Vector3(localPosition.x, containerInitPosition, localPosition.z);
    }

    public void ShowGameOver()
    {
        gameOverDisplay.SetActive(true); 
    }

    public void HideGameOver()
    {
        gameOverDisplay.SetActive(false);
    }
}
