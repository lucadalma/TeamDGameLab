using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public float maxhealth = 100;
    public float health;
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;  
    }

    private void Update()
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

    private void TakeDamage(float damage)
    {
        health -= damage;
    }
        
    

}
