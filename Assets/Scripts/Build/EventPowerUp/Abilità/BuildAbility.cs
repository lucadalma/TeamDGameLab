using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum abilityTypes { mis, over, emp }
public class BuildAbility : MonoBehaviour
{

    EventAbility EA;

    UIManager uIM;


    public abilityTypes abilityType;
    public GameObject connectedAbButton;

    private void Start()
    {
        if (EA == null)
            EA = FindObjectOfType<EventAbility>();
        if (uIM == null)
            uIM = FindObjectOfType<UIManager>();

        uIM.addAbility(gameObject);
    }

    private void OnDestroy()
    {
       uIM.removeAbility(gameObject, connectedAbButton);
    }
}
