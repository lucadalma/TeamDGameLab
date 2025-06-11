using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase_BuildingManager : MonoBehaviour
{
    public List<GameObject> buildings;

    public BoolVariable pause;

    public SO_EnemyBuilding enemyBuildingsList;
    private void Start()
    {
        StartCoroutine(SpawnBuildings());
    }

    private IEnumerator SpawnBuildings()
    {
        while (true)
        {

            for (int i = 0; i < enemyBuildingsList.buildings.Count; i++)
            {
                
                yield return StartCoroutine(WaitWhileNotPaused(enemyBuildingsList.timers[i]));


                if (buildings[i] != null)
                {
                    GameObject building = Instantiate(enemyBuildingsList.buildings[i], buildings[i].transform.position, Quaternion.identity, buildings[i].gameObject.transform.parent);
                    building.GetComponent<EnemyBase_Event>().unitToUpgrade = enemyBuildingsList.units[i];
                    GameObject build = buildings[i];
                    //buildings.Remove(buildings[i]);
                    buildings[i] = building;
                    Destroy(build);


                    //enemyBuildingsList.buildings.Remove(enemyBuildingsList.buildings[i])
                }


            }
        }
    }

    private IEnumerator WaitWhileNotPaused(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            if (!pause.Value)
            {
                timer += Time.deltaTime;
            }
            yield return null;
        }
    }





}
