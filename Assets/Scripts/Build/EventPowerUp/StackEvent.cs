using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.tvOS;

public class StackEvent : MonoBehaviour
{
    #region HPStack
    List<GameObject> HPStack = new List<GameObject>();

    public void AddStackHpList(GameObject Addobj)
    {
        HPStack.Add(Addobj);

    }


    public void RemoveStackHpList(GameObject obj)
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

        if( SpeedUpStack.Count == 1)
            stack = ammount1 * SpeedUpStack.Count;
        if (SpeedUpStack.Count >= 2)
            stack = ammount2 * (SpeedUpStack.Count + ammount1);


        return stack;
    }
    #endregion

    #region BuildSpeed
    List<GameObject> BuildigSpeedStack = new List<GameObject>();

    public void AddBuildSpeedList(GameObject Addobj)
    {
        BuildigSpeedStack.Add(Addobj);

    }

    public void RemoveBuildSpeedList(GameObject obj)
    {
        BuildigSpeedStack.Remove(obj);
    }

    public float ChaneStackBuildSpeed(float stack, float ammount)
    {

        stack = BuildigSpeedStack.Count * ammount;

        return stack;
    }
    #endregion




}
