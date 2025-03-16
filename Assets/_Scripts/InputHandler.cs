using UnityEngine;
using UnityEngine.Events;

public class InputHandler : SingletonMonoBehavior<InputHandler>
{
    public UnityEvent<Vector3> OnMove;
    public UnityEvent OnFire;

    private void Update()
    {
        Vector3 moveVector = Vector3.zero;
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveVector += Vector3.left;
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveVector += Vector3.right;
        if(Input.GetKeyDown(KeyCode.Space)) OnFire?.Invoke();
        OnMove?.Invoke(moveVector);
    }
}
