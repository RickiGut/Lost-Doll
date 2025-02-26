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
   

    //Audio 
    private AudioManager audioManager;


   //Collect
   int score = 0;
   public TextMeshProUGUI scoreText;

   private CollectSave[] allCollect;
    //Hide
    private bool isInBush = false;
    private Collider2D colliderPlayer;
    private bool isHiding = false;

    [SerializeField] GameObject VolumeHiding;
    [SerializeField] GameObject CanvasHiding;
    [SerializeField] ParticleSystem WalkPar;
    [SerializeField] ParticleSystem JumpPar;

    private bool wasGrounded = true; // Untuk menyimpan status sebelumnya




    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        respawnPoint = transform.position;
        oriParent = transform.parent;
        VolumeHiding.SetActive(false);
        CanvasHiding.SetActive(false);
      

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
        //Debug.Log("Jumlah Collect " + allCollect);
        foreach(CollectSave collectSoul in allCollect){
            if(PlayerManager.isGameOver){
                collectSoul.Reset();
                print("Ini berjalan brow");
            }else{
                collectSoul.LoadCollect();
            }
            
        }

        //Audio
        audioManager = FindObjectOfType<AudioManager>();

        //
        colliderPlayer = GetComponent<Collider2D>();

        //
        PlayerPrefs.GetInt("NPCSpirit_Met");


    }   

    // Update is called once per frame
    void Update()
    {
        //line 129 - 137 yg lama
        if (!isDashing)
        {
            PlayerVelocity();
        }

        Animations();
        Detection();
        Hide();

        // **Pastikan Particle System berhenti saat di udara & aktif kembali saat menyentuh tanah**
        if (!isGrounded()) // Jika karakter di udara
        {
            if (WalkPar.isPlaying)
            {
                WalkPar.Stop(); // Hentikan particle saat melompat atau jatuh
            }
            wasGrounded = false; // Tandai bahwa karakter sebelumnya di udara
        }
        else // Jika karakter kembali menyentuh tanah
        {
            if (!wasGrounded) // Pastikan ini adalah transisi dari udara ke tanah
            {
                if (Mathf.Abs(horizMov) > 0.1f) // Hanya play jika karakter bergerak
                {
                    if (!WalkPar.isPlaying)
                    {
                        WalkPar.Play(); // Aktifkan particle kembali jika karakter berjalan
                    }
                }
                wasGrounded = true; // Tandai bahwa karakter sudah di tanah
            }
        }
    }

    void Hide(){
        if(Input.GetKey(KeyCode.S) && isInBush == true){
            playerRb.simulated = false;
            spriteRenderer.enabled = false;
            colliderPlayer.enabled = false;
            isHiding = true;
            IsHiding();
            VolumeHiding.SetActive(true);
            CanvasHiding.SetActive(true);
        }
        else if(!Input.GetKey(KeyCode.S)){
            playerRb.simulated = true;
            spriteRenderer.enabled = true;
            colliderPlayer.enabled = true;
            isHiding = false;
            VolumeHiding.SetActive(false);
            CanvasHiding.SetActive(false);
        }
    }

    public bool IsHiding(){
        Debug.Log("Hiding sekarang : " + isHiding);
        return isHiding;
    }

    void PlayerVelocity(){
        playerRb.velocity = new Vector2(horizMov * moveSpeed,playerRb.velocity.y);

    }
    public void Move(InputAction.CallbackContext context)
    /*{
        if(Time.timeScale != 0){
            audioManager.PlaySFX(audioManager.playerRun);
            horizMov = context.ReadValue<Vector2>().x;
            //WalkPar.Play();
            if (horizMov > 0){
                spriteRenderer.flipX = false;
            }
            else if(horizMov < 0){
                spriteRenderer.flipX = true;
            }
        }
    }*/
    {
        if (Time.timeScale != 0)
        {
            audioManager.PlaySFX(audioManager.playerRun);
            horizMov = context.ReadValue<Vector2>().x;

            // Jika karakter berjalan, aktifkan particle
            if (horizMov != 0)
            {
                if (!WalkPar.isPlaying)
                {
                    WalkPar.Play();
                }

                // **Atur posisi Particle System sesuai arah gerakan**
                Vector3 particleOffsetKanan = new Vector3(-0.3f, -0.4f, 0); // Jarak partikel dari karakter
                Vector3 particleOffsetKiri = new Vector3(0.1f, -0.4f, 0); // Jarak partikel dari karakter
                if (horizMov < 0) // Jika berjalan ke kiri
                {
                    WalkPar.transform.localPosition = particleOffsetKiri;
                }
                else // Jika berjalan ke kanan
                {
                    WalkPar.transform.localPosition = particleOffsetKanan;
                }
            }
            else
            {
                if (WalkPar.isPlaying)
                {
                    WalkPar.Stop();
                }
            }

            // Mengatur arah karakter berdasarkan input
            if (horizMov > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (horizMov < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    /*{
        if(isGrounded() && !isDashing){
            if(context.performed){
                audioManager.PlaySFX(audioManager.playerJump);
                WalkPar.Stop();
                playerRb.velocity = new Vector2(playerRb.velocity.x,jumpPower);
            }else if(context.canceled){
                playerRb.velocity = new Vector2(playerRb.velocity.x,playerRb.velocity.y * 0.5f);
            }
        }
    }*/
    {
        if (isGrounded() && !isDashing)
        {
            if (context.performed)
            {
                audioManager.PlaySFX(audioManager.playerJump);
                JumpPar.Play();
                WalkPar.Stop(); // Hentikan particle saat melompat
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
            }
            else if (context.canceled)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y * 0.5f);
                JumpPar.Stop();
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
        }else if(other.tag == "Bush"){
            isInBush = true;
            print("Is Inn Bush : " + isInBush);
        }
    }

    
    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Bush"){
            isInBush = false;
        }
    }


    IEnumerator GetHurt(){
        isHurt = true;
        Physics2D.IgnoreLayerCollision(7,8);
        yield return new WaitForSeconds(0);

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

    public void ResetScore(){
        score = 0;
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("intScore",score);
        //Debug.Log("score telah di reset ke 0");
    }

    void CollectPlayerPrefs(){
            //PlayerPrefs posisi Player
            PlayerPrefs.SetFloat("PlayerPosX",transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosY",transform.position.y);
            PlayerPrefs.SetFloat("PlayerPosZ",transform.position.z);
           // print("Position sudah tersave");

            //Playerprefs Health player
            PlayerPrefs.SetInt("playerHealth",HealthManager.health);

            //Score
            PlayerPrefs.SetInt("intScore",score);
    
            //Saved Scene
            string currentSceneName = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("SavedScene",currentSceneName);
            PlayerPrefs.Save();
            //Debug.Log("Chekpoint " + currentSceneName + "Telah disimpan");
    }

    void DeletePlayerPrefs(){
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.DeleteKey("PlayerPosZ");
        PlayerPrefs.DeleteKey("playerHealth");
        PlayerPrefs.DeleteKey("intScore");
   
    }

}
