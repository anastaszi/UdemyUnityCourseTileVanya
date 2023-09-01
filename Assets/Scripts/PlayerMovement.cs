using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D playerRB;
    Animator playerAnimator;
    CapsuleCollider2D playerCollider;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    float gravityScaleAtStart;
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        gravityScaleAtStart = playerRB.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run(); 
        FlipSprite();
        ClimbLadder();  
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) {
        bool isTouchingGround = playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (!isTouchingGround) {
            return;
        }
        if (value.isPressed) {
            playerRB.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run() {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerRB.velocity.y);
       playerRB.velocity = playerVelocity;
       if (Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon) {
           playerAnimator.SetBool("isRunning", true);
       } else {
           playerAnimator.SetBool("isRunning", false);
       }
    }

    void ClimbLadder() {
        bool isTouchingLadder = playerCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
        if (!isTouchingLadder) {
            playerAnimator.SetBool("isClimbing", false);
            playerRB.gravityScale = gravityScaleAtStart;
            return;
        } 
        if (isTouchingLadder) {
            playerRB.gravityScale = 0f;
        }
        Vector2 climbVelocity = new Vector2(playerRB.velocity.x, moveInput.y * climbSpeed);
        playerRB.velocity = climbVelocity;
        bool playerHasVerticalSpeed = Mathf.Abs(playerRB.velocity.y) > Mathf.Epsilon;
        if (playerHasVerticalSpeed) {
            playerAnimator.SetBool("isClimbing", true);
        } else {
            playerAnimator.SetBool("isClimbing", false);
        }
    }


    void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(playerRB.velocity.x), 1f);
        }
    }
}
