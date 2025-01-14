using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public UnitData[] unitOptions; // Opzioni delle unità (Scriptable Objects)
    public LayerMask laneLayerMask; // Layer delle corsie
    public Transform targetPoint;  // Punto finale per le unità

    private UnitData selectedUnit; // Unità attualmente selezionata
    private float[] cooldownTimers; // Timer per il cooldown delle unità

    void Start()
    {
        // Inizializza i timer di cooldown
        cooldownTimers = new float[unitOptions.Length];
    }

    void Update()
    {
        HandleCooldowns();
        HandleUnitSelection();
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

    private void HandleUnitSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && unitOptions.Length > 0)
        {
            selectedUnit = unitOptions[0];
            Debug.Log($"Selezionata unità: {selectedUnit.unitName}");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && unitOptions.Length > 1)
        {
            selectedUnit = unitOptions[1];
            Debug.Log($"Selezionata unità: {selectedUnit.unitName}");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && unitOptions.Length > 2)
        {
            selectedUnit = unitOptions[2];
            Debug.Log($"Selezionata unità: {selectedUnit.unitName}");
        }
    }

    private void HandleLaneClick()
    {
        if (Input.GetMouseButtonDown(0) && selectedUnit != null)
        {
            int unitIndex = System.Array.IndexOf(unitOptions, selectedUnit);
            if (unitIndex < 0)
            {
                Debug.LogError("Errore: Unità selezionata non trovata tra le opzioni.");
                return;
            }

            // Controlla il cooldown dell'unità selezionata
            if (cooldownTimers[unitIndex] > 0)
            {
                Debug.Log($"Cooldown attivo per {selectedUnit.unitName}. Tempo rimanente: {cooldownTimers[unitIndex]:F2} secondi.");
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, laneLayerMask))
            {
                SpawnUnit(hit.point, unitIndex);
            }
        }
    }

    private void SpawnUnit(Vector3 position, int unitIndex)
    {
        if (selectedUnit.unitPrefab == null)
        {
            Debug.LogError($"Prefab non assegnato per {selectedUnit.unitName}");
            return;
        }

        // Istanzia l'unità
        GameObject unit = Instantiate(selectedUnit.unitPrefab, position, Quaternion.identity);
        UnitBehavior behavior = unit.GetComponent<UnitBehavior>();

        if (behavior != null)
        {
            behavior.Initialize(selectedUnit, targetPoint);
            Debug.Log($"{selectedUnit.unitName} spawnata in {position}");
        }
        else
        {
            Debug.LogError($"Componente UnitBehavior mancante nel prefab di {selectedUnit.unitName}");
        }

        // Avvia il cooldown
        cooldownTimers[unitIndex] = selectedUnit.spawnCd;
    }
}