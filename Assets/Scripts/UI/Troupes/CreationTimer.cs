using System;
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

    public GameObject BuildingPref;
    [NonSerialized] public GameObject buildingSlot;

    public Image timerBar;

    UIManager manager;

    float creationTimeMax;

    enum timerTypeEnum
    {
        unit,
        building
    }

    [SerializeField] timerTypeEnum timerType;
    void Start()
    {
        creationTimeMax  = creationTime;
        manager =  FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {

        updateCreationBar();

        if (timerType == timerTypeEnum.unit)
        {
            unitCreation();

        }
        else if (timerType == timerTypeEnum.building)
        {
            buildingCreation();
        }
    }



    void unitCreation()
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
            manager.removeUnitOnTimer(gameObject);
        }
    }

    void buildingCreation()
    {
        if (!activated && first)
        {
            creationTime -= Time.deltaTime;
        }


        if (creationTime < 0)
        {
            activated = true;
            creationTime = 1;

            Vector3 spawnSlot = buildingSlot.transform.position;
            manager.removeBuildingOnTimer(gameObject);
            Destroy(buildingSlot);
            Instantiate(BuildingPref, spawnSlot, Quaternion.identity);
        }
    }


    public void cancelUntCreation()
    {
        manager.removeUnitOnTimer(gameObject);
    }

    public void cancelBuildingCreation()
    {
       manager.removeBuildingOnTimer(gameObject);
    }

    void updateCreationBar()
    {
        timerBar.fillAmount = creationTime / creationTimeMax;
    }
}
