using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    //Movement
    [Header("Movement")]
    public Rigidbody2D playerRb;
    public float moveSpeed = 5f;
    float horizMov;
    SpriteRenderer spriteRenderer;

    //Jump
    [Header("Jump")]
    public float jumpPower = 10f;

    [Header("GroundCheck")]
    public Transform posCheckGround;
    public Vector2 groundCheckSize = new Vector2(0.5f,0.05f);
    public LayerMask groundLayer;


    //Dash
    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 2f;
    private float dashDirection;
    private bool isDashing = false;

    //Animasi
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDashing){
            PlayerVelocity();
        }
        Animations();
    }

    void PlayerVelocity(){
        playerRb.velocity = new Vector2(horizMov * moveSpeed,playerRb.velocity.y);

    }
    public void Move(InputAction.CallbackContext context){
        if(horizMov > 0){
            spriteRenderer.flipX = false;
        }else if(horizMov < 0){
            spriteRenderer.flipX = true;
        }
        horizMov = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context){
        if(isGrounded()){
            if(context.performed){
                playerRb.velocity = new Vector2(playerRb.velocity.x,jumpPower);
            }else if(context.canceled){
                playerRb.velocity = new Vector2(playerRb.velocity.x,playerRb.velocity.y * 0.5f);
            }
        }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(posCheckGround.position,groundCheckSize);
    }

    private bool isGrounded(){
        return Physics2D.OverlapBox(posCheckGround.position,groundCheckSize,0,groundLayer)!= null;
    }

    public void Dash(InputAction.CallbackContext context){
        if(context.performed && isGrounded() && !isDashing && horizMov != 0){
          dashDirection = spriteRenderer.flipX ? -1:1;  
            StartCoroutine(PerformDash());
        } 
    }

    private IEnumerator PerformDash(){
        isDashing = true;
        animator.SetBool("Dash",true);
        float oriGravity = playerRb.gravityScale;
        playerRb.gravityScale = 0;
        playerRb.velocity = new Vector2(dashDirection * dashSpeed,0);
        yield return new WaitForSeconds(dashDuration);
        playerRb.gravityScale = oriGravity;
        isDashing = false;
        animator.SetBool("Dash",false);
    }


    //Animasi
    void Animations(){
        animator.SetFloat("Moving",Mathf.Abs(horizMov));
        animator.SetBool("Jump",isGrounded());
    }




}
