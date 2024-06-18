using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button newGameButton;
    public Button loadGameButton;
    public GameObject PanelEmptyLoad;
    public GameObject settingPannel;
    private AudioManager audioManager;
    public GameObject panelTutorial;
    public GameObject creditTutorial;

    void Start(){
        audioManager = FindObjectOfType<AudioManager>();

        if(PlayerPrefs.HasKey("SavedScene")){
            loadGameButton.interactable = true;
            print("Berhasil di Klik");
        }else{
            // loadGameButton.interactable = false;
            print("Tidak terhubung");
        }
        PanelEmptyLoad.SetActive(false);
        settingPannel.SetActive(false);

        panelTutorial.SetActive(false);

    }

    public void MenuStart()
    {
        audioManager.PlaySFX(audioManager.MenuButton);
        float musicVolume = PlayerPrefs.GetFloat("musicVolume",1.0f);
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume",1.0f);

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("musicVolume",musicVolume);
        PlayerPrefs.SetFloat("SfxVolume",sfxVolume);
        PlayerPrefs.Save();
        SceneManager.LoadScene("ScenePembuka");
    }

    public void LoadGame(){
        if(PlayerPrefs.HasKey("SavedScene")){
            audioManager.PlaySFX(audioManager.MenuButton);
            string savedScene = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(savedScene);
            print("Berjalan");
        }else{
            audioManager.PlaySFX(audioManager.MenuButton);
            Debug.LogWarning("No Saved scene found");
            PanelEmptyLoad.SetActive(true);
        }
    }

    public void SettingGame(){
        audioManager.PlaySFX(audioManager.MenuButton);
        settingPannel.gameObject.SetActive(true);
    }


    public void QuitGame(){
        Application.Quit();
    }

    public void CloseLoadEmptyPanel(){
       audioManager.PlaySFX(audioManager.MenuButton);
        PanelEmptyLoad.SetActive(false);
    }

    public void CloseSetting(){
        audioManager.PlaySFX(audioManager.MenuButton);
        settingPannel.gameObject.SetActive(false);
    }

    public void OpenTutorial(){
        audioManager.PlaySFX(audioManager.MenuButton);
        panelTutorial.SetActive(true);
    }

    public void CloseTutorial(){
        audioManager.PlaySFX(audioManager.MenuButton);
        panelTutorial.SetActive(false);
    }

     public void OpenTutorial2(){
        audioManager.PlaySFX(audioManager.MenuButton);
        creditTutorial.SetActive(true);
    }

    public void CloseTutorial2(){
        audioManager.PlaySFX(audioManager.MenuButton);
        creditTutorial.SetActive(false);
    }


}
