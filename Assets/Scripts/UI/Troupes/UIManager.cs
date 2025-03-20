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
using TMPro;
using static UnityEditor.PlayerSettings;

public class UIManager : MonoBehaviour
{

    public BoolVariable pause;

    int posiblePages;
    int currentPage = 0;

    [SerializeField] float animSpeed;

    bool animInProgress = false;
    bool buildingButtonsVisible;
    bool mouseOverTimer;

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
    [SerializeField] List<GameObject> availableUnitsToUpgrade;
    [SerializeField] GameObject BuildingBackButton;

    List<GameObject> buildingButtonsDysplayed = new List<GameObject>();
    List<GameObject> buildingsOnTimer = new List<GameObject>();



    // UI panel references
    [SerializeField] Transform unitPanel;
    [SerializeField] Transform buildingsPanel;

    [SerializeField] List<Transform> unitPanelPos;
    [SerializeField] List<Transform> buildingsPanelPos;


    // info box variables
    [SerializeField] Transform infoBoxFolder;
    [SerializeField] LayerMask infoItemsLayer;
    [SerializeField] GameObject infoBoxBase;
    [SerializeField] TextMeshProUGUI infoBoxPreview;

    float infoBoxPressTimer;

    public GameObject TargetBuilding;
    [SerializeField] Transform buildingFolder;
    [NonSerialized] public Vector2 TargetPoint;
    [SerializeField] private LayerMask buildingLayerMask;
    [SerializeField] float r;

    [NonSerialized] public int numberOfBuildingsOnTimer;

    //Top UI variables

    //Mach timer
    [SerializeField] TextMeshProUGUI machTimer;
    float tickingTime;
    int seconds = 0;
    int tenSeconds = 0;
    int Minutes = 0;
    int tenMinutes = 0;

    // HP Bars
    [SerializeField] GameObject fortress;
    [SerializeField] GameObject enemy;
    [SerializeField] Image fortressBar;
    [SerializeField] Image enemyBar;

    //Pause Button
    GameManager gM;
    [SerializeField] Image PauseLight;


    void Start()
    {
        gM = FindObjectOfType<GameManager>();
        infoBoxPressTimer = 1;

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
        changePauseLight();
        MachTimerTracker();
        FillHPBars();
        ShowBuildingUIButton();
        PressdCloseBuildingUI();
        CreateInfoBox();
    }

    #region UnitControls
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
    #endregion

    #region BuildingControls
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

            if (!Physics.Raycast(ray, out RaycastHit hit2, Mathf.Infinity, buildingLayerMask) && !mouseOverTimer)
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

    public void PressdOpenBuildingUI(GameObject targetBuilding)
    {
        Debug.Log("ButtonPressed");
        turnOffInteractionsWithStaticUI(staticUI.transform);
        TargetBuilding = targetBuilding;
        openBuildingMenu(Input.mousePosition);

    }

    void PressdCloseBuildingUI()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (TargetBuilding != null && Input.GetMouseButtonDown(0))
            {
                TargetBuilding = null;
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
            spawnPoint = new Vector2(r * Screen.width * Mathf.Sin(ang * i) + CenterPoint.x, r * Screen.width * Mathf.Cos(ang * i) + CenterPoint.y);
            GameObject temp = Instantiate(availableBuildingCategories[i], CenterPoint, transform.rotation, buildingFolder);
            StartCoroutine(AnimationProgression(CenterPoint, spawnPoint, temp));
            temp.GetComponent<BuildingButton>().centerPoint = CenterPoint;
            buildingButtonsDysplayed.Add(temp);
        }

        foreach (var button in currentBuildingButtons)
        {
            DeactivateButtonVisibility(button.transform);
        }


