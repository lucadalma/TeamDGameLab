using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase_EventManager : MonoBehaviour
{
    [SerializeField] public List<Action> actions = new List<Action>();

    //[HideInInspector]

    public float newBuildSpeed;

    [HideInInspector]
    public bool Mace, Dart, Gladius, Javelin;


    private void Update()
    {
        UsedPowerUp();
    }



    public void AddListAction(Action action)
    {
        actions.Add(action);
    }
    public void RemoveListAction(Action action)
    {
        actions.Remove(action);
    }






    public void UsedPowerUp()
    {
        foreach (var evento in actions)
        {
            evento.Invoke();
        }
    }

    #region UpToUnit
    #region Dart
    [Header("Dart")]
    public float newHPReg1;
    public float newMoveSpeed1;
    public float newHp1;
    public float newArmor1;
    public float newRange1;
    public float newDmg1, newReload1;
    public bool ad1;
    public void ForUnitDart(ref float _newHPReg, ref float _newMoveSpeed, ref float _newHp,
        ref float _newArmor, ref float _newRange, ref float _newDmg, ref float _newReload, ref bool _ad)
    {
        _newHPReg += newHPReg1;
        _newMoveSpeed += newMoveSpeed1;
        _newHp += newHp1;
        _newArmor += newArmor1;
        _newRange += newRange1;
        _newDmg += newDmg1;
        _newReload += newReload1;
        _ad = ad1;
    }
    #endregion

    #region Javeling
    [Header("Javeling")]
    public float newHPReg2;
    public float newMoveSpeed2;
    public float newHp2;
    public float newArmor2;
    public float newRange2;
    public float newDmg2, newReload2;
    public bool ad2;
    public void ForUnitJaveling(ref float _newHPReg, ref float _newMoveSpeed, ref float _newHp,
       ref float _newArmor, ref float _newRange, ref float _newDmg, ref float _newReload, ref bool _ad)
    {
        _newHPReg += newHPReg2;
        _newMoveSpeed += newMoveSpeed2;
        _newHp += newHp2;
        _newArmor += newArmor2;
        _newRange += newRange2;
        _newDmg += newDmg2;
        _newReload = newReload2;
        _ad = ad2;
    }
    #endregion

    #region Mace
    [Header("Mace")]
    public float newHPReg3;
    public float newMoveSpeed3;
    public float newHp3;
    public float newArmor3;
    public float newRange3;
    public float newDmg3, newReload3;
    public bool ad3;
    public void ForUnitMace(ref float _newHPReg, ref float _newMoveSpeed, ref float _newHp,
       ref float _newArmor, ref float _newRange, ref float _newDmg, ref float _newReload, ref bool _ad)
    {
        _newHPReg += newHPReg3;
        _newMoveSpeed += newMoveSpeed3;
        _newHp += newHp3;
        _newArmor += newArmor3;
        _newRange += newRange3;
        _newDmg += newDmg3;
        _newReload += newReload3;
        _ad = ad3;
    }
    #endregion

    #region Gladius
    [Header("Gladius")]
    public float newHPReg4;
    public float newMoveSpeed4;
    public float newHp4;
    public float newArmor4;
    public float newRange4;
    public float newDmg4, newReload4;
    public bool ad4;

    public void ForUnitGladius(ref float _newHPReg, ref float _newMoveSpeed, ref float _newHp,
      ref float _newArmor, ref float _newRange, ref float _newDmg, ref float _newReload, ref bool _ad)
    {
        _newHPReg += newHPReg4;
        _newMoveSpeed += newMoveSpeed4;
        _newHp += newHp4;
        _newArmor += newArmor4;
        _newRange += newRange4;
        _newDmg += newDmg4;
        _newReload += newReload4;
        _ad = ad4;
    }
    #endregion
    #endregion
}
