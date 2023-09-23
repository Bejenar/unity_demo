using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput = Vector2.zero;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        var movement = new Vector2(moveInput.x, 0) * (moveSpeed * Time.deltaTime);

        transform.Translate(movement);
        
        if (moveInput.x != 0)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
