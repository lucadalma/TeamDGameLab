using System;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Salute massima della base
    public float health = 100f; // Salute corrente della base

    [SerializeField]
    BaseScript Base;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Base {gameObject.name} ha subito {damage} danni. Salute rimanente: {health}");

        if (health <= 0)
        {
            Die();
        }

        for (int i = 0; i < Base.hpThresholds.Count; i++)
        {
            if (health <= Base.hpThresholds[i] && i > Base.lastThresholdIndex)
            {
                Base.StartRetreat(i);
                Base.lastThresholdIndex = i;
                break;
            }
        }
    }

    private void Die()
    {
        Debug.Log($"Base {gameObject.name} è stata distrutta!");
        // Implementa la logica per la fine della partita o la distruzione della base
        Destroy(gameObject);
    }
}