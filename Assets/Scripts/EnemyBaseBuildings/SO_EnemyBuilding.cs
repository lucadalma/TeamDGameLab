using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyBase/Buildings")]
public class SO_EnemyBuilding : ScriptableObject
{
    [SerializeField]
    public List<GameObject> buildings;

    [SerializeField]
    public List<BuildingButton.forUnit> units;

    [SerializeField]
    public List<float> timers;

    private void OnValidate()
    {
        if (buildings.Count != timers.Count)
        {
            while (timers.Count < buildings.Count)
            {
                timers.Add(0);
                units.Add(BuildingButton.forUnit.notUnitUpgrade);
            }

            while (timers.Count > buildings.Count)
            {
                timers.RemoveAt(timers.Count - 1);
                timers.RemoveAt(units.Count - 1);
            }
        }
    }
}
