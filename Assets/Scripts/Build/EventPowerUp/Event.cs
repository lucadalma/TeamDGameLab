using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EventType
{
    None,
    HPRegen,
    Speed,
    RedUnità,
    GreenUnità,
    BlueUnità
};





public class Event : MonoBehaviour
{
    EventManager EM;
    StackEvent SE;


    public EventType eventType;
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
            default:
                break;
        }




    }




    void Stack()
    {
        if (eventType == EventType.HPRegen)
            SE.RemoveAddStackHpList(this.gameObject, null);
        if (eventType == EventType.Speed)
            SE.RemoveSpeedUpStackList(this.gameObject, null);
    }





    private void HPRegeneration()
    {
        stack = SE.AddStackHp(stack, 1);
        EM.newHP = stack * Time.deltaTime;

        if (ChangeEvent() == true)
        {
            EM.RemoveListAction(HPRegeneration);
            SE.RemoveAddStackHpList(null, this.gameObject);
            Destroy(this.gameObject);
        }
    }



    private void SpeedUp()
    {
        stack = SE.AddStackSpeedUp(stack, 1);
        EM.newMoveSpeed = stack;

        //if (ChangeEvent() == false)
        //{
        //    EM.RemoveListAction(SpeedUp);
        //    SE.RemoveSpeedUpStack(null, this.gameObject);
        //    Destroy(this.gameObject);
        //}
    }




    private void UnitaUnLook(bool red, bool green, bool blue)
    {
        if (red == true)
            EM.red = true;
        if (green == true)
            EM.green = true;
        if (blue == true)
            EM.blue = true;

        //if (ChangeEvent() == false)
        //    Destroy(this.gameObject);

    }





    bool change;
    bool ChangeEvent()
    {
        if (change == true)
        {
            return true;
        }

        return false;
    }


}
