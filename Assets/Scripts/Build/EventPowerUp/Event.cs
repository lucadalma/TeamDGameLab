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
    GladiusUnit,
    JavUnit
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


    float stack;
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
                UnitaUnLook(true, false, false, false);
                break;
            case EventType.DartUnit:
                UnitaUnLook(false, true, false, false);
                break;
            case EventType.GladiusUnit:
                UnitaUnLook(false, false, true, false);
                break;
            case EventType.JavUnit:
                UnitaUnLook(false, false, false, true);
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
            if (unitToUpgrade == BuildingButton.forUnit.dart)
                SE.AddHpStackList(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.javelin)
                SE.AddHpStackList2(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.mace)
                SE.AddHpStackList3(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.gladius)
                SE.AddHpStackList4(this.gameObject);
        }

        if (eventType == EventType.Speed)
        {
            if (unitToUpgrade == BuildingButton.forUnit.dart)
                SE.AddSpeedUpStackList(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.javelin)
                SE.AddSpeedUpStackList2(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.mace)
                SE.AddSpeedUpStackList3(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.gladius)
                SE.AddSpeedUpStackList4(this.gameObject);

        }
        if (eventType == EventType.BuildingCreationSpeed)
        {
            SE.AddBuildSpeedStackList(this.gameObject);

        }
        if (eventType == EventType.MaxHP)
        {
            if (unitToUpgrade == BuildingButton.forUnit.dart)
                SE.AddMaxHpStackList(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.javelin)
                SE.AddMaxHpStackList2(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.mace)
                SE.AddMaxHpStackList3(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.gladius)
                SE.AddMaxHpStackList4(this.gameObject);

        }
        if (eventType == EventType.Armor)
        {
            if (unitToUpgrade == BuildingButton.forUnit.dart)
                SE.AddArmorStackList(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.javelin)
                SE.AddArmorStackList2(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.mace)
                SE.AddArmorStackList3(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.gladius)
                SE.AddArmorStackList4(this.gameObject);

        }
        if (eventType == EventType.Range)
        {
            if (unitToUpgrade == BuildingButton.forUnit.dart)
                SE.AddRangeStackList(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.javelin)
                SE.AddRangeStackList2(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.mace)
                SE.AddRangeStackList3(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.gladius)
                SE.AddRangeStackList4(this.gameObject);

        }
        if (eventType == EventType.Caliber)
        {
            if (unitToUpgrade == BuildingButton.forUnit.dart)
                SE.AddCaliberStackList(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.javelin)
                SE.AddCaliberStackList2(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.mace)
                SE.AddCaliberStackList3(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.gladius)
                SE.AddCaliberStackList4(this.gameObject);

        }
        if (eventType == EventType.Riot)
        {
            if (unitToUpgrade == BuildingButton.forUnit.dart)
                SE.AddRiotStackList(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.javelin)
                SE.AddRiotStackList2(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.mace)
                SE.AddRiotStackList3(this.gameObject);
            if (unitToUpgrade == BuildingButton.forUnit.gladius)
                SE.AddRiotStackList4(this.gameObject);

        }

    }

    #endregion

    #region PowerUp
    private void HPRegeneration()
    {
        if (unitToUpgrade == BuildingButton.forUnit.dart)
        {
            stack = SE.ChangeStackHp(stack, ammount1, ammount2);
            dart = stack * Time.deltaTime;
            EM.newHPReg1 = dart;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.javelin)
        {
            stack = SE.ChangeStackHp2(stack, ammount1, ammount2);
            javelin = stack * Time.deltaTime;
            EM.newHPReg2 = javelin;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {
            stack = SE.ChangeStackHp3(stack, ammount1, ammount2);
            mace = stack * Time.deltaTime;
            EM.newHPReg3 = mace;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {
            stack = SE.ChangeStackHp4(stack, ammount1, ammount2);
            gladius = stack * Time.deltaTime;
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
            stack = SE.ChangeStackSpeedUp2(stack, ammount1, ammount2);
            javelin = stack;
            EM.newMoveSpeed2 = javelin;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {
            stack = SE.ChangeStackSpeedUp3(stack, ammount1, ammount2);
            mace = stack;
            EM.newMoveSpeed3 = mace;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {
            stack = SE.ChangeStackSpeedUp4(stack, ammount1, ammount2);
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

        if (unitToUpgrade == BuildingButton.forUnit.dart)
        {

            stack = SE.ChangeStackMaxHp(stack, ammount1);
            dart = stack;
            EM.newHp1 = dart;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.javelin)
        {
            stack = SE.ChangeStackMaxHp2(stack, ammount1);
            javelin = stack;
            EM.newHp2 = javelin;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {
            stack = SE.ChangeStackMaxHp3(stack, ammount1);
            mace = stack;
            EM.newHp3 = mace;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {
            stack = SE.ChangeStackMaxHp4(stack, ammount1);
            gladius = stack;
            EM.newHp4 = gladius;
        }



    }

    private void Armor()
    {
        if (unitToUpgrade == BuildingButton.forUnit.dart)
        {
            dart = SE.ChangeStackArmor(stack, ammount1, ammount2);
            EM.newArmor1 = dart;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.javelin)
        {
            javelin = SE.ChangeStackArmor2(stack, ammount1, ammount2);
            EM.newArmor1 = javelin;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {
            mace = SE.ChangeStackArmor3(stack, ammount1, ammount2);
            EM.newArmor1 = mace;
        }
        else if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {
            gladius = SE.ChangeStackArmor4(stack, ammount1, ammount2);
            EM.newArmor1 = gladius;
        }


    }

    private void Range()
    {

        if (unitToUpgrade == BuildingButton.forUnit.dart)
        {
            dart = SE.ChangeStackRange(stack, ammount1, ammount2);
            EM.newRange1 = dart;
            EM.newMoveSpeed1 = SE.SpeedDebuff(EM.newMoveSpeed1, debuff);
        }
        else if (unitToUpgrade == BuildingButton.forUnit.javelin)
        {

            javelin = SE.ChangeStackRange2(stack, ammount1, ammount2);
            EM.newRange1 = javelin;
            EM.newMoveSpeed1 = SE.SpeedDebuff2(EM.newMoveSpeed1, debuff);
        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {

            mace = SE.ChangeStackRange3(stack, ammount1, ammount2);
            EM.newRange1 = mace;
            EM.newMoveSpeed1 = SE.SpeedDebuff3(EM.newMoveSpeed1, debuff);
        }
        else if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {

            gladius = SE.ChangeStackRange4(stack, ammount1, ammount2);
            EM.newRange1 = gladius;
            EM.newMoveSpeed1 = SE.SpeedDebuff4(EM.newMoveSpeed1, debuff);
        }





      
    }

    private void Caliber()
    {


        if (unitToUpgrade == BuildingButton.forUnit.dart)
        {
            dart = SE.ChangeStackCaliber(stack, ammount1);
            EM.newDmg1 = dart;
            EM.newReload1 = SE.ReloadDebuff(EM.newReload1, debuff);
        }
        else if (unitToUpgrade == BuildingButton.forUnit.javelin)
        {
            javelin = SE.ChangeStackCaliber2(stack, ammount1);
            EM.newDmg1 = javelin;
            EM.newReload1 = SE.ReloadDebuff2(EM.newReload1, debuff);
        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {
            mace = SE.ChangeStackCaliber3(stack, ammount1);
            EM.newDmg1 = mace;
            EM.newReload1 = SE.ReloadDebuff3(EM.newReload1, debuff);
        }
        else if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {
            gladius = SE.ChangeStackCaliber4(stack, ammount1);
            EM.newDmg1 = gladius;
            EM.newReload1 = SE.ReloadDebuff4(EM.newReload1, debuff);
        }



      
    }

    private void Riot() //[1]
    {



        if (unitToUpgrade == BuildingButton.forUnit.dart)
        {
            dart = SE.ChangeStackRiot(stack, debuff);
            EM.newDmg1 = dart;
            EM.newReload1 = SE.ReloadBuff(EM.newReload1, ammount1);

        }
        else if (unitToUpgrade == BuildingButton.forUnit.javelin)
        {
            javelin = SE.ChangeStackRiot2(stack, debuff);
            EM.newDmg1 = javelin;
            EM.newReload1 = SE.ReloadBuff2(EM.newReload1, ammount1);
        }
        else if (unitToUpgrade == BuildingButton.forUnit.mace)
        {
            mace = SE.ChangeStackRiot3(stack, debuff);
            EM.newDmg1 = mace;
            EM.newReload1 = SE.ReloadBuff3(EM.newReload1, ammount1);
        }
        else if (unitToUpgrade == BuildingButton.forUnit.gladius)
        {
            gladius = SE.ChangeStackRiot4(stack, debuff);
            EM.newDmg1 = gladius;
            EM.newReload1 = SE.ReloadBuff4(EM.newReload1, ammount1);
        }




        
    }

    private void UnitaUnLook(bool red, bool green, bool blue, bool viola)
    {
        if (red == true)
            EM.Mace = true;
        if (green == true)
            EM.Dart = true;
        if (blue == true)
            EM.Gladius = true;
        if (viola == true)
            EM.Javelin = true;

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
                if (unitToUpgrade == BuildingButton.forUnit.dart)
                    SE.RemoveHpStackList(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.javelin)
                    SE.RemoveHpStackList2(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.mace)
                    SE.RemoveHpStackList3(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.gladius)
                    SE.RemoveHpStackList4(this.gameObject);
                SE.ChangeStackHp(stack, ammount1, ammount2);
                break;
            case EventType.Speed:
                EM.RemoveListAction(SpeedUp);
                if (unitToUpgrade == BuildingButton.forUnit.dart)
                    SE.RemoveSpeedUpStackList(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.javelin)
                    SE.RemoveSpeedUpStackList2(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.mace)
                    SE.RemoveSpeedUpStackList3(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.gladius)
                    SE.RemoveSpeedUpStackList4(this.gameObject);
                SE.ChangeStackSpeedUp(stack, ammount1, ammount2);
                break;
            case EventType.BuildingCreationSpeed:
                EM.RemoveListAction(BuildSpeed);
                SE.RemoveBuildSpeedStackList(this.gameObject);
                SE.ChangeStackBuildSpeed(stack, ammount1);
                break;
            case EventType.MaxHP:
                EM.RemoveListAction(MaxHP);
                if (unitToUpgrade == BuildingButton.forUnit.dart)
                    SE.RemoveMaxHpStackList(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.javelin)
                    SE.RemoveMaxHpStackList2(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.mace)
                    SE.RemoveMaxHpStackList3(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.gladius)
                    SE.RemoveMaxHpStackList4(this.gameObject);
                SE.ChangeStackMaxHp(stack, ammount1);
                break;
            case EventType.Armor:
                EM.RemoveListAction(Armor);
                if (unitToUpgrade == BuildingButton.forUnit.dart)
                    SE.RemoveArmorStackList(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.javelin)
                    SE.RemoveArmorStackList2(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.mace)
                    SE.RemoveArmorStackList3(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.gladius)
                    SE.RemoveArmorStackList4(this.gameObject);
                SE.ChangeStackArmor(stack, ammount1, ammount2);
                break;
            case EventType.Range:
                EM.RemoveListAction(Range);
                if (unitToUpgrade == BuildingButton.forUnit.dart)
                    SE.RemoveRangeStackList(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.javelin)
                    SE.RemoveRangeStackList2(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.mace)
                    SE.RemoveRangeStackList3(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.gladius)
                    SE.RemoveRangeStackList4(this.gameObject);
                SE.ChangeStackRange(stack, ammount1, ammount2);
                SE.SpeedDebuff(EM.newMoveSpeed1, debuff);
                break;
            case EventType.Caliber:
                EM.RemoveListAction(Caliber);
                if (unitToUpgrade == BuildingButton.forUnit.dart)
                    SE.RemoveCaliberStackList(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.javelin)
                    SE.RemoveCaliberStackList2(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.mace)
                    SE.RemoveCaliberStackList3(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.gladius)
                    SE.RemoveCaliberStackList4(this.gameObject);
                SE.ChangeStackCaliber(stack, ammount1);
                SE.ReloadDebuff(EM.newReload1, debuff);
                break;
            case EventType.Riot:
                EM.RemoveListAction(Riot);
                if (unitToUpgrade == BuildingButton.forUnit.dart)
                    SE.RemoveRiotStackList(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.javelin)
                    SE.RemoveRiotStackList2(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.mace)
                    SE.RemoveRiotStackList3(this.gameObject);
                if (unitToUpgrade == BuildingButton.forUnit.gladius)
                    SE.RemoveRiotStackList4(this.gameObject);
                SE.ChangeStackRiot(stack, debuff);
                SE.ReloadBuff(EM.newReload1, ammount1);
                break;
            case EventType.MaceUnit:
                UnitaUnLook(false, false, false, false);
                break;
            case EventType.DartUnit:
                UnitaUnLook(false, false, false, false);
                break;
            case EventType.GladiusUnit:
                UnitaUnLook(false, false, false, false);
                break;
            case EventType.JavUnit:
                UnitaUnLook(false, false, false, true);
                break;
            default:
                break;
        }


    }
    #endregion

}
