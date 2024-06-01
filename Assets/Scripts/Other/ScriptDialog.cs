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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && dialogActivated == true)
        {
            if(step >= speaker.Length)
            {
                dialogCanvas.SetActive(false);
                step = 0;
            }else{
                dialogCanvas.SetActive(true);
                speakerText.text = speaker[step];
                dialogText.text = dialogWords[step];
                portraitImage.sprite = portrait[step];
                step += 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "npc"){
            dialogActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "npc")
        dialogActivated = false;
        dialogCanvas.SetActive(false);
    }
}
