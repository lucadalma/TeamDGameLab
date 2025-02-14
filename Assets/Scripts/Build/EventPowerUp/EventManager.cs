using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public List<Action> actions = new List<Action>();

    UnitBehavior unitBehavior;

    public float newMoveSpeed;
    public float newHP;


    private void Update()
    {
        UsedPowerUp();

        if(Input.GetKeyDown(KeyCode.H))
        {
            newHP += 100 * Time.deltaTime;
        }
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
