using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public UnitData[] unitOptions; // Opzioni delle unità (Scriptable Objects)
    public LayerMask laneLayerMask; // Layer delle corsie
    public Transform targetPoint;  // Punto finale per le unità

    private UnitData selectedUnit; // Unità attualmente selezionata

    void Update()
    {
        HandleUnitSelection();
        HandleLaneClick();
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, laneLayerMask))
            {
                SpawnUnit(hit.point);
            }
        }
    }

    private void SpawnUnit(Vector3 position)
    {
        if (selectedUnit.unitPrefab == null)
        {
            Debug.LogError($"Prefab non assegnato per {selectedUnit.unitName}");
            return;
        }

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
    }
}