using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    [NonSerialized] public Vector2 centerPoint;

    public GameObject BuildingTimer;
    public enum TypeEnum
    {
        category,
        building,
        back
    }

    public enum CtegoryEnum
    {
        nullCategory,
        units,
        upgrades,
        abilities,
        fortification,
        supplayDepot,

    }

    UIManager uIM;

    public TypeEnum type;
    public CtegoryEnum category;

    void Start()
    {
        uIM = FindObjectOfType<UIManager>();
    }

    public void openCategory()
    {
        if (TypeEnum.category == type)
            uIM.selectBuildingCategory(centerPoint, category);
    }

    public void backToCategorySelector()
    {
        if (TypeEnum.back == type)
        {
            uIM.removeBuiidingMenu(centerPoint);
            uIM.openBuildingMenu(transform.position);
        }
    }

    public void StartBuildingTimer()
    {
        uIM.addBuildingOnTimer(BuildingTimer, centerPoint);
    }
}
