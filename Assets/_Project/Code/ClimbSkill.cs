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
    [SerializeField] private bool isInClimbZone = false;
    [SerializeField] private LayerMask climbablesLayer;
    private float verticalInput;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }


    public void Climb(CallbackContext cbc)
    {
        verticalInput = cbc.ReadValue<float>();
        if (cbc.canceled)
        {
            ResetVerticalSpeed();
        }

        if (CanClimb(cbc))
        {
            StartClimbing(cbc);
            rigidbody2d.AddForce(new Vector2(0, verticalInput * climbForce));
        }
    }

    private void ResetVerticalSpeed()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
    }

    private bool CanClimb(CallbackContext cbc)
    {
        if(cbc.performed && isInClimbZone)
        {
            print("can climb");
            return true;
        }

        print("CANNOT climb");

        return false;
    }

    private void StopClimbing()
    {
        print("stop climbing");
        TurnOnGravity();
        movement.EnableJump();
        climbing = false;
    }

    public void StartClimbing(CallbackContext cbc)
    {
        if (climbing)
            return;

        print("start climbing");
        ResetVerticalSpeed();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("trigger enter");
        if (LayerChecker.IsInMask(collision.gameObject, climbablesLayer))
        {
            print("In vine trigger");
            isInClimbZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("trigger exit");
        if (LayerChecker.IsInMask(collision.gameObject, climbablesLayer))
        {
            print("left vine trigger");
            isInClimbZone = false;
            StopClimbing();
        }
    }
}
