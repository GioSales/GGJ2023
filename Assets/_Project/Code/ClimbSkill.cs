using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ClimbSkill : MonoBehaviour
{

    [SerializeField] private float climbForce;


    private float savedGravityScale = 4f;
    private bool gravityEnabled = true;

    private Movement movement;
    private Rigidbody2D rigidbody2d => movement.GetRigidBody();

    private bool climbing = false;
    [SerializeField] private bool inClimbZone = false;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    public void Climb(CallbackContext cbc)
    {
        if (cbc.canceled)
        {
            ResetVerticalSpeed();
        }

        if (CanClimb(cbc))
        {
            StartClimbing(cbc);
            rigidbody2d.AddForce(new Vector2(0, climbForce));
        }
    }

    private void ResetVerticalSpeed()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
    }

    private bool CanClimb(CallbackContext cbc)
    {
        if(cbc.performed && inClimbZone)
        {
            return true;
        }

        return false;
    }

    private void StopClimbing()
    {
        TurnOnGravity();
        movement.EnableJump();
        climbing = false;
    }

    public void StartClimbing(CallbackContext cbc)
    {
        if (climbing)
            return;

        TurnOffGravity();
        movement.DisableJump();
        climbing = true;
    }

    private void TurnOffGravity()
    {
        if (!gravityEnabled)
            return;

        savedGravityScale = rigidbody2d.gravityScale;
        rigidbody2d.gravityScale = 0;
        gravityEnabled = false;
    }

    private void TurnOnGravity()
    {
        if (gravityEnabled)
            return;

        rigidbody2d.gravityScale = savedGravityScale;
        gravityEnabled = true;
    }
}
