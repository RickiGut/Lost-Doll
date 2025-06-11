using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCSpirit2 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D  other){
        if(other.CompareTag("Player")){
            gameObject.SetActive(false);
        }
    }

}
