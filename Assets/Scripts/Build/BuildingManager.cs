using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
     GameObject BuildingEffect;
     GameObject ZonaBuilding;




    void Update()
    {
        if(BuildingEffect != null && ZonaBuilding != null)
        {
            SpawnManagerBuilding(BuildingEffect, ZonaBuilding);
        }

    }



    public void SpawnManagerBuilding(GameObject _buildingEffect, GameObject _zonaBuilding)
    {

        Vector3 spawnPosition = _zonaBuilding.transform.position;
        GameObject buildingEffect = _buildingEffect;


        Instantiate(buildingEffect, spawnPosition, Quaternion.identity);
    }
}
