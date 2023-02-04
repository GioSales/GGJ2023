using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce = 800f;

    private float horizontalInput = 0f;
    private float horizontalMovement = 0f;
    private bool jumpEnabled = true;


    [Header("Refs")]
    [SerializeField] private Rigidbody2D rigidbody2d;

    void Update()
    {
        horizontalMovement = Mathf.Clamp(horizontalInput, -1, 1);

        rigidbody2d.velocity = new Vector2(horizontalMovement * speed, rigidbody2d.velocity.y);

    }

    public void MoveLeft(CallbackContext cbc)
    {
        MoveHorizontal(cbc, -1);
    }

    private void MoveHorizontal(CallbackContext cbc, float value)
    {
        if (cbc.performed)
            horizontalInput += value;

        if (ShouldCancel(cbc))
            horizontalInput -= value;
    }

    private bool ShouldCancel(CallbackContext cbc)
    {
        if (cbc.canceled)
            return true;

        return false;
    }

    public void MoveRight(CallbackContext cbc)
    {
        MoveHorizontal(cbc, 1);
    }



    public void Jump(CallbackContext cbc)
    {
        if (CanJump(cbc))
        {
            rigidbody2d.AddForce(new Vector2(0, jumpForce));
        }
    }

    private bool CanJump(CallbackContext cbc)
    {
        if (cbc.started)
            return true;

        return false;
    }

    public Rigidbody2D GetRigidBody()
    {
        return rigidbody2d;
    }

    public void DisableJump()
    {
        jumpEnabled = false;
    }

    public void EnableJump()
    {
        jumpEnabled = true;
    }
}
