using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/CreateEnemyGroup")]
public class EnemyGroup : ScriptableObject
{
    //[SerializeField]
    //Enemy enemy;

    [SerializeField]
    int numberOfEnemy = 0;
}
