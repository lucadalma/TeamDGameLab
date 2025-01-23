using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    int posiblePages;
    int currentPage = 0;

    [NonSerialized] public int numberOfUnitsOnTimer;
    [NonSerialized] public int numberOfUnitsOnDeploy;

    [SerializeField] List<GameObject> availableUnitsToCreate;


    List<GameObject> availableUnitsToDeploy = new List<GameObject>();
    List<GameObject> unitsOnTimer = new List<GameObject>();


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


    public void setUnitOnTimer()
    {
        if (unitsOnTimer.Count > 0)
        {
            for (int i = 0; i < unitsOnTimer.Count; i++)
            {
                unitsOnTimer[i].transform.SetParent(timerSlots[i]);
                unitsOnTimer[i].transform.position = timerSlots[i].position;
                if (i == 0)
                {
                    unitsOnTimer[i].GetComponent<CreationTimer>().first = true;
                }
            }
        }

        numberOfUnitsOnTimer = unitsOnTimer.Count;

    }

    public void addUnitOnTimer(GameObject unit)
    {
        if (numberOfUnitsOnTimer < 4 && numberOfUnitsOnTimer + numberOfUnitsOnDeploy < 10)
        {
            Debug.Log("now on timer" + unit.name);

            GameObject newUnit = Instantiate(unit, timerSlots[0]);

            unitsOnTimer.Add(newUnit);
            setUnitOnTimer();
        }
        else
        {
            Debug.LogWarning("no space for unit to be created");
        }



    }


    public void remopveUnitOnTimer(GameObject unit)
    {
        Debug.Log("removed" + unit.name);
        unitsOnTimer.Remove(unit);
        Destroy(unit);
        setUnitOnTimer();
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

       availableUnitsToDeploy = availableUnitsToDeploy.OrderBy(item => item.GetComponent<unitDeployButton>().unitID).ToList();

        for (int i = 0; i < availableUnitsToDeploy.Count; i++)
        {
            availableUnitsToDeploy[i].transform.SetParent(deploimentSelectorSlots[i]);
            availableUnitsToDeploy[i].transform.position = deploimentSelectorSlots[i].position;
        }
        numberOfUnitsOnDeploy = availableUnitsToDeploy.Count;
    }
}
