using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilityButton : MonoBehaviour
{
    AbilityManager aM;

    [SerializeField] Button button;
    [SerializeField] Image TimerImage;

    [SerializeField] float timer = 5;
    float timerTime = 0;


    void Start()
    {
        aM = FindObjectOfType<AbilityManager>();
        timer = Time.time;
    }

    void Update()
    {
        if (timerTime > 0)
        {

            if(button.enabled == true)
            {
                button.enabled = false;
            }

            timerTime -= Time.deltaTime;
            TimerImage.fillAmount = timerTime / timer ;


        }
        else
        {
            if (TimerImage.enabled == true)
            {
            TimerImage.enabled = false;
            TimerImage.fillAmount = 0;
            }

            if (aM.abilità != null)
            {
                if (button.enabled == true)
                {
                    button.enabled = false;
                }
            }
            else
            {
                if (button.enabled == false)
                {
                    button.enabled = true;
                }
            }
        }
    }

    public void OnButtonPress(GameObject obj)
    {
        aM.abilità = obj;
        aM.currentButton = this;
    }

    public void StartTimer()
    {
        timerTime = timer;
        TimerImage.enabled = true;
        TimerImage.fillAmount = 1;
    }
}
