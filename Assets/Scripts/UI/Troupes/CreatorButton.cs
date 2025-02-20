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
        Red,
        Green,
        Blue
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
            case ButtonType.Red:
                buttonControl(Em.red);
                break;
            case ButtonType.Green:
                buttonControl(Em.green);
                break;
            case ButtonType.Blue:
                buttonControl(Em.blue);
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
