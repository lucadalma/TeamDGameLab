using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void LoadScene()
    {
        SceneManager.LoadScene("TestBuild1");
    }

    public void QuitApplication() 
    {
        Application.Quit();
    }
}
