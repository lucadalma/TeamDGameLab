using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveTimer : MonoBehaviour
{

    WaveDisplay waveDisplay;

    public BoolVariable pause;

    int CurrentTime;

    [SerializeField] TextMeshProUGUI timerDisplay;

    private void Start()
    {
        waveDisplay = FindObjectOfType<WaveDisplay>();
    }

    private void Update()
    {
        displayTimer();
    }


    public void setTimer(float time)
    {
        StopAllCoroutines();
        CurrentTime = ((int)time);
        waveDisplay.ChangeDisplay();
        StartCoroutine(tickTimer());
    }





    private void displayTimer()
    {
        int timerToSeconds;
        int timerToMinutes;

        timerToSeconds = CurrentTime - (60 * Mathf.FloorToInt(CurrentTime / 60f));
        timerToMinutes = Mathf.FloorToInt(CurrentTime / 60f);

        if (timerToSeconds < 10 && timerToSeconds < 10)
        {
            timerDisplay.text = "0" + timerToMinutes + ":0" + timerToSeconds;
        }
        else if (timerToSeconds >= 10 && timerToSeconds < 10)
        {
            timerDisplay.text = timerToMinutes + ":0" + timerToSeconds;
        }
        else if (timerToSeconds < 10 && timerToSeconds >= 10)
        {
            timerDisplay.text = "0" + timerToMinutes + ":" + timerToSeconds;
        }
        else if (timerToSeconds >= 10 && timerToSeconds >= 10)
        {
            timerDisplay.text = timerToMinutes + ":" + timerToSeconds;
        }

    }


    private IEnumerator tickTimer()
    {
        while (CurrentTime > 1)
        {
            while (pause.Value)
            {
                yield return null;
            }
            yield return new WaitForSeconds(1);
            CurrentTime--;
        }
    }

}

