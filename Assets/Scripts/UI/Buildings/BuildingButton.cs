using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public enum ButtonEnum
    {
        nullButton,
        SupplyDepot,
        dart,
        javelin,
        mace,
        gladius,
        engineUpgrade,
        selfRapair,
        reactiveArmor,
        rangeUpgrade,
        DamageUpgrade,
        secondaryWeapon,
        riotMode,
        stremlinedProduction,
        advancedTargeting,
        artilleryStrike,
        eMP,
        fortification
    }

    UIManager uIM;
    EventManager Em;

    public TypeEnum type;
    public CtegoryEnum category;
    public ButtonEnum buttonType;


    void Start()
    {
        uIM = FindObjectOfType<UIManager>();
        Em = FindObjectOfType<EventManager>();

        if (type == TypeEnum.building)
        {
            switch (buttonType)     
            {
                case ButtonEnum.dart:
                    alreadyInSceneCheck(Em.Dart);
                    break;
                case ButtonEnum.javelin:
                    break;
                case ButtonEnum.mace:
                    alreadyInSceneCheck(Em.Mace);
                    break;
                case ButtonEnum.gladius:
                    alreadyInSceneCheck(Em.Gladius);
                    break;
                default:
                    break;
            }
        }
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

    void alreadyInSceneCheck(bool isInScene)
    {
        if (!isInScene)
        {
           GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
