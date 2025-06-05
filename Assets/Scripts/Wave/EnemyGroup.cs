using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/CreateEnemyGroup")]
public class EnemyGroup : ScriptableObject
{
    [SerializeField]
    public UnitData enemy;

    [SerializeField]
    public int numberOfEnemy = 0;

    [SerializeField]
    public int unitID = 0;
}
