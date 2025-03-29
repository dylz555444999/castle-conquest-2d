using System;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 15f;
    [SerializeField] float climbSpeed = 8f;

    Rigidbody2D myRigidBody2D;
    Animator myAnimator;
    BoxCollider2D myBoxCollider2D;
    PolygonCollider2D myPlayersFeet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myPlayersFeet = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        run();
        jump();
        climb();
    }

    private void climb()
    {
        if (myPlayersFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(myRigidBody2D.linearVelocity.x, controlThrow * climbSpeed);

            myRigidBody2D.linearVelocity = climbVelocity;

            myAnimator.SetBool("Climbing", true);
        }
        else
        {
            myAnimator.SetBool("Climbing", false);
        }
    }

    private void jump()
    {
        if (!myPlayersFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        bool isJumping = CrossPlatformInputManager.GetButtonDown("Jump");

        if (isJumping)
        {
            Vector2 jumpVelocity = new Vector2(myRigidBody2D.linearVelocity.x, jumpSpeed);
            myRigidBody2D.linearVelocity = jumpVelocity;
        }
    }

    private void run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2D.linearVelocity.y);
        myRigidBody2D.linearVelocity = playerVelocity;

        FlipSprite();
        changingToRunningState();
    }

    private void changingToRunningState()
    {
        bool runningHorizontally = Mathf.Abs(myRigidBody2D.linearVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", runningHorizontally);
    }

    private void FlipSprite()
    {
        bool runningHorizontally = Mathf.Abs(myRigidBody2D.linearVelocity.x) > Mathf.Epsilon;

        if (runningHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.linearVelocity.x), 1f);
        }
    }
}
