using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int posiblePages;
    int currentPage = 0;

    [SerializeField] float animSpeed;

    bool animInProgress = false;
    bool buildingButtonsVisible;

    [SerializeField] GameObject staticUI;

    [NonSerialized] public int numberOfUnitsOnTimer;
    [NonSerialized] public int numberOfUnitsOnDeploy;

    [SerializeField] List<GameObject> availableUnitsToCreate;


    List<GameObject> availableUnitsToDeploy = new List<GameObject>();
    List<GameObject> unitsOnTimer = new List<GameObject>();


    [SerializeField] List<Transform> creationSelectorSlots;
    [SerializeField] List<Transform> deploimentSelectorSlots;
    [SerializeField] List<Transform> unitTimerSlots;
    [SerializeField] List<GameObject> currentBuildingButtons;

    [SerializeField] List<GameObject> availableBuildingCategories;
    [SerializeField] List<GameObject> availableBuildings;
    [SerializeField] GameObject BuildingBackButton;

    List<GameObject> buildingButtonsDysplayed = new List<GameObject>();
    List<GameObject> buildingsOnTimer = new List<GameObject>();

    [SerializeField] Transform unitPanel;
    [SerializeField] Transform buildingsPanel;

    [SerializeField] List<Transform> unitPanelPos;
    [SerializeField] List<Transform> buildingsPanelPos;



    public GameObject TargetBuilding;
    //   public Transform TargetBuildParent;
    [NonSerialized] public Vector2 TargetPoint;
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
        ShowBuildingUIButton();
        PressdCloseBuildingUI();
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
                unitsOnTimer[i].transform.SetParent(unitTimerSlots[i + availableUnitsToDeploy.Count]);
                unitsOnTimer[i].transform.position = unitTimerSlots[i + availableUnitsToDeploy.Count].position;
                if (i == 0)
                {
                    unitsOnTimer[i].GetComponent<CreationTimer>().first = true;
                }
            }
        }

        numberOfUnitsOnTimer = unitsOnTimer.Count;
        unitPanelControl();

    }

    public void addUnitOnTimer(GameObject unit)
    {
        if (numberOfUnitsOnTimer + numberOfUnitsOnDeploy < 8)
        {
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
        unitsOnTimer.Remove(unit);
        Destroy(unit);
        setUnitOnTimer();
    }


    public void AddDeployUnits(GameObject unit)
    {
        GameObject newUnit = Instantiate(unit, deploimentSelectorSlots[0]);

        availableUnitsToDeploy.Add(newUnit);

        setDeployUnits();

    }

    public void removeDeployUnits(GameObject unit)
    {

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
        setUnitOnTimer();
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
                button.enabled = true;
            }

            if (child.childCount > 0)
            {
                turnOnInteractionsWithStaticUI(child);
            }
        }

    }


    void ShowBuildingUIButton()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, buildingLayerMask) && TargetBuilding == null)
            {
                if (!buildingButtonsVisible)
                {
                    foreach (var button in currentBuildingButtons)
                    {
                        ActivateButtonVisisbility(button.transform);
                    }
                    buildingButtonsVisible = true;
                }

            }
            else
            {
                if (buildingButtonsVisible)
                {
                    foreach (var button in currentBuildingButtons)
                    {
                        DeactivateButtonVisibility(button.transform);
                    }

                    buildingButtonsVisible = false;
                }



            }

        }
    }



    void DeactivateButtonVisibility(Transform parent)
    {
            Image image = parent.GetComponent<Image>();
            Button button = parent.GetComponent<Button>();


            if (image != null)
            {
                image.enabled = false;
            }

            if (button != null)
            {
                button.enabled = false;
            }

        foreach (Transform child in parent)
        {
            if (child.childCount > 0)
            {
                DeactivateButtonVisibility(child);
            }
        }
    }


    void ActivateButtonVisisbility(Transform parent)
    {
            Image image = parent.GetComponent<Image>();
            Button button = parent.GetComponent<Button>();


            if (image != null)
            {
                image.enabled = true;
            }

            if (button != null)
            {
                button.enabled = true;
            }

        foreach (Transform child in parent)
        {
            if (child.childCount > 0)
            {
                ActivateButtonVisisbility(child);
            }
        }
    }


    //buildin buttons functions
    public void PressdOpenBuildingUI(GameObject targetBuilding)
    {
        Debug.Log("ButtonPressed");
        turnOffInteractionsWithStaticUI(staticUI.transform);
        TargetBuilding = targetBuilding;
        //      TargetBuildParent = hit.collider.gameObject.transform.parent;
        openBuildingMenu(Input.mousePosition);

    }

    void PressdCloseBuildingUI()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (TargetBuilding != null && Input.GetMouseButtonDown(0))
            {
                TargetBuilding = null;
                //TargetBuildParent = null;
                removeBuiidingMenu(Input.mousePosition);
                turnOnInteractionsWithStaticUI(staticUI.transform);
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
            GameObject temp = Instantiate(availableBuildingCategories[i], CenterPoint, transform.rotation, transform);
            StartCoroutine(AnimationProgression(CenterPoint, spawnPoint, temp));
            temp.GetComponent<BuildingButton>().centerPoint = CenterPoint;
            buildingButtonsDysplayed.Add(temp);
        }

        foreach (var button in currentBuildingButtons)
        {
            DeactivateButtonVisibility(button.transform);
        }


        buildingButtonsVisible = false;
        //StartCoroutine(AnimStartCreate(ang, CenterPoint));
    }
    public void removeBuiidingMenu(Vector2 CenterPoint)
    {
        foreach (var buildingButton in buildingButtonsDysplayed)
        {
            Vector2 startPos = buildingButton.transform.position;
            StartCoroutine(AnimationProgression(startPos, CenterPoint, buildingButton));
            Destroy(buildingButton.gameObject);
        }

        //StartCoroutine(AnimStartRemove(CenterPoint));

        buildingButtonsDysplayed.Clear();


    }


    public void selectBuildingCategory(Vector2 CenterPoint, BuildingButton.CtegoryEnum Category)
    {
        foreach (var buildingButton in buildingButtonsDysplayed)
        {
            Vector2 startPos = buildingButton.transform.position;
            StartCoroutine(AnimationProgression(startPos, CenterPoint, buildingButton));

            Destroy(buildingButton.gameObject);
        }
        buildingButtonsDysplayed.Clear();

        GameObject backButton = Instantiate(BuildingBackButton, CenterPoint, transform.rotation, transform);
        buildingButtonsDysplayed.Add(backButton);

        List<GameObject> buttonsInCurrentCategory = new List<GameObject>();
        buttonsInCurrentCategory.Clear();

        foreach (var buildingButton in availableBuildings)
        {
            if (buildingButton.GetComponent<BuildingButton>().category == Category)
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
            GameObject temp = Instantiate(buttonsInCurrentCategory[i], CenterPoint, transform.rotation, transform);

            if (temp.GetComponent<BuildingButton>().buttonType == TargetBuilding.GetComponent<BuildingCategorization>().type)
            {
                temp.GetComponent<Button>().interactable = false;
            }

            StartCoroutine(AnimationProgression(CenterPoint, spawnPoint, temp));
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

                CreationTimer Timer = buildingsOnTimer[i].GetComponent<CreationTimer>();

                Timer.onOrder = i;
                //if (i == 0)
                //{
                //    Timer.BuldingOrderText.text = "";
                //}
                //else
                //{
                //    Timer.BuldingOrderText.text = i.ToString();
                //}

            }
        }

        numberOfBuildingsOnTimer = buildingsOnTimer.Count;
    }

    public void addBuildingOnTimer(GameObject building, Vector2 CenterPoint)
    {
        if (building != null)
        {
            if (numberOfBuildingsOnTimer < 8)
            {

                GameObject newBuilding = Instantiate(building, TargetBuilding.GetComponent<BuildingCategorization>().BuildingButton.transform);
                newBuilding.GetComponent<CreationTimer>().buildingSlot = TargetBuilding;
                TargetBuilding.layer = LayerMask.NameToLayer("BuiildingsInQueue");

                buildingsOnTimer.Add(newBuilding);

                setBuildingOnTimer();
                TargetBuilding = null;
                removeBuiidingMenu(CenterPoint);
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
        buildingsOnTimer.Remove(building);
        Destroy(building);
        setBuildingOnTimer();
    }

    public void removeBuildingButton(GameObject ButtonToRemove)
    {
        currentBuildingButtons.Remove(ButtonToRemove);
    }

    public void addBuildingButton(GameObject ButtonToAdd)
    {
        currentBuildingButtons.Add(ButtonToAdd);
    }



    IEnumerator AnimStartRemove(Vector2 CenterPoint)
    {
        foreach (var buildingButton in buildingButtonsDysplayed)
        {
            Vector2 startPos = buildingButton.transform.position;
            StartCoroutine(AnimationProgression(startPos, CenterPoint, buildingButton));

            while (animInProgress)
            {
                yield return null;
            }
            Destroy(buildingButton.gameObject);
            yield return null;

        }
    }






    IEnumerator AnimStartCreate(float ang, Vector2 CenterPoint)
    {
        Vector2 spawnPoint;

        for (int i = 0; i < availableBuildingCategories.Count; i++)
        {
            while (animInProgress)
            {
                yield return null;
            }

            spawnPoint = new Vector2(r * Mathf.Sin(ang * i) + CenterPoint.x, r * Mathf.Cos(ang * i) + CenterPoint.y);
            GameObject temp = Instantiate(availableBuildingCategories[i], CenterPoint, transform.rotation, transform);
            StartCoroutine(AnimationProgression(CenterPoint, spawnPoint, temp));
            temp.GetComponent<BuildingButton>().centerPoint = CenterPoint;
            buildingButtonsDysplayed.Add(temp);
            yield return null;
        }
    }

    IEnumerator AnimationProgression(Vector2 startPos, Vector2 endPos, GameObject obj)
    {
        bool animationTransitioned = false;
        animInProgress = true;

        for (float i = 0; i < 1; i += animSpeed * Time.deltaTime)
        {
            obj.transform.position = Vector2.Lerp(startPos, endPos, i);
            if (i > 0.5 && !animationTransitioned)
            {
                animationTransitioned = true;
                animInProgress = false;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    void unitPanelControl()
    {
        int currentPos;

        if ((unitsOnTimer.Count + availableUnitsToDeploy.Count) % 2 == 0)
        {
            currentPos = (unitsOnTimer.Count + availableUnitsToDeploy.Count) / 2;
        }
        else
        {
            currentPos = (unitsOnTimer.Count + availableUnitsToDeploy.Count + 1) / 2;
        }
        unitPanel.position = unitPanelPos[currentPos].position;

    }

    void AbilityPanelControl()
    {

        int currentPos;

        if (buildingsOnTimer.Count % 2 == 0)
        {
            currentPos = buildingsOnTimer.Count / 2;
        }
        else
        {
            currentPos = (buildingsOnTimer.Count + 1) / 2;
        }
        buildingsPanel.position = buildingsPanelPos[currentPos].position;

    }



}
