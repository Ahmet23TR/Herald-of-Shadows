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
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Vector2 moveInput;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D >();
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
        if(value.isPressed && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidbody.gravityScale = 6f;
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