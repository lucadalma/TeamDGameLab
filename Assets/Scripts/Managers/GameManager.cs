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
    GameObject winMenu;

    [SerializeField]
    GameObject GameOverMenu;

    GameStatus gameStatus;

    private void Start()
    {
        gameStatus = GameStatus.GameRunning;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameStatus == GameStatus.GameRunning)
            {
                gameStatus = GameStatus.GamePause;
                pauseEvent.Raise();

            }
            else if (gameStatus == GameStatus.GamePause)
            {
                gameStatus = GameStatus.GameRunning;
                resumeEvent.Raise();
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
