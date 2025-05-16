using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbilityManager : MonoBehaviour
{
    /*[HideInInspector]*/
    public GameObject abilità;
    public LayerMask laneLayerMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (abilità != null)
                SpawnAbilityAtMouseClick();
        }
    }


    float timer;
    int stop;

    void SpawnAbilityAtMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, laneLayerMask))
            {
                Instantiate(abilità, hit.point, Quaternion.identity);
            }
        }
       


    }
}
}
