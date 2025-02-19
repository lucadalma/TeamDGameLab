using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackEvent : MonoBehaviour
{

    List<GameObject> HPStack = new List<GameObject>();
    public float stackHP;

    List<GameObject> SpeedUpStack = new List<GameObject>();



    #region HPStack

    public void RemoveAddStackHp(GameObject Addobj, GameObject Remobj)
    {

        HPStack.Add(Addobj);
        HPStack.Remove(Remobj);

    }

    public float AddStackHp(float stack, float ammount)
    {


        foreach (var item in HPStack)
        {
            stack += ammount;
        }

     
        return stack;


    }
    #endregion



    #region SpeedUpStack
    public void AddStackSpeedUp(GameObject obj, float stack, float ammount)
    {
        SpeedUpStack.Add(obj);

        for (int i = 0; i < HPStack.Count; i++)
        {
            stack += ammount;
        }
    }

    public void RemoveSpeedUpStack(GameObject Addobj, GameObject Remobj)
    {
        SpeedUpStack.Add(Addobj);
        SpeedUpStack.Remove(Remobj);
    }

    #endregion


}
