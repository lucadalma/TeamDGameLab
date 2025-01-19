using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    int posiblePages;
    int currentPage = 0;

    [NonSerialized] public int unitsOnTimer;
    [NonSerialized] public int unitsOnDeploy;

    [SerializeField] List<GameObject> availableUnitsToCreate;
    List<GameObject> availableUnitsToDeploy = new List<GameObject>();

    [SerializeField] List<Transform> creationSelectorSlots;
    [SerializeField] List<Transform> deploimentSelectorSlots;
    [SerializeField] List<Transform> timerSlots;

    // Start is called before the first frame update
    void Start()
    {
        if (availableUnitsToCreate.Count % 6 == 0)
        {
            posiblePages = (availableUnitsToCreate.Count / 6) - 1;
        }
        else
        {
            posiblePages = availableUnitsToCreate.Count / 6;
        }




        SetAvailableUnits();
    }

    // Update is called once per frame
    void Update()
    {

    }



    void SetAvailableUnits()
    {
        if (availableUnitsToCreate.Count <= 6)
        {
            for (int i = 0; i < availableUnitsToCreate.Count; i++)
            {
                Instantiate(availableUnitsToCreate[i], creationSelectorSlots[i]);
            }
        }
        else
        {
            if (currentPage != posiblePages)
            {
                for (int i = 0; i < 6; i++)
                {
                    Instantiate(availableUnitsToCreate[i + currentPage * 6], creationSelectorSlots[i]);
                }
            }
            else
            {
                for (int i = 0; i < availableUnitsToCreate.Count - currentPage * 6; i++)
                {
                    Instantiate(availableUnitsToCreate[i + currentPage * 6], creationSelectorSlots[i]);
                }
            }

        }

    }

    void clearAvailableUnits()
    {
        foreach (var slot in creationSelectorSlots)
        {
            if (slot.childCount > 0)
            {
                GameObject unit = slot.GetChild(0).gameObject;
                Destroy(unit);
            }
        }
    }


    public void nextPage()
    {
        if (currentPage == posiblePages)
        {
            currentPage = 0;
        }
        else
        {
            currentPage++;
        }

        clearAvailableUnits();
        SetAvailableUnits();
    }

    public void previousPage()
    {
        if (currentPage == 0)
        {
            currentPage = posiblePages;
        }
        else
        {
            currentPage--;
        }

        clearAvailableUnits();
        SetAvailableUnits();
    }


    public void setUnitOnTimer(GameObject unit)
    {
        if (unitsOnTimer + unitsOnDeploy < 10 && unitsOnTimer < 8)
        {
            bool Spawned = false;

            foreach (var slot in timerSlots)
            {
                if (slot.childCount == 0)
                {
                    Instantiate(unit, slot);
                    unitsOnTimer++;
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("cant create any more units");
        }

    }

    public void AddDeployUnits(GameObject unit)
    {

        Debug.Log("spawned" + unit.name);
        


        GameObject newUnit = Instantiate(unit, deploimentSelectorSlots[0]);

       availableUnitsToDeploy.Add(newUnit);

        setDeployUnits();

    }

    public void removeDeployUnits(GameObject unit)
    {
        Debug.Log("removed" + unit.name);
        availableUnitsToDeploy.Remove(unit);
        Destroy(unit);
        setDeployUnits();

    }



    void setDeployUnits()
    {
        for (int i = 0; i < availableUnitsToDeploy.Count; i++)
        {
            availableUnitsToDeploy[i].transform.SetParent(deploimentSelectorSlots[i]);
            availableUnitsToDeploy[i].transform.position = deploimentSelectorSlots[i].position;
        }
        unitsOnDeploy = availableUnitsToDeploy.Count;
    }
}
