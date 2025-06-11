using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCPenandaSpirit : MonoBehaviour
{
     public TextMeshProUGUI teksHilang;

    void Start()
    {
        teksHilang.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            teksHilang.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            teksHilang.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
