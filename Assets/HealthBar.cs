using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public UnitData unitData;
    public Slider healthSlider;
    public float maxhealth;
    public float health;
    
    

    // Start is called before the first frame update
    void Start()
    {
        unitData.health = maxhealth;

        health = maxhealth;  
    }

    public void Update()
    {
        // Mantiene il valore della health aggiornata sulla barra
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        // Rotazione per far sempre guardare la healthbar verso la camera
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0); // Inverte la rotazione per essere correttamente visibile

        // Test: Premi Spazio per togliere 10 HP
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    

    }

   
        
    

}
