using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    [NonSerialized] public Vector2 centerPoint;

    public bool isSingleUse;

    public GameObject BuildingTimer;
    public enum TypeEnum
    {
        category,
        nonUniyUpgradebuilding,
        unitUpgradeBuilding,
        unitSelector,
        backToCtaegory,
        backToUpgrades,
    }

    public enum CategoryEnum
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
        engineTurbocharger,
        selfRapair,
        reactiveArmor,
        TargetingComputer,
        HeavyCaliber,
        secondaryWeapon,
        riotMode,
        stremlinedProduction,
        ablativeArmor,
        advancedOptics,
        missileStrike,
        overdrive,
        eMP,
        fortification
    }

    public enum forUnit
    {
        notUnitUpgrade,
        dart,
        javelin,
        mace,
        gladius,
    }



    UIManager uIM;
    EventManager Em;

    public TypeEnum type;
    public CategoryEnum category;
    public ButtonEnum buttonType;
    public forUnit unitAffected;


    void Start()
    {
        uIM = FindObjectOfType<UIManager>();
        Em = FindObjectOfType<EventManager>();

        if (type == TypeEnum.nonUniyUpgradebuilding)
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
        if (TypeEnum.backToCtaegory == type)
        {
            uIM.removeBuiidingMenu(centerPoint);
            uIM.openBuildingMenu(transform.position);
        }

        if (TypeEnum.backToUpgrades == type)
        {
            uIM.selectBuildingCategory(transform.position, CategoryEnum.upgrades);
        }
    }

    public void openForUnitSelector()
    {
        if (TypeEnum.unitUpgradeBuilding == type)
            uIM.SelectUnitToUpgrade(centerPoint, buttonType, unitAffected, BuildingTimer);
    }




    public void StartBuildingTimer()
    {
        uIM.addBuildingOnTimer(BuildingTimer, centerPoint, type, unitAffected, buttonType);
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
