using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreationTimer : MonoBehaviour
{
    public bool first = false; 
    bool activated = false;


    public float creationTime;
    public GameObject DeployUnit;


    public Image timerBar;

    UIManager manager;

    float creationTimeMax;

    void Start()
    {
        creationTimeMax  = creationTime;
        manager =  FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        creation();
        updateCreationBar();
    }



    void creation()
    {
        if (!activated && first ) 
        {
            creationTime -= Time.deltaTime;
        }


        if (creationTime < 0)
        {
            activated = true;
            creationTime = 1;
            manager.AddDeployUnits(DeployUnit);
            manager.remopveUnitOnTimer(gameObject);
        }
    }

    public void cancelUntCreation()
    {
        manager.remopveUnitOnTimer(gameObject);
    }

    void updateCreationBar()
    {
        timerBar.fillAmount = creationTime / creationTimeMax;
    }
}
