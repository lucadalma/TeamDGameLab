using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAbility : MonoBehaviour
{

    EventAbility EA;
    public bool mis, over, emp;

    private void Start()
    {
        if(EA != null)
            EA = FindObjectOfType<EventAbility>();
    }

    private void Update()
    {
        EA.mis = mis;
        EA.over = over;
        EA.emp = emp;
    }
}
