using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    [SerializeField]
    GameEvent evento;

    private void OnDestroy()
    {
        evento.Raise();
    }
}
