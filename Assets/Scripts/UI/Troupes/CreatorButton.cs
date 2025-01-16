using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorButton : MonoBehaviour
{
    public GameObject unitWaitTimer;
    UIManager manager;


    void Start()
    {
        manager = FindObjectOfType<UIManager>();
    }

    public void startUnitTimer()
    {
        manager.setUnitOnTimer(unitWaitTimer);
    }
}
