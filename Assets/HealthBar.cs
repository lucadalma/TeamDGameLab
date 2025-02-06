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
        if(healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);

        }
       
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    

    }

   
        
    

}
