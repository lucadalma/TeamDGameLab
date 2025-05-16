using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbilityManager : MonoBehaviour
{
    /*[HideInInspector]*/
    public GameObject abilit�;
    public LayerMask laneLayerMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (abilit� != null)
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
                Instantiate(abilit�, hit.point, Quaternion.identity);
            }
        }
       


    }
}
}
