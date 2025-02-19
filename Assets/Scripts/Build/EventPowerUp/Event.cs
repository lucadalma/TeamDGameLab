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

        //Stack();
        //stack = SE.AddStackHp(stack, 1);
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
            SE.RemoveAddStackHp(this.gameObject, null);
        if (eventType == EventType.Speed)
            SE.AddStackSpeedUp(this.gameObject, stack, 1);
    }





    private void HPRegeneration()
    {
        
        EM.newHP = 1 * Time.deltaTime;
        Debug.Log(stack);

        if (ChangeEvent(EventType.HPRegen) == false)
        {
            stack = 0;
            EM.RemoveListAction(HPRegeneration);
            EM.RemoveAddStackHp(null, this.gameObject);
        }
    }



    private void SpeedUp()
    {

        EM.newMoveSpeed = 1;

        if (ChangeEvent(EventType.HPRegen) == false)
        {
            stack = 0;
            EM.RemoveListAction(SpeedUp);
            EM.RemoveSpeedUpStack(null, this.gameObject);
        }
    }




    private void UnitaUnLook(bool red, bool green, bool blue)
    {
        if (red == true)
        {
            EM.red = true;
        }
        if (green == true)
        {
            EM.green = true;
        }
        if (blue == true)
        {
            EM.blue = true;
            Debug.Log(EM.blue);
        }

    }






    bool ChangeEvent(EventType _eventType)
    {
        if (eventType != _eventType)
        {
            return false;
        }

        return true;
    }


}
