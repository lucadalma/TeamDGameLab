using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbilityManager : MonoBehaviour
{
    /*[HideInInspector]*/
    public GameObject abilit�;
    public abilityButton currentButton;
    public LayerMask laneLayerMask;

    float timer;
    int stop;
    bool nulled;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (abilit� != null)
                SpawnAbilityAtMouseClick();
        }

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (nulled)
            {
                abilit� = null;
                currentButton = null;
                nulled = false;
            }
        }
    }



    void SpawnAbilityAtMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, laneLayerMask))
            {
                Instantiate(abilit�, hit.point, Quaternion.identity);
                currentButton.StartTimer();

                timer = 0.001f;
                nulled = true;
                Debug.Log("fired");
            }
            else
            {
                Debug.Log("missfired");
                abilit� = null;
                currentButton = null;
            }
        }
       


    }
}
