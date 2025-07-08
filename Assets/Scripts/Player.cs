using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
[Obsolete]
public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;

    Rigidbody2D myRigidBody2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2D.velocity.y);
        myRigidBody2D.velocity = playerVelocity;
        FlipSprite();
    }
    private void FlipSprite()
    {
        bool runninghorizontally = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;

        if (runninghorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.velocity.x), 1f);
        }
    }
}