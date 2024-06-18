using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject canvasSetting;
    private AudioManager audioManager;
    public GameObject creditTutorial;
    public GameObject panelTutorial;

    void Start(){
         audioManager = FindObjectOfType<AudioManager>();
        canvasSetting.SetActive(false);
    }

    public void PauseGame(){
        audioManager.PlaySFX(audioManager.MenuButton);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        audioManager.PlaySFX(audioManager.MenuButton);
        Time.timeScale = 1f;
    }

    public void SettingGame(){
        audioManager.PlaySFX(audioManager.MenuButton);
        canvasSetting.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        audioManager.PlaySFX(audioManager.MenuButton);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuStart");
    }

      public void CloseSetting(){
        audioManager.PlaySFX(audioManager.MenuButton);
       canvasSetting.gameObject.SetActive(false);
    }

        public void OpenTutorial(){
        audioManager.PlaySFX(audioManager.MenuButton);  
        creditTutorial.SetActive(true);
    }

    public void CloseTutorial(){
        audioManager.PlaySFX(audioManager.MenuButton);
        creditTutorial.SetActive(false);
    }

    public void OpenTutorial2(){
        audioManager.PlaySFX(audioManager.MenuButton);
        panelTutorial.SetActive(true);
    }

    public void CloseTutorial2(){
        audioManager.PlaySFX(audioManager.MenuButton);
        panelTutorial.SetActive(false);
    }

}
