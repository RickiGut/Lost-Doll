using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonNext : MonoBehaviour
{
    private AudioManager audioManager;

    void Start(){
        audioManager = FindObjectOfType<AudioManager>();
    }


    public void ContinueGame()
    {
        audioManager.PlaySFX(audioManager.MenuButton);
        SceneManager.LoadScene("BTesting1");
    }
}
