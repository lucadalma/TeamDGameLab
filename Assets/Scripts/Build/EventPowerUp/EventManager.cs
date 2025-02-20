using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public List<Action> actions = new List<Action>();

    //[HideInInspector]
    public float newHP;
    [HideInInspector]
    public float newMoveSpeed;
    [HideInInspector]
    public bool red, green, blue;


    private void Update()
    {
        UsedPowerUp();
    }





    public void AddListAction(Action action)
    {
        actions.Add(action);
    }
    public void RemoveListAction(Action action)
    {
        actions.Remove(action);
    }






    public void UsedPowerUp()
    {
        foreach (var evento in actions)
        {
            evento.Invoke();
        }
    }


}
