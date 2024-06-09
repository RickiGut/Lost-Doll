using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //Movement
    [Header("Movement")]
    public Rigidbody2D playerRb;
    public float moveSpeed = 5f;
    float horizMov;
    public SpriteRenderer spriteRenderer;

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


    [Header("Fall Detector")]
    //Detection
    private Vector3 respawnPoint;
    public GameObject fallDetector;

    //Health
    private bool isHurt = false;

    private Transform oriParent;

    [Header("Reset Position Object")]
    //Reset Object
    public GameObject[] resetPosObject;
   

   //Collect
   int score = 0;
   public TextMeshProUGUI scoreText;
   private CollectSave[] allCollect;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        respawnPoint = transform.position;
        oriParent = transform.parent;

        //Posisi Player diambil dari playerprefs
        float PosX = PlayerPrefs.GetFloat("PlayerPosX",transform.position.x);
        float PosY = PlayerPrefs.GetFloat("PlayerPosY",transform.position.y);
        float PosZ = PlayerPrefs.GetFloat("PlayerPosZ",transform.position.z);
        transform.position = new Vector3(PosX,PosY,PosZ);

        //Ambil Health dari Playerprefs
        HealthManager.health = PlayerPrefs.GetInt("playerHealth",HealthManager.health);

        //Score
        score = PlayerPrefs.GetInt("intScore");
        scoreText.text = score.ToString();

        //Collectables
        allCollect = FindObjectsOfType<CollectSave>();
        foreach(CollectSave collectSoul in allCollect){
            if(PlayerManager.isGameOver){
                collectSoul.Reset();
                print("Ini berjalan brow");
            }else{
                collectSoul.LoadCollect();
            }
            
        }

    }   

    // Update is called once per frame
    void Update()
    {
        if(!isDashing){
            PlayerVelocity();
        }
        Animations();
        Detection();
    }

    void PlayerVelocity(){
        playerRb.velocity = new Vector2(horizMov * moveSpeed,playerRb.velocity.y);

    }
    public void Move(InputAction.CallbackContext context){
        if(Time.timeScale != 0){
            horizMov = context.ReadValue<Vector2>().x;
            if(horizMov > 0){
                spriteRenderer.flipX = false;
            }else if(horizMov < 0){
                spriteRenderer.flipX = true;
            }
        }
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


    //Detection
    void Detection(){
        fallDetector.transform.position = new Vector2(transform.position.x,fallDetector.transform.position.y);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Detection" && !isHurt){
           HealthManager.health--;
           transform.position = respawnPoint;
           ResetPosObj();
           if(HealthManager.health <= 0){
            PlayerManager.isGameOver = true;
            gameObject.SetActive(false);
            DeletePlayerPrefs();
            foreach(CollectSave collectSoul in allCollect){
                collectSoul.Reset();
            }
           }else{
            StartCoroutine(GetHurt());
           }
        }else if(other.tag == "Checkpoint"){
            respawnPoint = transform.position;
            CollectPlayerPrefs();   
        }else if(other.tag == "Collect"){
            CollectSave saveCollect = other.GetComponent<CollectSave>();
            if(saveCollect != null){
                saveCollect.Take();
                score +=1;
                scoreText.text = score.ToString();
                other.gameObject.SetActive(false);
       
            }

        }
    }


    IEnumerator GetHurt(){
        isHurt = true;
        Physics2D.IgnoreLayerCollision(7,8);
        yield return new WaitForSeconds(1);

        Physics2D.IgnoreLayerCollision(7,8,false);
        isHurt =false;
    }


    void ResetPosObj(){
        foreach(GameObject obj in resetPosObject){
            if(obj !=null){
            obj.GetComponent<ResetPosObj>().ResetPositionObject();

            }
        }
    }

    void CollectPlayerPrefs(){
            //PlayerPrefs posisi Player
            PlayerPrefs.SetFloat("PlayerPosX",transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosY",transform.position.y);
            PlayerPrefs.SetFloat("PlayerPosZ",transform.position.z);
            print("Position sudah tersave");

            //Playerprefs Health player
            PlayerPrefs.SetInt("playerHealth",HealthManager.health);

            //Score
            PlayerPrefs.SetInt("intScore",score);
    
            //Saved Scene
            string currentSceneName = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("SavedScene",currentSceneName);
            PlayerPrefs.Save();
            Debug.Log("Chekpoint " + currentSceneName + "Telah disimpan");
    }

    void DeletePlayerPrefs(){
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.DeleteKey("PlayerPosZ");
        PlayerPrefs.DeleteKey("playerHealth");
        PlayerPrefs.DeleteKey("intScore");
   
    }

}
