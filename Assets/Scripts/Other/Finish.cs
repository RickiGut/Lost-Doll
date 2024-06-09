using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int totalScenes = SceneManager.sceneCountInBuildSettings;
            print("Jumlah Scene : " + totalScenes);
            if(currentSceneIndex < totalScenes-1){
                SceneManager.LoadScene(currentSceneIndex+1);
                Debug.Log("Masuk ke Scene " + currentSceneIndex+1);
            } else{
                Debug.Log("Sudah mencapai scene akhir");
            }
        }
    }
}
