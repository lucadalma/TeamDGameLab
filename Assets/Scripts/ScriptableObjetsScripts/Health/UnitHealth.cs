using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour
{
    public FloatVariable HP;

    public bool ResetHP;

    public FloatReference StartingHP;
    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;

    private void Start()
    {
        if (ResetHP)
            HP.SetValue(StartingHP);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Codice dove l'unità prende danno

        //if (collision.gameObject.name == "Enemy") 
        //{
        //    DamageEvent.Invoke();
        //}
    }
}
