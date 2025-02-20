using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EventType
{
    None,
    HPRegen,
    Speed,
    BuildingCreationSpeed,
    RedUnità,
    GreenUnità,
    BlueUnità
};





public class Event : MonoBehaviour
{
    EventManager EM;
    
    StackEvent SE;
    public EventType eventType;

    [Header("AmmountStack")]
    public float hpStack;
    public float speedStack;
    public float buildSpeedStack;


    float stack;


    private void Start()
    {
        if (EM == null)
            EM = FindObjectOfType<EventManager>();
        if (SE == null)
            SE = FindObjectOfType<StackEvent>();

        Stack();
    }


    void Update()
    {
        SwitcAbility();
    }



    void SwitcAbility()
    {
        switch (eventType)
        {
            case EventType.None:
                break;
            case EventType.HPRegen:
                EM.AddListAction(HPRegeneration);
                break;
            case EventType.Speed:
                EM.AddListAction(SpeedUp);
                break;
            case EventType.RedUnità:
                UnitaUnLook(true, false, false);
                break;
            case EventType.GreenUnità:
                UnitaUnLook(false, true, false);
                break;
            case EventType.BlueUnità:
                UnitaUnLook(false, false, true);
                break;
            case EventType.BuildingCreationSpeed:
                EM.AddListAction(BuildSpeed);
                break;
            default:
                break;
        }




    }


    void Stack()
    {
        if (eventType == EventType.HPRegen)
            SE.AddStackHpList(this.gameObject);
        if (eventType == EventType.Speed)
            SE.AddSpeedUpStackList(this.gameObject);
        if (eventType == EventType.Speed)
            SE.AddBuildSpeedList(this.gameObject);
    }





    private void HPRegeneration()
    {
        stack = SE.ChangeStackHp(stack, hpStack);
        EM.newHP = stack * Time.deltaTime;
    }



    private void SpeedUp()
    {
        stack = SE.ChangeStackSpeedUp(stack, speedStack);
        EM.newMoveSpeed = stack;


    }


    private void BuildSpeed()
    {
        stack = SE.ChaneStackBuildSpeed(stack, buildSpeedStack);
        EM.newBuildSpeed = stack;
    }


    private void UnitaUnLook(bool red, bool green, bool blue)
    {
        if (red == true)
            EM.red = true;
        if (green == true)
            EM.green = true;
        if (blue == true)
            EM.blue = true;

    }


    private void OnDestroy()
    {
        switch (eventType)
        {
            case EventType.None:
                break;
            case EventType.HPRegen:
                EM.RemoveListAction(HPRegeneration);
                SE.RemoveStackHpList(this.gameObject);
                SE.ChangeStackHp(stack, hpStack);
                break;
            case EventType.Speed:
                EM.RemoveListAction(SpeedUp);
                SE.RemoveSpeedUpStackList(this.gameObject);
                SE.ChangeStackSpeedUp(stack, speedStack);
                break;
            case EventType.BuildingCreationSpeed:
                EM.RemoveListAction(BuildSpeed);
                SE.RemoveBuildSpeedList(this.gameObject);
                SE.ChaneStackBuildSpeed(stack, buildSpeedStack);
                break;
            case EventType.RedUnità:
                UnitaUnLook(false, false, false);
                break;
            case EventType.GreenUnità:
                UnitaUnLook(false, false, false);
                break;
            case EventType.BlueUnità:
                UnitaUnLook(false, false, false);
                break;
            default:
                break;
        }


    }




}
