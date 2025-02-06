using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{

    public float second, miun;

    [Header("TypeBuildingEffect")]
    public GameObject TypeBuildingEffect;

    [Header("TimerSpawner")]
    public float sec;
    public float min;



    void Start()
    {

    }

    void Update()
    {
        Timer();
       
    }




    void Timer()
    {
        if (second < 60)
        {
            second += 1 * Time.deltaTime;
        }
        else if (second >= 60)
        {
            second = 0;
            miun += 1;
            Debug.Log(miun + " min");
        }
        
        if (miun >= min && second >= sec)
        {

            Instantiate(TypeBuildingEffect, transform.position, Quaternion.identity);

            second = 0;
            miun = 0;

            Destroy(this);
        }
    }


   
}
