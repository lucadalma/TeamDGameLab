using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/CreateEnemyGroup")]
public class EnemyGroup : ScriptableObject
{
    [SerializeField]
    public GameObject enemy;

    [SerializeField]
    public int numberOfEnemy = 0;
}
