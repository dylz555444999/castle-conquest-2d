using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 15f;

    Rigidbody2D myRigidBody2D;
    Animator myAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        run();
        jump();
    }

    private void jump()
    {
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

