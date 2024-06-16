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

    void Start(){
        
        data = new String[]{
            "Halo Dunia","Baik kawan,kau gimana?",""};
            UpdateCollectList();        
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
                    PlayerPrefs.DeleteKey("intScore");
                }
                else{
                    
                    UpdateCollectList();
                    bool semuaTerkumpul = true;
                    foreach(CollectSave collectSoul in allCollect){
                        collectSoul.LoadCollect();
                        if(PlayerPrefs.GetInt(collectSoul.collectId,0) != 1){
                            semuaTerkumpul = false;
                            break;
                        }

                    }
                        // jalanBuntuCollider.SetActive(true);
                    if(!semuaTerkumpul){
                    data[2] = $"Aku baik juga,kamu tinggal mengumpulkan {allCollect.Length} collect lagi";
                        Debug.Log("Kamu kurang " + allCollect.Length);
                    }else{
                        data[2] = $"Bagus kamu sudah mendapatkan semua collect";
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
