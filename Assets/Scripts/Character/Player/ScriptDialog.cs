using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScriptDialog : MonoBehaviour
{   
    [SerializeField]
    private GameObject dialogCanvas;
 
    [SerializeField]
    private TMP_Text speakerText;

    [SerializeField]
    private TMP_Text dialogText;

    [SerializeField]
    private Image portraitImage;

    //Dialog Content
    [SerializeField]
    private string[] speaker;

    [SerializeField]
    [TextArea]
    private string[] dialogWords;

    [SerializeField]
    private Sprite[] portrait;

    private bool dialogActivated;
    private int step;

    [SerializeField]
    private GameObject jalanBuntuCollider;
    private CollectSave[] allCollect;

    private string[] data ;
    
    public GameObject canvasTextSoul;
    public TextMeshProUGUI textCariSoul;
    bool semuaTerkumpul;
    CollectSave collectSave;

    void Start(){
        
        data = new String[]{
            "Excuse me, Grandma, do you know where this place is? I'm Nayla, I'm lost here. I'm looking for my teddy bear, did Grandma see it?",
            "Nduk, awakmu saiki enek ing donya ne para lelembut. Nyapo cah ayu bisa nyasar rene?",
            "What Grandma? Nayla doesn't understand what Grandma is talking about.",
            "Cah ayu, awakmu kudu kondur ing donya manungsa. Nanging, awakmu kudu nggoleki sepuluh jiwa dhisik.Yen wes ketemu, cah ayu bisa kondur menyang donya ne para manungsa. Ati - ati ya cah ayu",
            "I don't understand what the Grandma meant. But I'm sure she gave a clue. I'll keep going then.",
            ""};

            UpdateCollectList();        
            // textCariSoul.gameObject.SetActive(false);
            canvasTextSoul.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && dialogActivated == true)
        {
            ContinueDialog();
        }
    }

    public void UpdateCollectList(){
        allCollect = FindObjectsOfType<CollectSave>();
    }



    public void ContinueDialog(){

            if(step >= speaker.Length)
                {
                    if(dialogCanvas != null){
                        dialogCanvas.SetActive(false);
                    }
                    step = 0;
                    Time.timeScale = 1;                    
                    if(!semuaTerkumpul){
                        canvasTextSoul.SetActive(true);
                    }else{
                        canvasTextSoul.SetActive(false);
                        collectSave.Reset();
                    
                    }
                    // textCariSoul.gameObject.SetActive(true);
                }
                else{
                    
                    UpdateCollectList();
                    semuaTerkumpul = true;
                    foreach(CollectSave collectSoul in allCollect){
                        collectSoul.LoadCollect();
                        if(PlayerPrefs.GetInt(collectSoul.collectId,0) != 1){
                            semuaTerkumpul = false;
                            break;
                        }

                    }
                        // jalanBuntuCollider.SetActive(true);
                    if(!semuaTerkumpul){
                        data[5] = $"Awakmu tinggal ngumpulake {allCollect.Length} soul maneh";
                    textCariSoul.text = $"You need to find {allCollect.Length} more souls";
                        Debug.Log("Kamu kurang " + allCollect.Length);
                    }else{
                        canvasTextSoul.SetActive(false);
                        // textCariSoul.gameObject.SetActive(false);
                        data[5] = $"Nggih cah ayu, saiki yen awakmu kepengen metu saka kene, tutno wae dalan iki, mengko awakmu bakal cethuk Mbah Darmini";
                        Debug.Log("Bagus");
                        jalanBuntuCollider.SetActive(false);
                    }



                    if(dialogCanvas != null)
                    {
                        dialogCanvas.SetActive(true);
                    }
                    if(speakerText != null && dialogText != null && portraitImage != null){
                        speakerText.text = speaker[step];
                        dialogText.text = data[step];
                        portraitImage.sprite = portrait[step];

                    }
                    step += 1;
                    Time.timeScale = 0;
                } 
    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            dialogActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            dialogActivated = false;
            dialogCanvas.SetActive(false);
    }

    


}
