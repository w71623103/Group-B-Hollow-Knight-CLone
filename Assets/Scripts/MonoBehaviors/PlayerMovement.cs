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
        
        Down,
        Ground,
    };
    public Rigidbody2D playerRB;
    public SpriteRenderer spriteR;
    public PlayerController playerController;
    public Animator playerAN;
    public bool isLeft = true;
    public float playerMovementSpeed = 5f;
    public bool isGrounded = false;
    public float verticalMovement = 0f;
    public float horizontalMovement = 0f;
    public float jumpSpeed = 0f;
    public JumpState jState = JumpState.Default;
    public int jumpStartTimer;
    public int maxJumpKeyFrame = 30;
    public int minJumpKeyFrame = 20;
    public float jumpForceADD = 1.3f;

    private int _JTUPHash;
    private int _JTDownHash;
    private int _JHash;
    private int _WalkHash;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>(); //get a reference to the Rigidbody2D component on this player gameObject!
        spriteR = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
        playerAN = GetComponent<Animator>();

        _JTUPHash = Animator.StringToHash("JTUP");
        _JTDownHash = Animator.StringToHash("JTD");
        _JHash = Animator.StringToHash("Jumping");
        _WalkHash = Animator.StringToHash("Walking");
    }

    public void Jump()
    {
        if (playerController.inputJumpDown && isGrounded)
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
                    playerAN.SetTrigger(_JTUPHash);
                    if (jumpStartTimer <= maxJumpKeyFrame)
                    {
                        if (jumpStartTimer >= minJumpKeyFrame)
                        {
                            if (playerController.inputJump)
                            {
                                jumpSpeed += jumpForceADD;
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
                playerAN.SetTrigger(_JHash);
                //playerCD.size = new Vector2(defaultColliderSize.x * 0.8f, defaultColliderSize.y);
                if (playerRB.velocity.y <= 0)
                    jState = JumpState.Down;

                break;
            case JumpState.Down:
                playerAN.SetTrigger(_JHash);
                //playerCD.size = new Vector2(defaultColliderSize.x * 0.8f, defaultColliderSize.y);
                if (isGrounded)
                {
                    jState = JumpState.Ground;
                }
                break;

            case JumpState.Ground:
                //need animator condition
                jState = JumpState.Default;
                break;
        }

        var scale = transform.localScale;
        if (isLeft)
            transform.localScale = new Vector3(scale.x > 0 || horizontalMovement == 0 ? scale.x : -scale.x, scale.y, scale.z);
        else
            transform.localScale = new Vector3(scale.x < 0 || horizontalMovement == 0 ? scale.x : -scale.x, scale.y, scale.z);
    }

    public void Movement()
    {
        
        horizontalMovement = playerController.inputAxis.x * playerMovementSpeed;

        playerRB.velocity = new Vector2(horizontalMovement, playerRB.velocity.y);

        if (horizontalMovement < 0)
            isLeft = true;
        else if (horizontalMovement > 0)
            isLeft = false;

        if (horizontalMovement != 0)
        {
            //playerAN.SetTrigger(_WalkHash);
            playerAN.Play("Walking");
        }

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
