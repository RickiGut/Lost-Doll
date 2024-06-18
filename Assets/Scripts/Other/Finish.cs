using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    CollectSave collectSave;
    private PlayerController playerController;

    private void Start(){
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int totalScenes = SceneManager.sceneCountInBuildSettings;
            print("Jumlah Scene : " + totalScenes);
            if(currentSceneIndex < totalScenes-1){
                if(playerController != null){
                    playerController.ResetScore();
                }
                SceneManager.LoadScene(currentSceneIndex+1);
                Debug.Log("Masuk ke Scene " + currentSceneIndex+1);
                collectSave.Reset();
                // PlayerPrefs.DeleteKey("intScore");
            } else{
                Debug.Log("Sudah mencapai scene akhir");
            }
        }
    }
}
