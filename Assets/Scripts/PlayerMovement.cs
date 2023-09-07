using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D playerRB;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider;

    bool isAlive;
    BoxCollider2D playerFeetCollider;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(0f, 10f);
    [SerializeField] Transform gun;
    [SerializeField] GameObject bullet;
    float gravityScaleAtStart;
    // Start is called before the first frame update
    void Start()
    {
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = playerRB.gravityScale;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) {
            return;
        }
        Run(); 
        FlipSprite();
        ClimbLadder();  
        Die();
    }
    


    void OnMove(InputValue value) {
        if (!isAlive) {
            return;
        }
        moveInput = value.Get<Vector2>();
    }

    void Die() {
        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard"))) {
            isAlive = false;
            playerAnimator.SetTrigger("Dying");
            playerRB.velocity = deathKick;
            // GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);
            // FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void OnFire(InputValue value) {
        if (!isAlive) {
            return;}
        Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnJump(InputValue value) {
                if (!isAlive) {
            return;
        }
        bool isTouchingGround = playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
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
        bool isTouchingLadder = playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
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
