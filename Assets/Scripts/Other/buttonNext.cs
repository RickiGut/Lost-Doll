using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonNext : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneManager.LoadScene("Testing");
    }
}
