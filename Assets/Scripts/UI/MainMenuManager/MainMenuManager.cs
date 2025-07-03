using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public void LoadGameScene()
    {
        SceneManager.LoadScene("DemoLevel");
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }


    public void QuitApplication() 
    {
        Application.Quit();
    }
}
