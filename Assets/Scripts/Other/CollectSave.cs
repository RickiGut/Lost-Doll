using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSave : MonoBehaviour
{
    public string collectId;
    // Start is called before the first frame update
    void Start()
    {
     int buahStatus = PlayerPrefs.GetInt(collectId, 0);
     Debug.Log($"Starting {collectId} with status: {buahStatus}");
     LoadCollect();   
    }


    public void Take(){
        PlayerPrefs.SetInt(collectId,1);
        gameObject.SetActive(false);
    }

    public void Reset(){
        PlayerPrefs.SetInt(collectId,0);
        gameObject.SetActive(true);
         Debug.Log($"Resetting {collectId}");
    }

    public void LoadCollect(){
        if(PlayerPrefs.GetInt(collectId,0) == 1){
            gameObject.SetActive(false);
        }
    }

}
