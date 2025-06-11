using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjInteraction : MonoBehaviour
{
    public string objectDesc;
    public GameObject EIndicator;
    public GameObject BGEIndicator;
    public GameObject descriptionPanel;
    public GameObject PanelBg;
    public TextMeshProUGUI descText;

    private bool isPlayerNear = false;
    // Start is called before the first frame update
    void Start()
    {
        EIndicator.SetActive(false);
        BGEIndicator.SetActive(false);
        descriptionPanel.SetActive(false);
        PanelBg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            DescriptionShow();
        }
    }
     
        void DescriptionShow(){
            descriptionPanel.SetActive(true);
            PanelBg.SetActive(true);
            descText.text = objectDesc;
            Time.timeScale = 0;
        }


       public void DescriptionHide()
        {
            descriptionPanel.SetActive(false);
            PanelBg.SetActive(false);
            Time.timeScale = 1;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                EIndicator.SetActive(true);
                BGEIndicator.SetActive(true);
                isPlayerNear = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                EIndicator.SetActive(false);
                BGEIndicator.SetActive(false);
                isPlayerNear = false;
                DescriptionHide();
            }
        }

}
