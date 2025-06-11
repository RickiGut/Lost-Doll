using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Detection"){
            Destroy(this.gameObject);
        }
    }

}
