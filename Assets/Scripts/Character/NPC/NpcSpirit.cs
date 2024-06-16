using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpirit : MonoBehaviour
{
    public bool isMeet;
    // Start is called before the first frame update
    void Start()
    {
        // isMeet = PlayerPrefs.GetInt("NPCSpirit_Met",0)==1;

        // if(isMeet){
        //     gameObject.SetActive(false);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            isMeet = true;
            this.gameObject.SetActive(true);
            print(isMeet);
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            Destroy(this.gameObject);
            print(isMeet);
            PlayerPrefs.SetInt("NPCSpirit_Met",1);
        }
    }
}
