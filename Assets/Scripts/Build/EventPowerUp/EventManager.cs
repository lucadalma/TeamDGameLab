using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public List<Action> actions = new List<Action>();


    public float newMoveSpeed;
    public float newHP;


    private void Update()
    {
        UsedPowerUp();
    }



    public void AddListAction(Action action)
    {
        actions.Add(action);
    }


    public void UsedPowerUp()
    {
        foreach (var evento in actions)
        {
            evento.Invoke();
        }
    }

}
