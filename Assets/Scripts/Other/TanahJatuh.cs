using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanahJatuh : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    void Start(){
    }

    private void OnTriggerEnter2D(Collider2D other){
            if(other.CompareTag("Player")){
                rb.bodyType = RigidbodyType2D.Dynamic;
                Debug.Log("Berhasil terkena jebakan");
            }

        }
}
