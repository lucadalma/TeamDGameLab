using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.tvOS;

public class StackEvent : MonoBehaviour
{
    #region HPStack
    List<GameObject> HPStack = new List<GameObject>();

    public void AddHpStackList(GameObject Addobj)
    {
        HPStack.Add(Addobj);

    }


    public void RemoveHpStackList(GameObject obj)
    {
        HPStack.Remove(obj);
    }

    public float ChangeStackHp(float stack, float ammount1, float ammount2)
    {
        if (HPStack.Count <= 1)
            stack = HPStack.Count * ammount1;
        else if (HPStack.Count >= 2)
            stack = (HPStack.Count + ammount1) * ammount2;

        return stack;
    }

    #endregion

    #region SpeedUpStack
    List<GameObject> SpeedUpStack = new List<GameObject>();
 

    public void AddSpeedUpStackList(GameObject Addobj)
    {
        SpeedUpStack.Add(Addobj);

    }

    public void RemoveSpeedUpStackList(GameObject Remobj)
    {
        SpeedUpStack.Remove(Remobj);
    }

    public float ChangeStackSpeedUp(float stack, float ammount1, float ammount2)
    {

        if (SpeedUpStack.Count <= 1)
        {
            float debuff = RangeStack.Count * (-0.5f);
            stack = (ammount1 + debuff) * SpeedUpStack.Count;
        }
        if (SpeedUpStack.Count >= 2)
        {
            float debuff = (RangeStack.Count + 1) * (-0.5f);
            stack = ammount2 * (SpeedUpStack.Count + ammount1 + (debuff));
        }





        return stack;
    }
    #endregion

    #region BuildSpeed
    List<GameObject> BuildigSpeedStack = new List<GameObject>();

    public void AddBuildSpeedStackList(GameObject Addobj)
    {
        BuildigSpeedStack.Add(Addobj);

    }

    public void RemoveBuildSpeedStackList(GameObject obj)
    {
        BuildigSpeedStack.Remove(obj);
    }

    public float ChangeStackBuildSpeed(float stack, float ammount)
    {

        stack = BuildigSpeedStack.Count * ammount;

        return stack;
    }
    #endregion

    #region NewHPRegen

    List<GameObject> MaxHPStack = new List<GameObject>();

    public void AddMaxHpStackList(GameObject Addobj)
    {
        MaxHPStack.Add(Addobj);

    }


    public void RemoveMaxHpStackList(GameObject obj)
    {
        MaxHPStack.Remove(obj);
    }

    public float ChangeStackMaxHp(float stack, float ammount1)
    {
        stack = (MaxHPStack.Count * ammount1 / 100);

        return stack;
    }



    #endregion

    #region StackArmor

    List<GameObject> ArmorStack = new List<GameObject>();

    public void AddArmorStackList(GameObject Addobj)
    {
        ArmorStack.Add(Addobj);

    }


    public void RemoveArmorStackList(GameObject obj)
    {
        ArmorStack.Remove(obj);
    }

    public float ChangeStackArmor(float stack, float ammount1, float ammount2)
    {
        if (ArmorStack.Count <= 1)
            stack = ArmorStack.Count * ammount1;
        if (ArmorStack.Count >= 2)
            stack = (ArmorStack.Count * (ammount1 / 2) + ammount2);

        return stack;
    }

    #endregion

    #region StackRange


    List<GameObject> RangeStack = new List<GameObject>();

    public void AddRangeStackList(GameObject Addobj)
    {
        RangeStack.Add(Addobj);

    }


    public void RemoveRangeStackList(GameObject obj)
    {
        RangeStack.Remove(obj);
    }

    public float ChangeStackRange(float stack, float ammount1, float ammount2)
    {
        if (RangeStack.Count <= 1)
            stack = RangeStack.Count * ((ammount1 / 100) + 4);



        if (RangeStack.Count >= 2)
            stack = (RangeStack.Count * ammount2) + 0.25f;


        return stack;
    }

    public float SpeedDebuff(float speed, float debuff)
    {
        speed = RangeStack.Count * (-debuff);

        if (speed < 0)
            speed = 0;

        return speed;
    }


    #endregion

    #region StackCaliber

    List<GameObject> CaliberStack = new List<GameObject>();

    public void AddCaliberStackList(GameObject Addobj)
    {
        CaliberStack.Add(Addobj);

    }


    public void RemoveCaliberStackList(GameObject obj)
    {
        CaliberStack.Remove(obj);
    }

    public float ChangeStackCaliber(float stack, float ammount1)
    {


        if (CaliberStack.Count <= 1)
        {
            float debuff = RiotStack.Count * (-0.5f);
            stack = (CaliberStack.Count * ((ammount1 / 100) + 6)) + debuff;
        }
        if (CaliberStack.Count >= 2)
        {
            float debuff = RiotStack.Count * (-0.5f);
            stack = (CaliberStack.Count + 24) * (ammount1 / 100) + debuff;
        }




        return stack;
    }

    public float ReloadDebuff(float stack, float debuff)
    {
        float debuffR = RiotStack.Count * (-0.5f);

        stack = (CaliberStack.Count * (debuff / 100)) + debuffR;

        return stack;
    }

    #endregion

    #region StackRiot


    List<GameObject> RiotStack = new List<GameObject>();

    public void AddRiotStackList(GameObject Addobj)
    {
        RiotStack.Add(Addobj);
    }


    public void RemoveRiotStackList(GameObject obj)
    {
        RiotStack.Remove(obj);
    }

    public float ChangeStackRiot(float stack, float debuff)
    {

        if (CaliberStack.Count <= 0)
            stack = RiotStack.Count * (-debuff / 100);
        else if (CaliberStack.Count >= 1)
            stack = ChangeStackCaliber(stack, debuff / 2);

        return stack;

    }


    public float ReloadBuff(float stack, float ammount)
    {
        if (CaliberStack.Count <= 0)
            stack = RiotStack.Count * (-ammount / 100);
        else if (CaliberStack.Count >= 1)
            stack = ChangeStackCaliber(stack, ammount - 40);

        return stack;
    }

    #endregion

}
