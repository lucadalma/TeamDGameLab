using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum GameStatus
{
    GamePause,
    GameRunning
}


public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameEvent pauseEvent;

    [SerializeField]
    GameEvent resumeEvent;

    [SerializeField]
    BoolVariable pause;

    [SerializeField]
    GameObject winMenu;

    [SerializeField]
    GameObject GameOverMenu;

    GameStatus gameStatus;

    private void Start()
    {
        gameStatus = GameStatus.GameRunning;
        pause.SetValue(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameStatus == GameStatus.GameRunning)
            {
                gameStatus = GameStatus.GamePause;
                pauseEvent.Raise();
                pause.SetValue(true);

            }
            else if (gameStatus == GameStatus.GamePause)
            {
                gameStatus = GameStatus.GameRunning;
                resumeEvent.Raise();
                pause.SetValue(false);
            }
        }
    }



    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ActivateMenu(GameObject menu)
    {
        if (menu == null) return;
        menu.SetActive(true);
    }

}
