using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ScriptDialog4Npc : MonoBehaviour
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

    void Start(){
        
        data = new String[]{
            "How can you be here, Grandma? Weren't you there before?!",
            "Nduk, wis gawa sepuluh cahya biru sing tak omongne maeng",
            "I don't know what you mean, but I've collected 10 blue lights here. What should I do with these?",
            "Endi wenehno aku nduk, iki pangananku. Suwun ya cah ayu. Wes gek dang sampeyan lanjutne lakon mu.",
            "Huh? You want to eat these lights? Grandma, you're so strange",
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
                    // PlayerPrefs.DeleteKey("intScore");
                    if(!semuaTerkumpul){
                        canvasTextSoul.SetActive(true);
                    }else{
                        canvasTextSoul.SetActive(false);
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
                        data[5] = $"Awakmu tinggal ngumpulake soul maneh";
                    textCariSoul.text = $"You need to find 10 more souls";
                        Debug.Log("Kamu kurang " + allCollect.Length);
                    }else{
                        canvasTextSoul.SetActive(false);
                        // textCariSoul.gameObject.SetActive(false);
                        data[5] = $"Bagus cah ayu, saiki yen awakmu metu saka kene, awakmu mung kudu ngetutake jalur iki, mengko awakmu bakal ketemu karo wong.";
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
