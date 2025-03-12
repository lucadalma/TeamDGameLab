using System.Collections;
using System.Collections.Generic;
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
        if (HPStack.Count == 1)
            stack = HPStack.Count * ammount1;
        else if (HPStack.Count >= 2)
            stack = (HPStack.Count + ammount1) * ammount2;

        return stack;
    }

    #endregion


    #region SpeedUpStack
    List<GameObject> SpeedUpStack = new List<GameObject>();
    float stackSpeed;

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

        if (SpeedUpStack.Count == 1)
            stack = ammount1 * SpeedUpStack.Count;
        if (SpeedUpStack.Count >= 2)
            stack = ammount2 * (SpeedUpStack.Count + ammount1);


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
        stack = MaxHPStack.Count * ammount1;

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
        if (ArmorStack.Count == 1)
            stack = ArmorStack.Count * ammount1;
        if(ArmorStack.Count >= 2)
            stack = (ArmorStack.Count * (ammount1/2) + ammount2);

        return stack;
    }


    #endregion
}
