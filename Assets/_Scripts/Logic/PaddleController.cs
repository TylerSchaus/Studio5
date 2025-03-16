using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minBallBounceBackSpeed;
    [SerializeField] private float maxBallBounceBackSpeed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputHandler.Instance.OnMove.AddListener(MovePaddle);
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnMove.RemoveListener(MovePaddle);
    }

    private void MovePaddle(Vector3 moveDirection)
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
