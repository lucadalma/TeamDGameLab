using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CreationTimer : MonoBehaviour
{
    //public TextMeshProUGUI BuldingOrderText;

    public BuildingButton.ButtonEnum type;
    public bool first = false;
    public int onOrder = 1;
    bool activated = false;
    public BoolVariable pause;
    public BuildingButton.forUnit unitToUpgrade;

    public float creationTime;
    public GameObject DeployUnit;

    public GameObject BuildingPref;
    [NonSerialized] public GameObject buildingSlot;
    [NonSerialized] public Transform parent;

    public Image timerBar;

    UIManager manager;
    EventManager eM;

    public float creationTimeMax;

    enum timerTypeEnum
    {
        unit,
        building
    }


    [SerializeField] timerTypeEnum timerType;
    void Start()
    {

        manager = FindObjectOfType<UIManager>();
        eM = FindObjectOfType<EventManager>();
        if (timerType == timerTypeEnum.building)
        {
            creationTimeMax -= creationTimeMax * eM.newBuildSpeed;
            creationTime = creationTimeMax;

        }
        else
        {
            creationTimeMax = creationTime;
        }
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
        if (pause.Value == false)
        {
            if (!activated && first)
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

    }

    void buildingCreation()
    {
        if (pause.Value == false)
        {
            if (!activated && onOrder == 0)
            {
                creationTime -= Time.deltaTime;
            }


            if (creationTime < 0)
            {
                activated = true;
                creationTime = 1;

                Vector3 spawnSlot = buildingSlot.transform.position;
                Transform parent = buildingSlot.transform.parent;
                manager.removeBuildingButton(buildingSlot.GetComponent<BuildingCategorization>().BuildingButton);
                manager.removeBuildingOnTimer(gameObject);
                Destroy(buildingSlot);
                GameObject newBuild = Instantiate(BuildingPref, spawnSlot, Quaternion.identity,parent);
                if (unitToUpgrade != BuildingButton.forUnit.notUnitUpgrade)
                {
                    newBuild.GetComponent<BuildingCategorization>().unitToUpgrade = unitToUpgrade;
                }
                manager.addBuildingButton(newBuild.GetComponent<BuildingCategorization>().BuildingButton);
//                newBuild.transform.parent = parent;
            }

        }
    }


    public void cancelUntCreation()
    {
        manager.removeUnitOnTimer(gameObject);
    }

    public void cancelBuildingCreation()
    {
        buildingSlot.layer = LayerMask.NameToLayer("Buildings");
        manager.removeBuildingOnTimer(gameObject);
    }

    void updateCreationBar()
    {
        timerBar.fillAmount = creationTime / creationTimeMax;
    }
}
