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
        if (eventType == EventType.HPRegen)
            EM.AddListAction(HPRegeneration);
    }



    public void HPRegeneration()
    {
        EM.newHP = 1 * Time.deltaTime;
    }
}
