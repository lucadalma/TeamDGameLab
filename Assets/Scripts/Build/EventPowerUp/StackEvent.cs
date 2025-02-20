using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackEvent : MonoBehaviour
{
    #region HPStack
    List<GameObject> HPStack = new List<GameObject>();
    float stackHP;

    public void RemoveAddStackHpList(GameObject Addobj, GameObject Remobj)
    {

        HPStack.Add(Addobj);
        HPStack.Remove(Remobj);

    }

    public float AddStackHp(float stack, float ammount)
    {
        if (HPStack.Count > stackHP)
        {
            stack += ammount;
            stackHP++;
        }

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

    public float ChangeStackSpeedUp(float stack, float ammount)
    {
        stack = SpeedUpStack.Count * ammount;
       
        return stack;
    }
    #endregion



    List<GameObject> BuildigSpeedStack = new List<GameObject>();
    float stackBuildSpeed;

    public void RemoveAddBuildSpeedList(GameObject Addobj, GameObject Remobj)
    {
        BuildigSpeedStack.Add(Addobj);
        BuildigSpeedStack.Remove(Remobj);
    }


    public float AddStackBuildSpeed(float stack, float ammount)
    {
        if (BuildigSpeedStack.Count > stackSpeed)
        {
            stack += ammount;
            stackBuildSpeed++;
        }

        return stack;
    }





}
