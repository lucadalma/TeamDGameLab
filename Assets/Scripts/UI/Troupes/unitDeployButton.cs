using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitDeployButton : MonoBehaviour
{
    public int unitID;
    private UnitManager manager;

    private void Start()
    {
        manager = FindObjectOfType<UnitManager>();
    }

    public void selectUnitToDeploy()
    {
        manager.HandleUnitSelection(unitID,gameObject);
    }

}
