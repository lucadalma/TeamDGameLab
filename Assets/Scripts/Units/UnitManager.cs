using System.Runtime.CompilerServices;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public UnitData[] unitOptions; // Opzioni delle unità (Scriptable Objects)
    public LayerMask laneLayerMask; // Layer delle corsie
    public Transform targetPoint;  // Punto finale per le unità

    private UnitData selectedUnit; // Unità attualmente selezionata
    private float[] cooldownTimers; // Timer per il cooldown delle unità

    private bool readyToDeploy;
    private GameObject currentUnitButton;
    private UIManager uIManager;
    [SerializeField] 
    public Transform[] spawnpoints; // Punti di spawn per le corsie

    [SerializeField]
    public Transform baseTransform;


    void Start()
    {
        // Inizializza i timer di cooldown
        cooldownTimers = new float[unitOptions.Length];
        uIManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        HandleCooldowns();
        HandleLaneClick();
    }

    private void HandleCooldowns()
    {
        // Aggiorna tutti i timer di cooldown
        for (int i = 0; i < cooldownTimers.Length; i++)
        {
            if (cooldownTimers[i] > 0)
            {
                cooldownTimers[i] -= Time.deltaTime;
            }
        }
    }

    public void HandleUnitSelection(int UnitIDToSelect, GameObject currentButton)
    {
        foreach (UnitData unitData in unitOptions)
        {
            if (unitData.unitID == UnitIDToSelect)
            {
                currentUnitButton = currentButton;
                selectedUnit = unitData;
                Debug.Log($"Selezionata unità: {selectedUnit.unitName}");
                readyToDeploy = true;
                return;
            }
        }

        Debug.LogError("Unità non trovata");
    }

    private void HandleLaneClick()
    {
        if (readyToDeploy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                int unitIndex = System.Array.IndexOf(unitOptions, selectedUnit);
                if (unitIndex < 0)
                {
                    Debug.LogError("Errore: Unità selezionata non trovata tra le opzioni.");
                    return;
                }

                if (cooldownTimers[unitIndex] > 0)
                {
                    Debug.Log($"Cooldown attivo per {selectedUnit.unitName}. Tempo rimanente: {cooldownTimers[unitIndex]:F2} secondi.");
                    return;
                }

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, laneLayerMask))
                {
                    Debug.Log($"Hit su oggetto: {hit.transform.name}");
                    int laneIndex = GetLaneIndex(hit.transform);
                    Debug.Log($"Lane index cliccato: {laneIndex}");

                    
                    if (laneIndex >= 0 && laneIndex < spawnpoints.Length)
                    {
                        uIManager.removeDeployUnits(currentUnitButton);
                        Vector3 spawnPosition = new Vector3(spawnpoints[laneIndex].position.x, spawnpoints[laneIndex].position.y, baseTransform.position.z);
                        SpawnUnitsInArea(spawnPosition, unitIndex, laneIndex);
                        readyToDeploy = false;
                    }
                    else
                    {
                        Debug.LogError("Indice della corsia non valido!");
                    }
                }
            }
        }
    }

    private void SpawnUnitsInArea(Vector3 centerPosition, int unitIndex, int laneIndex)
    {
        if (selectedUnit == null || selectedUnit.unitPrefab == null)
        {
            Debug.LogError("Unità selezionata o prefab mancante!");
            return;
        }

        int unitCount = selectedUnit.unitCount;
        float areaWidth = 5f;
        float areaLength = 5f;

        Debug.Log($"Inizio spawn di {unitCount} unità...");

        for (int i = 0; i < unitCount; i++)
        {
            float offsetX = Random.Range(-areaWidth / 2, areaWidth / 2);
            float offsetZ = Random.Range(-areaLength / 2, areaLength / 2);
            Vector3 spawnPosition = centerPosition + new Vector3(offsetX, 0, offsetZ);

            Debug.Log($"Spawn unità {i + 1} in posizione {spawnPosition}");

            GameObject unit = Instantiate(selectedUnit.unitPrefab, spawnPosition, Quaternion.identity);
            TanksBehavior behavior = unit.GetComponent<TanksBehavior>();
            if (laneIndex == 0)
            {
                behavior.lane = Lane.Lane3;
            }
            else if (laneIndex == 1)
            {
                behavior.lane = Lane.Lane2;
            }
            else
            {
                behavior.lane = Lane.Lane1;
            }

            if (behavior != null)
            {
                behavior.Initialize(selectedUnit, targetPoint);
                Debug.Log($"{selectedUnit.unitName} spawnata in {spawnPosition}");
                behavior.isEnemy = false;
            }
            else
            {
                Debug.LogError($"Componente UnitBehavior mancante nel prefab di {selectedUnit.unitName}");
            }
        }

        cooldownTimers[unitIndex] = selectedUnit.spawnCd;
    }
    private int GetLaneIndex(Transform hitTransform)
    {
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            if (spawnpoints[i] == hitTransform)
            {
                return i;
            }
        }

        return -1;
    }


}