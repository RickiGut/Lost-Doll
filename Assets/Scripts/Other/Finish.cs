using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    CollectSave collectSave;
    private PlayerController playerController;

    [SerializeField] ParticleSystem ParFinish1;
    [SerializeField] ParticleSystem ParFinish2;
    [SerializeField] ParticleSystem ParFinish3;

    private void Start(){
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadNextSceneAfterDelay(1.5f)); // Menjalankan coroutine dengan delay 1 detik
            ParFinish1.Play();
            ParFinish2.Play();
            ParFinish3.Play();
        }
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Menunggu selama 1 detik

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        print("Jumlah Scene : " + totalScenes);

        if (currentSceneIndex < totalScenes - 1)
        {
            if (playerController != null)
            {
                playerController.ResetScore();
            }

            SceneManager.LoadScene(currentSceneIndex + 1);
            Debug.Log("Masuk ke Scene " + (currentSceneIndex + 1));
            collectSave.Reset();
            // PlayerPrefs.DeleteKey("intScore");
        }
        else
        {
            Debug.Log("Sudah mencapai scene akhir");
        }
    }

}
