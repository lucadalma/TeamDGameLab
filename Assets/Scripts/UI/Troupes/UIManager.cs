using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int posiblePages;
    int currentPage = 0;

    [SerializeField] GameObject staticUI;

    [NonSerialized] public int numberOfUnitsOnTimer;
    [NonSerialized] public int numberOfUnitsOnDeploy;

    [SerializeField] List<GameObject> availableUnitsToCreate;


    List<GameObject> availableUnitsToDeploy = new List<GameObject>();
    List<GameObject> unitsOnTimer = new List<GameObject>();


    [SerializeField] List<Transform> creationSelectorSlots;
    [SerializeField] List<Transform> deploimentSelectorSlots;
    [SerializeField] List<Transform> unitTimerSlots;
    [SerializeField] List<Transform> buildingTimerSlots;

    [SerializeField] List<GameObject> availableBuildingCategories;
    [SerializeField] List<GameObject> availableBuildings;
    [SerializeField] GameObject BuildingBackButton;

    List<GameObject> buildingButtonsDysplayed = new List<GameObject>();
    List<GameObject> buildingsOnTimer = new List<GameObject>();

    [NonSerialized] public GameObject TargetBuilding;
    [SerializeField] private LayerMask buildingLayerMask;
    [SerializeField] float r;

    [NonSerialized] public int numberOfBuildingsOnTimer;
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
        searchForBuilding();
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
                unitsOnTimer[i].transform.SetParent(unitTimerSlots[i]);
                unitsOnTimer[i].transform.position = unitTimerSlots[i].position;
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

            GameObject newUnit = Instantiate(unit, unitTimerSlots[0]);

            unitsOnTimer.Add(newUnit);
            setUnitOnTimer();
        }
        else
        {
            Debug.LogWarning("no space for unit to be created");
        }



    }


    public void removeUnitOnTimer(GameObject unit)
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


    // Fuctions to disable or enable the static ui
    void turnOffInteractionsWithStaticUI(Transform parent)
    {

        foreach (Transform child in parent)
        {

            Image image = child.GetComponent<Image>();
            Button button = child.GetComponent<Button>();


            if (image != null)
            {

                image.raycastTarget = false;

            }

            if (button != null)
            {
                button.enabled = false;
            }


            if (child.childCount > 0)
            {
                turnOffInteractionsWithStaticUI(child);
            }
        }

    }

    void turnOnInteractionsWithStaticUI(Transform parent)
    {

        foreach (Transform child in parent)
        {

            Image image = child.GetComponent<Image>();
            Button button = child.GetComponent<Button>();


            if (image != null)
            {

                image.raycastTarget = true;

            }

            if (button != null)
            {
                button.enabled = true ;
            }

            if (child.childCount > 0)
            {
                turnOnInteractionsWithStaticUI(child);
            }
        }

    }



    //buildin buttons functions
    void searchForBuilding()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {


            if (Input.GetMouseButtonDown(0))
            {
                if (TargetBuilding == null)
                {
                    Debug.Log("sheatch build");
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, buildingLayerMask))
                    {
                        turnOffInteractionsWithStaticUI(staticUI.transform);
                        Debug.Log("build found");
                        TargetBuilding = hit.collider.gameObject;
                        openBuildingMenu(Input.mousePosition);
                    }
                }
                else
                {
                    TargetBuilding = null;
                    removeBuiidingMenu();
                    turnOnInteractionsWithStaticUI(staticUI.transform);
                }
            }
        }
    }

    public void openBuildingMenu(Vector2 CenterPoint)
    {
        Vector2 spawnPoint;
        float numberOfCategories = availableBuildingCategories.Count;
        float ang = (360f / numberOfCategories) * Mathf.Deg2Rad;

        for (int i = 0; i < availableBuildingCategories.Count; i++)
        {
            spawnPoint = new Vector2(r * Mathf.Sin(ang * i) + CenterPoint.x, r * Mathf.Cos(ang * i) + CenterPoint.y);
            GameObject temp = Instantiate(availableBuildingCategories[i], spawnPoint, transform.rotation, transform);
            temp.GetComponent<BuildingButton>().centerPoint = CenterPoint;
            buildingButtonsDysplayed.Add(temp);
        }
    }
    public void removeBuiidingMenu()
    {
        foreach (var buildingButton in buildingButtonsDysplayed)
        {
            Destroy(buildingButton.gameObject);
        }

        buildingButtonsDysplayed.Clear();

        
    }


    public void selectBuildingCategory(Vector2 CenterPoint, BuildingButton.CtegoryEnum Category)
    {
        foreach (var buildingButton in buildingButtonsDysplayed)
        {
            Destroy(buildingButton.gameObject);
        }
        buildingButtonsDysplayed.Clear();

        GameObject backButton =Instantiate(BuildingBackButton, CenterPoint, transform.rotation, transform);
        buildingButtonsDysplayed.Add(backButton);

        List<GameObject> buttonsInCurrentCategory = new List<GameObject>();
        buttonsInCurrentCategory.Clear();

        foreach (var buildingButton in availableBuildings)
        {
            if(buildingButton.GetComponent<BuildingButton>().category == Category)
            {
                buttonsInCurrentCategory.Add(buildingButton);
            }
        }

        Vector2 spawnPoint;
        float numberOfCategories = buttonsInCurrentCategory.Count;
        float ang = (360f / numberOfCategories) * Mathf.Deg2Rad;

        for (int i = 0; i < buttonsInCurrentCategory.Count; i++)
        {
            spawnPoint = new Vector2(r * Mathf.Sin(ang * i) + CenterPoint.x, r * Mathf.Cos(ang * i) + CenterPoint.y);
            GameObject temp = Instantiate(buttonsInCurrentCategory[i], spawnPoint, transform.rotation, transform);
            temp.GetComponent<BuildingButton>().centerPoint = spawnPoint;
            buildingButtonsDysplayed.Add(temp);
        }

    }



    public void setBuildingOnTimer()
    {
        if (buildingsOnTimer.Count > 0)
        {
            for (int i = 0; i < buildingsOnTimer.Count; i++)
            {
                buildingsOnTimer[i].transform.SetParent(buildingTimerSlots[i]);
                buildingsOnTimer[i].transform.position = buildingTimerSlots[i].position;
                if (i == 0)
                {
                    buildingsOnTimer[i].GetComponent<CreationTimer>().first = true;
                }
            }
        }

        numberOfBuildingsOnTimer = buildingsOnTimer.Count;

    }

    public void addBuildingOnTimer(GameObject building)
    {
        if (building != null) 
        {
            if (numberOfBuildingsOnTimer < 4)
            {
                Debug.Log("now on timer" + building.name);

                GameObject newBuilding = Instantiate(building, buildingTimerSlots[0]);
                newBuilding.GetComponent<CreationTimer>().buildingSlot = TargetBuilding;

                buildingsOnTimer.Add(newBuilding);
                setBuildingOnTimer();
                removeBuiidingMenu();
                turnOnInteractionsWithStaticUI(staticUI.transform);
            }
            else
            {
                Debug.LogWarning("no space for building to be created");
            }
        }
        else
        {
            Debug.LogWarning("building does not have setup");
        }




    }


    public void removeBuildingOnTimer(GameObject building)
    {
        Debug.Log("removed" + building.name);
        buildingsOnTimer.Remove(building);
        Destroy(building);
        setBuildingOnTimer();
    }
}
