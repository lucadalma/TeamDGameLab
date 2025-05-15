using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ABILITY
{
    Missile,
    Overdrive,
    EMP

}

public class EventAbility : MonoBehaviour
{
    AbilityManager manager;


    public ABILITY Ability;
    public GameObject Missile;
    public GameObject Overdrive;
    public GameObject EMP;

    [HideInInspector] public bool mis, over, emp;

    private void Update()
    {
        if(manager != null)
        {
            manager = FindObjectOfType<AbilityManager>();
        }


        Switch();
    }

    public void Switch()
    {
        switch (Ability)
        {
            case ABILITY.Missile:
                manager.abilit� = Missile;
                break;
            case ABILITY.Overdrive:
                manager.abilit� = Overdrive;
                break;
            case ABILITY.EMP:
                manager.abilit� = EMP;
                break;
            default:
                break;
        }
    }
}
