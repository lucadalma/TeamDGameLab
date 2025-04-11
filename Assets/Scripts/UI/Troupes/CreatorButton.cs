using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatorButton : MonoBehaviour
{
    public GameObject unitWaitTimer;
    UIManager manager;
    public Button targetButton;
    enum ButtonType
    {
        Mace,
        Dart,
        Galdius,
        Javelin

    }

    [SerializeField] ButtonType type;

    EventManager Em;

    void Start()
    {
        manager = FindObjectOfType<UIManager>();
        Em = FindObjectOfType<EventManager>();
    }

    private void Update()
    {
        switch (type)
        {
            case ButtonType.Mace:
                buttonControl(Em.Mace);
                break;
            case ButtonType.Dart:
                buttonControl(Em.Dart);
                break;
            case ButtonType.Galdius:
                buttonControl(Em.Gladius);
                break;
            case ButtonType.Javelin:
                buttonControl(Em.Javelin);
                break;
            default:
                break;
        }
    }

    public void startUnitTimer()
    {
        //Debug.Log("creation Button Pressed");
        manager.addUnitOnTimer(unitWaitTimer);
    }


    void buttonControl(bool isBuilt)
    {
        if (isBuilt)
        {
            targetButton.interactable = true;
        }
        else 
        { 
            targetButton.interactable = false;
        }
    }
}
