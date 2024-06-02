using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    [SerializeField] float moveSpeed;
    [SerializeField] float climbSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] GameObject Arrow;
    [SerializeField] Transform ArrowSpawnPoint;

    [SerializeField] Vector2 deathJump = new Vector2 (10f, 10f);

    public bool CanMove=true;

    private float currentGravity;

    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;


    void Start()
    {
        myRigidbody=GetComponent<Rigidbody2D>();
        myAnimator=GetComponent<Animator>();
        myBodyCollider=GetComponent<CapsuleCollider2D>();
        myFeetCollider=GetComponent<BoxCollider2D>();
        currentGravity=myRigidbody.gravityScale;
    }

    void Update()
    {
        if (!CanMove){return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!CanMove){return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump (InputValue value)
    {
        if (!CanMove){return;}
        if (value.isPressed && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void ClimbLadder()
    {
        if (!CanMove){return;}
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            
            Vector2 playerVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y*climbSpeed);
            myRigidbody.velocity = playerVelocity;
            myRigidbody.gravityScale=0;
        }else
        {
            myRigidbody.gravityScale=currentGravity;
        }
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))&&playerHasVerticalSpeed)
        {
            myAnimator.SetBool("isClimbing", true);
        }else
        {
            myAnimator.SetBool("isClimbing", false);
        }


    }


     void Run()
     {
 if (CanMove)
 {
           Vector2 playerVelocity = new Vector2(moveInput.x*moveSpeed, myRigidbody.velocity.y);
           myRigidbody.velocity = playerVelocity;
 }
        // if (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon)
        // {
        //     myAnimator.SetBool("isRunning", true);
        // } else
        // {
        //     myAnimator.SetBool("isRunning", false);
        // }
        
     }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    if (playerHasHorizontalSpeed)
    {
        transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x),1f);
    }
        
    }



    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            CanMove=false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity =deathJump;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }


    void OnFire(InputValue value)
    {
        Instantiate(Arrow, ArrowSpawnPoint.position, transform.rotation);
    }


}
