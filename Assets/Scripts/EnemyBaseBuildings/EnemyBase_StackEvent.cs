using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase_StackEvent : MonoBehaviour
{
    #region HPStack
    List<GameObject> HPStack = new List<GameObject>();
    List<GameObject> HPStack2 = new List<GameObject>();
    List<GameObject> HPStack3 = new List<GameObject>();
    List<GameObject> HPStack4 = new List<GameObject>();

    public void AddHpStackList(GameObject Addobj)
    {
        HPStack.Add(Addobj);
    }
    public void AddHpStackList2(GameObject Addobj)
    {
        HPStack2.Add(Addobj);
    }
    public void AddHpStackList3(GameObject Addobj)
    {
        HPStack3.Add(Addobj);
    }
    public void AddHpStackList4(GameObject Addobj)
    {
        HPStack4.Add(Addobj);
    }
    ////////
    public void RemoveHpStackList(GameObject obj)
    {
        HPStack.Remove(obj);
    }
    public void RemoveHpStackList2(GameObject obj)
    {
        HPStack2.Remove(obj);
    }
    public void RemoveHpStackList3(GameObject obj)
    {
        HPStack3.Remove(obj);
    }
    public void RemoveHpStackList4(GameObject obj)
    {
        HPStack4.Remove(obj);
    }

    public float ChangeStackHp(float stack, float ammount1, float ammount2)
    {
        if (HPStack.Count <= 1)
            stack = HPStack.Count * ammount1;
        else if (HPStack.Count >= 2)
            stack = (HPStack.Count + ammount1) * ammount2;

        return stack;
    }

    public float ChangeStackHp2(float stack, float ammount1, float ammount2)
    {


        if (HPStack2.Count <= 1)
            stack = HPStack2.Count * ammount1;
        else if (HPStack2.Count >= 2)
            stack = (HPStack2.Count + ammount1) * ammount2;

        return stack;
    }

    public float ChangeStackHp3(float stack, float ammount1, float ammount2)
    {


        if (HPStack3.Count <= 1)
            stack = HPStack3.Count * ammount1;
        else if (HPStack3.Count >= 2)
            stack = (HPStack3.Count + ammount1) * ammount2;

        return stack;
    }

    public float ChangeStackHp4(float stack, float ammount1, float ammount2)
    {


        if (HPStack4.Count <= 1)
            stack = HPStack4.Count * ammount1;
        else if (HPStack4.Count >= 2)
            stack = (HPStack4.Count + ammount1) * ammount2;

        return stack;
    }

    #endregion

    #region SpeedUpStack
    List<GameObject> SpeedUpStack = new List<GameObject>();
    List<GameObject> SpeedUpStack2 = new List<GameObject>();
    List<GameObject> SpeedUpStack3 = new List<GameObject>();
    List<GameObject> SpeedUpStack4 = new List<GameObject>();


    public void AddSpeedUpStackList(GameObject Addobj)
    {
        SpeedUpStack.Add(Addobj);

    }
    public void AddSpeedUpStackList2(GameObject Addobj)
    {
        SpeedUpStack2.Add(Addobj);

    }
    public void AddSpeedUpStackList3(GameObject Addobj)
    {
        SpeedUpStack3.Add(Addobj);

    }
    public void AddSpeedUpStackList4(GameObject Addobj)
    {
        SpeedUpStack4.Add(Addobj);

    }

    public void RemoveSpeedUpStackList(GameObject Remobj)
    {
        SpeedUpStack.Remove(Remobj);
    }
    public void RemoveSpeedUpStackList2(GameObject Remobj)
    {
        SpeedUpStack2.Remove(Remobj);
    }
    public void RemoveSpeedUpStackList3(GameObject Remobj)
    {
        SpeedUpStack3.Remove(Remobj);
    }
    public void RemoveSpeedUpStackList4(GameObject Remobj)
    {
        SpeedUpStack4.Remove(Remobj);
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
    public float ChangeStackSpeedUp2(float stack, float ammount1, float ammount2)
    {

        if (SpeedUpStack2.Count <= 1)
        {
            float debuff = RangeStack2.Count * (-0.5f);
            stack = (ammount1 + debuff) * SpeedUpStack2.Count;
        }
        if (SpeedUpStack2.Count >= 2)
        {
            float debuff = (RangeStack2.Count + 1) * (-0.5f);
            stack = ammount2 * (SpeedUpStack2.Count + ammount1 + (debuff));
        }





        return stack;
    }
    public float ChangeStackSpeedUp3(float stack, float ammount1, float ammount2)
    {

        if (SpeedUpStack3.Count <= 1)
        {
            float debuff = RangeStack3.Count * (-0.5f);
            stack = (ammount1 + debuff) * SpeedUpStack3.Count;
        }
        if (SpeedUpStack3.Count >= 2)
        {
            float debuff = (RangeStack3.Count + 1) * (-0.5f);
            stack = ammount2 * (SpeedUpStack3.Count + ammount1 + (debuff));
        }





        return stack;
    }
    public float ChangeStackSpeedUp4(float stack, float ammount1, float ammount2)
    {

        if (SpeedUpStack4.Count <= 1)
        {
            float debuff = RangeStack4.Count * (-0.5f);
            stack = (ammount1 + debuff) * SpeedUpStack4.Count;
        }
        if (SpeedUpStack.Count >= 2)
        {
            float debuff = (RangeStack4.Count + 1) * (-0.5f);
            stack = ammount2 * (SpeedUpStack4.Count + ammount1 + (debuff));
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

    #region NewHPMax

    List<GameObject> MaxHPStack = new List<GameObject>();
    List<GameObject> MaxHPStack2 = new List<GameObject>();
    List<GameObject> MaxHPStack3 = new List<GameObject>();
    List<GameObject> MaxHPStack4 = new List<GameObject>();

    public void AddMaxHpStackList(GameObject Addobj)
    {
        MaxHPStack.Add(Addobj);

    }
    public void AddMaxHpStackList2(GameObject Addobj)
    {
        MaxHPStack2.Add(Addobj);

    }
    public void AddMaxHpStackList3(GameObject Addobj)
    {
        MaxHPStack3.Add(Addobj);

    }
    public void AddMaxHpStackList4(GameObject Addobj)
    {
        MaxHPStack4.Add(Addobj);

    }


    public void RemoveMaxHpStackList(GameObject obj)
    {
        MaxHPStack.Remove(obj);
    }
    public void RemoveMaxHpStackList2(GameObject obj)
    {
        MaxHPStack2.Remove(obj);
    }
    public void RemoveMaxHpStackList3(GameObject obj)
    {
        MaxHPStack3.Remove(obj);
    }
    public void RemoveMaxHpStackList4(GameObject obj)
    {
        MaxHPStack4.Remove(obj);
    }

    public float ChangeStackMaxHp(float stack, float ammount1)
    {
        stack = (MaxHPStack.Count * ammount1 / 100);

        return stack;
    }
    public float ChangeStackMaxHp2(float stack, float ammount1)
    {
        stack = (MaxHPStack2.Count * ammount1 / 100);

        return stack;
    }
    public float ChangeStackMaxHp3(float stack, float ammount1)
    {
        stack = (MaxHPStack3.Count * ammount1 / 100);

        return stack;
    }
    public float ChangeStackMaxHp4(float stack, float ammount1)
    {
        stack = (MaxHPStack4.Count * ammount1 / 100);

        return stack;
    }



    #endregion

    #region StackArmor

    List<GameObject> ArmorStack = new List<GameObject>();
    List<GameObject> ArmorStack2 = new List<GameObject>();
    List<GameObject> ArmorStack3 = new List<GameObject>();
    List<GameObject> ArmorStack4 = new List<GameObject>();

    public void AddArmorStackList(GameObject Addobj)
    {
        ArmorStack.Add(Addobj);

    }
    public void AddArmorStackList2(GameObject Addobj)
    {
        ArmorStack2.Add(Addobj);

    }
    public void AddArmorStackList3(GameObject Addobj)
    {
        ArmorStack3.Add(Addobj);

    }
    public void AddArmorStackList4(GameObject Addobj)
    {
        ArmorStack4.Add(Addobj);

    }


    public void RemoveArmorStackList(GameObject obj)
    {
        ArmorStack.Remove(obj);
    }
    public void RemoveArmorStackList2(GameObject obj)
    {
        ArmorStack2.Remove(obj);
    }
    public void RemoveArmorStackList3(GameObject obj)
    {
        ArmorStack3.Remove(obj);
    }
    public void RemoveArmorStackList4(GameObject obj)
    {
        ArmorStack4.Remove(obj);
    }

    public float ChangeStackArmor(float stack, float ammount1, float ammount2)
    {
        if (ArmorStack.Count <= 1)
            stack = ArmorStack.Count * ammount1;
        if (ArmorStack.Count >= 2)
            stack = (ArmorStack.Count * (ammount1 / 2) + ammount2);

        return stack;
    }
    public float ChangeStackArmor2(float stack, float ammount1, float ammount2)
    {
        if (ArmorStack2.Count <= 1)
            stack = ArmorStack2.Count * ammount1;
        if (ArmorStack2.Count >= 2)
            stack = (ArmorStack2.Count * (ammount1 / 2) + ammount2);

        return stack;
    }
    public float ChangeStackArmor3(float stack, float ammount1, float ammount2)
    {
        if (ArmorStack3.Count <= 1)
            stack = ArmorStack3.Count * ammount1;
        if (ArmorStack3.Count >= 2)
            stack = (ArmorStack3.Count * (ammount1 / 2) + ammount2);

        return stack;
    }
    public float ChangeStackArmor4(float stack, float ammount1, float ammount2)
    {
        if (ArmorStack4.Count <= 1)
            stack = ArmorStack4.Count * ammount1;
        if (ArmorStack4.Count >= 2)
            stack = (ArmorStack4.Count * (ammount1 / 2) + ammount2);

        return stack;
    }

    #endregion

    #region StackRange


    List<GameObject> RangeStack = new List<GameObject>();
    List<GameObject> RangeStack2 = new List<GameObject>();
    List<GameObject> RangeStack3 = new List<GameObject>();
    List<GameObject> RangeStack4 = new List<GameObject>();

    public void AddRangeStackList(GameObject Addobj)
    {
        RangeStack.Add(Addobj);

    }
    public void AddRangeStackList2(GameObject Addobj)
    {
        RangeStack2.Add(Addobj);

    }
    public void AddRangeStackList3(GameObject Addobj)
    {
        RangeStack3.Add(Addobj);

    }
    public void AddRangeStackList4(GameObject Addobj)
    {
        RangeStack4.Add(Addobj);

    }


    public void RemoveRangeStackList(GameObject obj)
    {
        RangeStack.Remove(obj);
    }
    public void RemoveRangeStackList2(GameObject obj)
    {
        RangeStack2.Remove(obj);
    }
    public void RemoveRangeStackList3(GameObject obj)
    {
        RangeStack3.Remove(obj);
    }
    public void RemoveRangeStackList4(GameObject obj)
    {
        RangeStack4.Remove(obj);
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
    public float ChangeStackRange2(float stack, float ammount1, float ammount2)
    {
        if (RangeStack2.Count <= 1)
            stack = RangeStack2.Count * ((ammount1 / 100) + 4);



        if (RangeStack2.Count >= 2)
            stack = (RangeStack2.Count * ammount2) + 0.25f;


        return stack;
    }

    public float SpeedDebuff2(float speed, float debuff)
    {
        speed = RangeStack2.Count * (-debuff);

        if (speed < 0)
            speed = 0;

        return speed;
    }
    public float ChangeStackRange3(float stack, float ammount1, float ammount2)
    {
        if (RangeStack3.Count <= 1)
            stack = RangeStack3.Count * ((ammount1 / 100) + 4);



        if (RangeStack3.Count >= 2)
            stack = (RangeStack3.Count * ammount2) + 0.25f;


        return stack;
    }

    public float SpeedDebuff3(float speed, float debuff)
    {
        speed = RangeStack3.Count * (-debuff);

        if (speed < 0)
            speed = 0;

        return speed;
    }
    public float ChangeStackRange4(float stack, float ammount1, float ammount2)
    {
        if (RangeStack4.Count <= 1)
            stack = RangeStack4.Count * ((ammount1 / 100) + 4);



        if (RangeStack4.Count >= 2)
            stack = (RangeStack4.Count * ammount2) + 0.25f;


        return stack;
    }

    public float SpeedDebuff4(float speed, float debuff)
    {
        speed = RangeStack4.Count * (-debuff);

        if (speed < 0)
            speed = 0;

        return speed;
    }


    #endregion

    #region StackCaliber

    List<GameObject> CaliberStack = new List<GameObject>();
    List<GameObject> CaliberStack2 = new List<GameObject>();
    List<GameObject> CaliberStack3 = new List<GameObject>();
    List<GameObject> CaliberStack4 = new List<GameObject>();

    public void AddCaliberStackList(GameObject Addobj)
    {
        CaliberStack.Add(Addobj);

    }
    public void AddCaliberStackList2(GameObject Addobj)
    {
        CaliberStack2.Add(Addobj);

    }
    public void AddCaliberStackList3(GameObject Addobj)
    {
        CaliberStack3.Add(Addobj);

    }
    public void AddCaliberStackList4(GameObject Addobj)
    {
        CaliberStack4.Add(Addobj);

    }


    public void RemoveCaliberStackList(GameObject obj)
    {
        CaliberStack.Remove(obj);
    }
    public void RemoveCaliberStackList2(GameObject obj)
    {
        CaliberStack2.Remove(obj);
    }
    public void RemoveCaliberStackList3(GameObject obj)
    {
        CaliberStack3.Remove(obj);
    }
    public void RemoveCaliberStackList4(GameObject obj)
    {
        CaliberStack4.Remove(obj);
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
    public float ChangeStackCaliber2(float stack, float ammount1)
    {


        if (CaliberStack2.Count <= 1)
        {
            float debuff = RiotStack2.Count * (-0.5f);
            stack = (CaliberStack2.Count * ((ammount1 / 100) + 6)) + debuff;
        }
        if (CaliberStack2.Count >= 2)
        {
            float debuff = RiotStack2.Count * (-0.5f);
            stack = (CaliberStack2.Count + 24) * (ammount1 / 100) + debuff;
        }




        return stack;
    }

    public float ReloadDebuff2(float stack, float debuff)
    {
        float debuffR = RiotStack2.Count * (-0.5f);

        stack = (CaliberStack2.Count * (debuff / 100)) + debuffR;

        return stack;
    }
    public float ChangeStackCaliber3(float stack, float ammount1)
    {


        if (CaliberStack3.Count <= 1)
        {
            float debuff = RiotStack3.Count * (-0.5f);
            stack = (CaliberStack3.Count * ((ammount1 / 100) + 6)) + debuff;
        }
        if (CaliberStack3.Count >= 2)
        {
            float debuff = RiotStack3.Count * (-0.5f);
            stack = (CaliberStack3.Count + 24) * (ammount1 / 100) + debuff;
        }




        return stack;
    }

    public float ReloadDebuff3(float stack, float debuff)
    {
        float debuffR = RiotStack3.Count * (-0.5f);

        stack = (CaliberStack3.Count * (debuff / 100)) + debuffR;

        return stack;
    }
    public float ChangeStackCaliber4(float stack, float ammount1)
    {


        if (CaliberStack4.Count <= 1)
        {
            float debuff = RiotStack4.Count * (-0.5f);
            stack = (CaliberStack4.Count * ((ammount1 / 100) + 6)) + debuff;
        }
        if (CaliberStack4.Count >= 2)
        {
            float debuff = RiotStack4.Count * (-0.5f);
            stack = (CaliberStack4.Count + 24) * (ammount1 / 100) + debuff;
        }




        return stack;
    }

    public float ReloadDebuff4(float stack, float debuff)
    {
        float debuffR = RiotStack4.Count * (-0.5f);

        stack = (CaliberStack4.Count * (debuff / 100)) + debuffR;

        return stack;
    }

    #endregion

    #region StackRiot


    List<GameObject> RiotStack = new List<GameObject>();
    List<GameObject> RiotStack2 = new List<GameObject>();
    List<GameObject> RiotStack3 = new List<GameObject>();
    List<GameObject> RiotStack4 = new List<GameObject>();

    public void AddRiotStackList(GameObject Addobj)
    {
        RiotStack.Add(Addobj);
    }
    public void AddRiotStackList2(GameObject Addobj)
    {
        RiotStack2.Add(Addobj);
    }
    public void AddRiotStackList3(GameObject Addobj)
    {
        RiotStack3.Add(Addobj);
    }
    public void AddRiotStackList4(GameObject Addobj)
    {
        RiotStack4.Add(Addobj);
    }


    public void RemoveRiotStackList(GameObject obj)
    {
        RiotStack.Remove(obj);
    }
    public void RemoveRiotStackList2(GameObject obj)
    {
        RiotStack2.Remove(obj);
    }
    public void RemoveRiotStackList3(GameObject obj)
    {
        RiotStack3.Remove(obj);
    }
    public void RemoveRiotStackList4(GameObject obj)
    {
        RiotStack4.Remove(obj);
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
    public float ChangeStackRiot2(float stack, float debuff)
    {

        if (CaliberStack2.Count <= 0)
            stack = RiotStack2.Count * (-debuff / 100);
        else if (CaliberStack2.Count >= 1)
            stack = ChangeStackCaliber2(stack, debuff / 2);

        return stack;

    }


    public float ReloadBuff2(float stack, float ammount)
    {
        if (CaliberStack2.Count <= 0)
            stack = RiotStack2.Count * (-ammount / 100);
        else if (CaliberStack2.Count >= 1)
            stack = ChangeStackCaliber2(stack, ammount - 40);

        return stack;
    }
    public float ChangeStackRiot3(float stack, float debuff)
    {

        if (CaliberStack3.Count <= 0)
            stack = RiotStack3.Count * (-debuff / 100);
        else if (CaliberStack3.Count >= 1)
            stack = ChangeStackCaliber3(stack, debuff / 2);

        return stack;

    }


    public float ReloadBuff3(float stack, float ammount)
    {
        if (CaliberStack3.Count <= 0)
            stack = RiotStack3.Count * (-ammount / 100);
        else if (CaliberStack3.Count >= 1)
            stack = ChangeStackCaliber3(stack, ammount - 40);

        return stack;
    }
    public float ChangeStackRiot4(float stack, float debuff)
    {

        if (CaliberStack4.Count <= 0)
            stack = RiotStack4.Count * (-debuff / 100);
        else if (CaliberStack4.Count >= 1)
            stack = ChangeStackCaliber4(stack, debuff / 2);

        return stack;

    }


    public float ReloadBuff4(float stack, float ammount)
    {
        if (CaliberStack4.Count <= 0)
            stack = RiotStack4.Count * (-ammount / 100);
        else if (CaliberStack4.Count >= 1)
            stack = ChangeStackCaliber4(stack, ammount - 40);

        return stack;
    }

    #endregion
}
