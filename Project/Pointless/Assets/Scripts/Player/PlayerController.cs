using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpForce = 5000f;
    public float moveSpeed = 30f;

    public InputManager inputManager;
    public MiniGameManager miniGameManager;

    private Rigidbody2D playerRigidbody;
    private bool jump = false;
    private bool moveLeft = false;
    private bool moveRight = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        jump = jump || Input.GetKeyDown(KeyCode.Space);
        moveRight = Input.GetKey(KeyCode.D);
        moveLeft = Input.GetKey(KeyCode.A);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalSpeed = moveRight ? moveSpeed : 0f;
        horizontalSpeed -= (moveLeft ? moveSpeed : 0f);

        if(jump)
        {
            playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jump = false;
        }

        // Move the character by finding the target velocity
        playerRigidbody.velocity = new Vector3(horizontalSpeed, playerRigidbody.velocity.y, 0f);
    }
}
