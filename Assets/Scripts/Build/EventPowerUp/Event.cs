using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EventType
{
    None,
    HPRegen,
    Speed,
    BuildingCreationSpeed,
    MaxHP,
    Armor,
    Range,
    Caliber,
    Riot,
    MaceUnit,
    DartUnit,
    GladiusUnit
};





public class Event : MonoBehaviour
{
    EventManager EM;
    public BuildingButton.forUnit unitToUpgrade;

    BuildingCategorization BC;

    StackEvent SE;
    public EventType eventType;

    [Header("AmmountStack")]
    [Tooltip("Prima stack")]
    public float ammount1;
    [Tooltip("Se il build ha una seconda stack differente dalla prima")]
    public float ammount2;
    [Tooltip("Se il build ha una debuff")]
    public float debuff;


    float stack, stack1, stack2, stack3, stack4;
    float dart, javelin, mace, gladius;


    private void Start()
    {
        if (EM == null)
            EM = FindObjectOfType<EventManager>();
        if (SE == null)
            SE = FindObjectOfType<StackEvent>();
        if (BC == null)
            BC = FindObjectOfType<BuildingCategorization>();

        unitToUpgrade = BC.unitToUpgrade;

        Stack();
    }


    void Update()
    {
        SwitcAbility();
    }


    #region AddPower&Stack

    void SwitcAbility()
    {
        switch (eventType)
        {
            case EventType.None:
                break;
            case EventType.HPRegen:
                EM.AddListAction(HPRegeneration);
                break;
            case EventType.Speed:
                EM.AddListAction(SpeedUp);
                break;
            case EventType.MaceUnit:
                UnitaUnLook(true, false, false);
                break;
            case EventType.DartUnit:
                UnitaUnLook(false, true, false);
                break;
            case EventType.GladiusUnit:
                UnitaUnLook(false, false, true);
                break;
            case EventType.BuildingCreationSpeed:
                EM.AddListAction(BuildSpeed);
                break;
            case EventType.MaxHP:
                EM.AddListAction(MaxHP);
                break;
            case EventType.Armor:
                EM.AddListAction(Armor);
                break;
            case EventType.Range:
                EM.AddListAction(Range);
                break;
            case EventType.Caliber:
                EM.AddListAction(Caliber);
                break;
            case EventType.Riot:
                EM.AddListAction(Riot);
                break;
            default:
                break;
        }




    }


    void Stack()
    {
        if (eventType == EventType.HPRegen)
        {
            SE.AddHpStackList(this.gameObject);
        }

        if (eventType == EventType.Speed)
            SE.AddSpeedUpStackList(this.gameObject);
        if (eventType == EventType.BuildingCreationSpeed)
            SE.AddBuildSpeedStackList(this.gameObject);
        if (eventType == EventType.MaxHP)
            SE.AddMaxHpStackList(this.gameObject);
        if (eventType == EventType.Armor)
            SE.AddArmorStackList(this.gameObject);
        if (eventType == EventType.Range)
            SE.AddRangeStackList(this.gameObject);
        if (eventType == EventType.Caliber)
            SE.AddCaliberStackList(this.gameObject);
        if (eventType == EventType.Riot)
            SE.AddRiotStackList(this.gameObject);

    }

    #endregion


