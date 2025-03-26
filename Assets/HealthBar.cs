using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public UnitData unitData;
    public Slider healthSlider;
    public float maxhealth;
    public float health;
    private float armor;

    EventManager EM;

    // Start is called before the first frame update
    void Start()
    {
        if(EM == null)
            EM = FindObjectOfType<EventManager>();

        PowerUp();

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

    }

    public void TakeDamage(float damage)
    {
        damage -= armor;

        health -= damage;
    
    }

    void PowerUp()
    {
        maxhealth += EM.newHp1;
        health += EM.newHPReg1;
        armor += EM.newArmor1;
    }
   
        
    

}
