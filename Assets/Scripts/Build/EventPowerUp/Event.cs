using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    None,
    HPRegen,
    Speed
};


public class Event : MonoBehaviour
{
    EventManager EM;


    public EventType eventType;


    private void Start()
    {
        if (EM == null)
            EM = FindObjectOfType<EventManager>();
    }


    void Update()
    {

        SwitcAbility();

    }



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
                break;
            default:
                break;
        }
    }



    public void HPRegeneration()
    {
        EM.newHP = 1 * Time.deltaTime;
    }
}
