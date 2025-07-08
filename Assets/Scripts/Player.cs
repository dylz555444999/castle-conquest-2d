using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2D.linearVelocity.y);
        myRigidBody2D.linearVelocity = playerVelocity;
    }
}
