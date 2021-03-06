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
    public PlayerAudio playerAudio;
    public Animator playerAN;
    public bool isLeft = true;
    public float playerMovementSpeed = 5f;
    public bool isGrounded = false;
    public float verticalMovement = 0f;
    public float horizontalMovement = 0f;
    public float jumpSpeed = 0f;
    public JumpState jState = JumpState.Default;
    public float jumpStartTimer;
    public int maxJumpKeyFrame = 30;
    public int minJumpKeyFrame = 20;
    public float jumpForceADD = 1.3f;
    public float jumpMinForce = 10f;
    public bool isJumped = false;

    public float groundCheckRayLength = 3.8f;


    private int _JTUPHash;
    private int _JTDownHash;
    private int _JHash;
    //private int _WalkHash;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>(); //get a reference to the Rigidbody2D component on this player gameObject!
        spriteR = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
        playerAudio = GetComponent<PlayerAudio>();
        playerAN = GetComponent<Animator>();

        _JTUPHash = Animator.StringToHash("JTUP");
        _JTDownHash = Animator.StringToHash("JTD");
        _JHash = Animator.StringToHash("Jumping");
        //_WalkHash = Animator.StringToHash("Walking");
    }

    public void Jump()
    {
        playerAN.SetBool("isGroundAnim", isGrounded);
        if (playerController.inputJumpDown && isGrounded)
        {
            isJumped = false;
            jState = JumpState.Start;
            playerAN.SetTrigger(_JTUPHash);
        }
        else if ((playerRB.velocity.y < 0) && jState == JumpState.Default && !isGrounded)
        {
            jState = JumpState.Down;
        }
        switch (jState)
        {
            case JumpState.Default:
                //playerCD.size = defaultColliderSize;
                isGrounded = true;
                jumpSpeed = 0f;
                break;
            case JumpState.Start:
                if (isGrounded && !isJumped)
                {
                    playerRB.AddForce(Vector2.up * jumpMinForce, ForceMode2D.Impulse);
                    isJumped = true;
                    playerAudio.PlayAudioJump();
                }
                else
                {
                    if (jumpStartTimer <= maxJumpKeyFrame)
                    {
                        if (jumpStartTimer >= minJumpKeyFrame)
                        {
                            if (playerController.inputJump)
                            {
                                playerRB.AddForce(Vector2.up * (jumpForceADD * Time.deltaTime * 100), ForceMode2D.Impulse);
                            }
                        }
                        jumpStartTimer += Time.deltaTime * 100;
                    }
                    else
                    {
                        jState = JumpState.Up;
                    }
                }
                break;
            case JumpState.Up:
                jumpStartTimer = 0;
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
                    playerAudio.PlayAudioLand();
                }
                break;

            case JumpState.Ground:
                playerAN.SetTrigger(_JTDownHash);
                playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
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
        CheckGrounded();
        horizontalMovement = playerController.inputAxis.x * playerMovementSpeed;

        playerRB.velocity = new Vector2(horizontalMovement, playerRB.velocity.y);

        if (horizontalMovement < 0)
            isLeft = true;
        else if (horizontalMovement > 0)
            isLeft = false;

        if (horizontalMovement != 0 && isGrounded)
        {
            playerAN.SetBool("isWalking", true);
                playerAudio.PlayWalking();
        }
        else 
        {
            playerAN.SetBool("isWalking", false);
            playerAudio.StopWalking();
        }

    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumped = false;
            Vector3 hit = collision.contacts[0].normal;
            float angle = Vector3.Angle(hit, Vector3.up);
            if (Mathf.Approximately(angle, 0)) isGrounded = true;
        }
    }*/

    /*
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerCD.size = new Vector2(defaultColliderSize.x * 0.8f, defaultColliderSize.y);
        }
    }*/

    /*void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            //playerAudio.StopWalking();
        }
    }*/

    void CheckGrounded()
    {
        LayerMask mask = LayerMask.GetMask("Ground");
        bool cast = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(2f, 0.1f), 0,
            Vector2.down, groundCheckRayLength, mask);
        Debug.DrawRay(transform.position, Vector3.down * groundCheckRayLength, Color.blue);
        isGrounded = cast;
    }

}
