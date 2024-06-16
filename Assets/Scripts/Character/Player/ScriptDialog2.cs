using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ScriptDialog2 : MonoBehaviour
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


    void Start(){
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && dialogActivated == true)
        {
            ContinueDialog();
        }
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
                    
                 
                    if(dialogCanvas != null)
                    {
                        dialogCanvas.SetActive(true);
                    }
                    if(speakerText != null && dialogText != null && portraitImage != null){
                        speakerText.text = speaker[step];
                        dialogText.text = dialogWords[step];
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