    #region PowerUp
    private void HPRegeneration()
    {
        if (unitToUpgrade == BuildingButton.forUnit.dart)
        {
            stack1 = SE.ChangeStackHp(stack1, ammount1, ammount2);
            dart = stack1;
            EM.newHPReg1 = dart;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.javelin)
        {
            stack2 = SE.ChangeStackHp2(stack2, ammount1, ammount2);
            javelin = stack2;
            EM.newHPReg2 = javelin;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {
            stack3 = SE.ChangeStackHp3(stack3, ammount1, ammount2);
            mace = stack3;
            EM.newHPReg3 = mace;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {
            stack4 = SE.ChangeStackHp4(stack4, ammount1, ammount2);
            gladius = stack4;
            EM.newHPReg4 = gladius;
        }




    }



    private void SpeedUp()
    {

        if (unitToUpgrade == BuildingButton.forUnit.dart)
        {
            stack = SE.ChangeStackSpeedUp(stack, ammount1, ammount2);
            dart = stack;
            EM.newMoveSpeed1 = dart;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.javelin)
        {
            stack = SE.ChangeStackSpeedUp(stack, ammount1, ammount2);
            javelin = stack;
            EM.newMoveSpeed2 = javelin;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {
            stack = SE.ChangeStackSpeedUp(stack, ammount1, ammount2);
            mace = stack;
            EM.newMoveSpeed3 = mace;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {
            stack = SE.ChangeStackSpeedUp(stack, ammount1, ammount2);
            gladius = stack;
            EM.newMoveSpeed4 = gladius;
        }





    }


    private void BuildSpeed()
    {
        stack = SE.ChangeStackBuildSpeed(stack, ammount1);
        EM.newBuildSpeed = stack;
    }


    private void MaxHP()
    {

        if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {

            stack = SE.ChangeStackMaxHp(stack, ammount1);
            dart = stack;
            EM.newHp1 = dart;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.dart)
        {
            stack = SE.ChangeStackMaxHp(stack, ammount1);
            javelin = stack;
            EM.newHp2 = javelin;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.javelin)
        {
            stack = SE.ChangeStackMaxHp(stack, ammount1);
            mace = stack;
            EM.newHp3 = mace;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {
            stack = SE.ChangeStackMaxHp(stack, ammount1);
            gladius = stack;
            EM.newHp4 = gladius;
        }



    }

    private void Armor()
    {

        if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {

        }
        else if (unitToUpgrade == BuildingButton.forUnit.dart)
        {

        }
        else if (unitToUpgrade == BuildingButton.forUnit.javelin)
        {

        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {

        }




        stack = SE.ChangeStackArmor(stack, ammount1, ammount2);
        EM.newArmor1 = stack;
    }

    private void Range()
    {

        if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {

        }
        else if (unitToUpgrade == BuildingButton.forUnit.dart)
        {

        }
        else if (unitToUpgrade == BuildingButton.forUnit.javelin)
        {

        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {

        }





        stack = SE.ChangeStackRange(stack, ammount1, ammount2);
        EM.newRange1 = stack;
        EM.newMoveSpeed1 = SE.SpeedDebuff(EM.newMoveSpeed1, debuff);
    }

    private void Caliber()
    {


        if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {

        }
        else if (unitToUpgrade == BuildingButton.forUnit.dart)
        {

        }
        else if (unitToUpgrade == BuildingButton.forUnit.javelin)
        {

        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {

        }



        stack = SE.ChangeStackCaliber(stack, ammount1);
        EM.newDmg1 = stack;
        EM.newReload1 = SE.ReloadDebuff(EM.newReload1, debuff);
    }

    private void Riot() //[1]
    {



        if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {

        }
        else if (unitToUpgrade == BuildingButton.forUnit.dart)
        {

        }
        else if (unitToUpgrade == BuildingButton.forUnit.javelin)
        {

        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {

        }




        stack = SE.ChangeStackRiot(stack, debuff);
        EM.newDmg1 = stack;
        EM.newReload1 = SE.ReloadBuff(EM.newReload1, ammount1);

    }

    private void UnitaUnLook(bool red, bool green, bool blue)
    {
        if (red == true)
            EM.Mace = true;
        if (green == true)
            EM.Dart = true;
        if (blue == true)
            EM.Gladius = true;

    }
    #endregion

    #region OnDestroy
    private void OnDestroy()
    {
        switch (eventType)
        {
            case EventType.None:
                break;
            case EventType.HPRegen:
                EM.RemoveListAction(HPRegeneration);
                SE.RemoveHpStackList(this.gameObject);
                SE.ChangeStackHp(stack, ammount1, ammount2);
                break;
            case EventType.Speed:
                EM.RemoveListAction(SpeedUp);
                SE.RemoveSpeedUpStackList(this.gameObject);
                SE.ChangeStackSpeedUp(stack, ammount1, ammount2);
                break;
            case EventType.BuildingCreationSpeed:
                EM.RemoveListAction(BuildSpeed);
                SE.RemoveBuildSpeedStackList(this.gameObject);
                SE.ChangeStackBuildSpeed(stack, ammount1);
                break;
            case EventType.MaxHP:
                EM.RemoveListAction(MaxHP);
                SE.RemoveMaxHpStackList(this.gameObject);
                SE.ChangeStackMaxHp(stack, ammount1);
                break;
            case EventType.Armor:
                EM.RemoveListAction(Armor);
                SE.RemoveArmorStackList(this.gameObject);
                SE.ChangeStackArmor(stack, ammount1, ammount2);
                break;
            case EventType.Range:
                EM.RemoveListAction(Range);
                SE.RemoveRangeStackList(this.gameObject);
                SE.ChangeStackRange(stack, ammount1, ammount2);
                SE.SpeedDebuff(EM.newMoveSpeed1, debuff);
                break;
            case EventType.Caliber:
                EM.RemoveListAction(Caliber);
                SE.RemoveCaliberStackList(this.gameObject);
                SE.ChangeStackCaliber(stack, ammount1);
                SE.ReloadDebuff(EM.newReload1, debuff);
                break;
            case EventType.Riot:
                EM.RemoveListAction(Riot);
                SE.RemoveRiotStackList(this.gameObject);
                SE.ChangeStackRiot(stack, debuff);
                SE.ReloadBuff(EM.newReload1, ammount1);
                break;
            case EventType.MaceUnit:
                UnitaUnLook(false, false, false);
                break;
            case EventType.DartUnit:
                UnitaUnLook(false, false, false);
                break;
            case EventType.GladiusUnit:
                UnitaUnLook(false, false, false);
                break;
            default:
                break;
        }


    }
    #endregion


}
