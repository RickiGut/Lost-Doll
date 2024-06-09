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

    void Start(){
        if(PlayerPrefs.HasKey("SavedScene")){
            loadGameButton.interactable = true;
            print("Berhasil di Klik");
        }else{
            // loadGameButton.interactable = false;
            print("Tidak terhubung");
        }
        PanelEmptyLoad.SetActive(false);
    }

    public void MenuStart()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("ScenePembuka");
    }

    public void LoadGame(){
        if(PlayerPrefs.HasKey("SavedScene")){
            string savedScene = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(savedScene);
            print("Berjalan");
        }else{
            Debug.LogWarning("No Saved scene found");
            PanelEmptyLoad.SetActive(true);
        }
    }


    public void CloseLoadEmptyPanel(){
        PanelEmptyLoad.SetActive(false);
    }

}