        buildingButtonsVisible = false;
    }
    public void removeBuiidingMenu(Vector2 CenterPoint)
    {
        foreach (var buildingButton in buildingButtonsDysplayed)
        {
            Destroy(buildingButton.gameObject);
        }

        buildingButtonsDysplayed.Clear();
    }


    public void selectBuildingCategory(Vector2 CenterPoint, BuildingButton.CategoryEnum Category)
    {
        foreach (var buildingButton in buildingButtonsDysplayed)
        {
            Destroy(buildingButton.gameObject);
        }

        buildingButtonsDysplayed.Clear();

        GameObject backButton = Instantiate(BuildingBackButton, CenterPoint, transform.rotation, buildingFolder);
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
            spawnPoint = new Vector2(r * Screen.width * Mathf.Sin(ang * i) + CenterPoint.x, r * Screen.width * Mathf.Cos(ang * i) + CenterPoint.y);
            GameObject temp = Instantiate(buttonsInCurrentCategory[i], CenterPoint, transform.rotation, buildingFolder);


            if (temp.GetComponent<BuildingButton>().buttonType == TargetBuilding.GetComponent<BuildingCategorization>().type && temp.GetComponent<BuildingButton>().category != BuildingButton.CategoryEnum.upgrades)
            {
                temp.GetComponent<Button>().interactable = false;
            }

            StartCoroutine(AnimationProgression(CenterPoint, spawnPoint, temp));
            temp.GetComponent<BuildingButton>().centerPoint = CenterPoint;
            buildingButtonsDysplayed.Add(temp);
        }

    }

    public void SelectUnitToUpgrade(Vector2 CenterPoint, BuildingButton.ButtonEnum Upgrade, BuildingButton.forUnit unitToUpgardes, GameObject timer)
    {
        foreach (var buildingButton in buildingButtonsDysplayed)
        {
            Vector2 startPos = buildingButton.transform.position;
            StartCoroutine(AnimationProgression(startPos, CenterPoint, buildingButton));

            Destroy(buildingButton.gameObject);
        }
        buildingButtonsDysplayed.Clear();

        GameObject backButton = Instantiate(BuildingBackButton, CenterPoint, transform.rotation, buildingFolder);
        buildingButtonsDysplayed.Add(backButton);
        backButton.GetComponent<BuildingButton>().type = BuildingButton.TypeEnum.backToUpgrades;

        Vector2 spawnPoint;
        float numberOfCategories = availableUnitsToUpgrade.Count;
        float ang = (360f / numberOfCategories) * Mathf.Deg2Rad;

        for (int i = 0; i < availableUnitsToUpgrade.Count; i++)
        {
            spawnPoint = new Vector2(r * Screen.width * Mathf.Sin(ang * i) + CenterPoint.x, r * Screen.width * Mathf.Cos(ang * i) + CenterPoint.y);
            GameObject temp = Instantiate(availableUnitsToUpgrade[i], CenterPoint, transform.rotation, buildingFolder);
            temp.GetComponent<BuildingButton>().buttonType = Upgrade;
            temp.GetComponent<BuildingButton>().BuildingTimer = timer;

            if (temp.GetComponent<BuildingButton>().buttonType == TargetBuilding.GetComponent<BuildingCategorization>().type && temp.GetComponent<BuildingButton>().unitAffected == TargetBuilding.GetComponent<BuildingCategorization>().unitToUpgrade)
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

    public void addBuildingOnTimer(GameObject building, Vector2 CenterPoint, BuildingButton.TypeEnum type, BuildingButton.forUnit affectrdUnit)
    {
        if (building != null)
        {
            if (numberOfBuildingsOnTimer < 8)
            {

                GameObject newBuilding = Instantiate(building, TargetBuilding.GetComponent<BuildingCategorization>().BuildingButton.transform);
                newBuilding.GetComponent<CreationTimer>().buildingSlot = TargetBuilding;

                if (type == BuildingButton.TypeEnum.unitSelector)
                {
                    newBuilding.GetComponent<CreationTimer>().unitToUpgrade = affectrdUnit;
                }


                TargetBuilding.layer = LayerMask.NameToLayer("BuiildingsInQueue");

                buildingsOnTimer.Add(newBuilding);

                setBuildingOnTimer();
                TargetBuilding = null;
                removeBuiidingMenu(CenterPoint);
                turnOnInteractionsWithStaticUI(staticUI.transform);
                buildingButtonsVisible = false;
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
        buildingButtonsVisible = false;
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
            if (obj != null)
            {
                obj.transform.position = Vector2.Lerp(startPos, endPos, i);
                if (i > 0.5 && !animationTransitioned)
                {
                    animationTransitioned = true;
                    animInProgress = false;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    #endregion

    #region PanelControls
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
    #endregion

    #region InfoBox

    void CreateInfoBox()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(1))
            {
                infoBoxPressTimer = 0;
            }

            if (Input.GetMouseButton(1))
            {
                infoBoxPressTimer += Time.deltaTime;
            }


            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<InfoForInfoBox>() != null)
                {
                    if (Input.GetMouseButtonUp(1) && infoBoxPressTimer <= 0.2f)
                    {
                        InfoForInfoBox infos = result.gameObject.GetComponent<InfoForInfoBox>();
                        GameObject tempInfoBox = Instantiate(infoBoxBase, Input.mousePosition, Quaternion.identity, infoBoxFolder);
                        tempInfoBox.GetComponent<ItemBox>().SetItemBox(infos.InfoIcon, infos.InfoName, infos.InfoDescription);
                    }

                    if (infoBoxPreview.text != result.gameObject.GetComponent<InfoForInfoBox>().InfoName)
                    {
                        infoBoxPreview.text = result.gameObject.GetComponent<InfoForInfoBox>().InfoName;
                    }
                    break;
                }

            }
            foreach (RaycastResult result in results)
            {

                if (result.gameObject.tag == "BuildingTimer")
                {
                    mouseOverTimer = true;
                    break;
                }
                else
                {
                    mouseOverTimer = false;
                }

            }
        }
        else
        {
            mouseOverTimer = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, infoItemsLayer))
            {

                if (Input.GetMouseButtonDown(1))
                {
                    infoBoxPressTimer = 0;
                }

                if (Input.GetMouseButton(1))
                {
                    infoBoxPressTimer += Time.deltaTime;
                }
                if (hit.transform.GetComponent<InfoForInfoBox>() != null)
                {
                    if (Input.GetMouseButtonUp(1) && infoBoxPressTimer <= 0.2f)
                    {
                        InfoForInfoBox infos = hit.transform.GetComponent<InfoForInfoBox>();
                        GameObject tempInfoBox = Instantiate(infoBoxBase, Input.mousePosition, Quaternion.identity, infoBoxFolder);
                        tempInfoBox.GetComponent<ItemBox>().SetItemBox(infos.InfoIcon, infos.InfoName, infos.InfoDescription);
                    }

                    if (infoBoxPreview.text != hit.transform.gameObject.GetComponent<InfoForInfoBox>().InfoName)
                    {
                        infoBoxPreview.text = hit.transform.gameObject.GetComponent<InfoForInfoBox>().InfoName;
                    }
                }
                else
                {
                    if (infoBoxPreview.text != "")
                    {
                        infoBoxPreview.text = "";
                    }
                }

            }
            else
            {
                if (infoBoxPreview.text != "")
                {
                    infoBoxPreview.text = "";
                }
            }
        }

    }


    #endregion


    #region TopUI
    void MachTimerTracker()
    {
        if (pause.Value == false)
        {
            if (tickingTime <= 1)
            {
                tickingTime += Time.deltaTime;
            }
            else
            {
                tickingTime = 0;
                if (seconds < 9)
                {
                    seconds++;
                }
                else
                {
                    seconds = 0;
                    if (tenSeconds < 6)
                    {
                        tenSeconds++;
                    }
                    else
                    {
                        tenSeconds = 0;
                        if (Minutes < 9)
                        {
                            Minutes++;
                        }
                        else
                        {
                            Minutes = 0;
                            tenMinutes++;
                        }
                    }

                }

            }
        }
        machTimer.text = "" + tenMinutes + Minutes + ":" + tenSeconds + seconds;
    }

    void FillHPBars()
    {
        fortressBar.fillAmount = fortress.GetComponent<BaseHealth>().health / fortress.GetComponent<BaseHealth>().maxHealth;
        enemyBar.fillAmount = enemy.GetComponent<BaseHealth>().health / enemy.GetComponent<BaseHealth>().maxHealth;
    }

    public void PauseGameButton()
    {
        gM.PauseGame();
    }

    void changePauseLight()
    {
        switch (gM.getGameStatus)
        {
            case GameStatus.GamePause:
                PauseLight.color = Color.red;
                break;
            case GameStatus.GameRunning:
                PauseLight.color = Color.green;
                break;
            default:
                break;
        }
    }


    #endregion


}
