using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float JumpSpeed = 5f;
    [SerializeField] float ClimbSpeed = 3f;
    bool isPlayerMoveHorizontal;
    bool isPlayerMoveVertical;
    CapsuleCollider2D myCollider2D;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Vector2 moveInput;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if(value.isPressed && myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRigidbody.velocity = new Vector2(0f, JumpSpeed);        
        }
    }    
    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;


        isPlayerMoveHorizontal = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", isPlayerMoveHorizontal);
    }
    void FlipSprite()
    {
        isPlayerMoveHorizontal = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(isPlayerMoveHorizontal)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
    void ClimbLadder()
    {
        if(!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            myRigidbody.gravityScale = 8f;
            myAnimator.SetBool("IsClimbing", false);
            return;
        }
       
        Vector2 climbingVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * ClimbSpeed);
        myRigidbody.velocity = climbingVelocity;
        myRigidbody.gravityScale = 0f;

        isPlayerMoveVertical = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", isPlayerMoveVertical);
    }
}
