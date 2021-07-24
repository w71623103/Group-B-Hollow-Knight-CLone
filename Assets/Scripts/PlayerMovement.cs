using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum JumpState 
    {
        Default,
        Start,
        Up,
        Trans,
        Down,
        Ground,
    };
    public Rigidbody2D playerRB;
    public SpriteRenderer spriteR;
    public bool isLeft = true;
    public float playerMovementSpeed = 5f;
    public bool isGrounded = false;
    public float verticalMovement = 0f;
    public float horizontalMovement = 0f;
    public float jumpSpeed = 0f;
    public JumpState jState = JumpState.Default;
    public int jumpStartTimer;

    

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>(); //get a reference to the Rigidbody2D component on this player gameObject!
        spriteR = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.K)) && isGrounded)
        {
            jState = JumpState.Start;
        }
        switch (jState)
        {
            case JumpState.Default:
                //playerCD.size = defaultColliderSize;
                isGrounded = true;
                jumpSpeed = 0f;
                break;
            case JumpState.Start:
                if (isGrounded)
                {
                    if (jumpStartTimer <= 30)
                    {
                        if (jumpStartTimer >= 20)
                        {
                            if (Input.GetKey(KeyCode.K))
                            {
                                jumpSpeed += 1.3f;
                            }
                        }
                        else
                        {
                            jumpSpeed = 20f;
                        }

                        jumpStartTimer++;
                    }
                    else
                    {
                        playerRB.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                        jState = JumpState.Up;
                        jumpStartTimer = 0;
                    }
                }
                else 
                {
                    jState = JumpState.Up;
                }
                break;
            case JumpState.Up:
                //playerCD.size = new Vector2(defaultColliderSize.x * 0.8f, defaultColliderSize.y);
                if (playerRB.velocity.y <= 0)
                    jState = JumpState.Down;
                break;
            case JumpState.Down:
                //playerCD.size = new Vector2(defaultColliderSize.x * 0.8f, defaultColliderSize.y);
                if (isGrounded)
                {
                    jState = JumpState.Default;
                }
                break;
        }

        if (isLeft)
            spriteR.flipX = false;
        else
            spriteR.flipX = true;
    }

    void FixedUpdate()
    {
        
        horizontalMovement = Input.GetAxis("Horizontal") * playerMovementSpeed;

        playerRB.velocity = new Vector2(horizontalMovement, playerRB.velocity.y);

        if (horizontalMovement <= 0)
            isLeft = true;
        else
            isLeft = false;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    /*
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerCD.size = new Vector2(defaultColliderSize.x * 0.8f, defaultColliderSize.y);
        }
    }*/

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

}
