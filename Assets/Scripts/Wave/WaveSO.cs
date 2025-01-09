using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave/Create Wave")]
public class WaveSO : ScriptableObject
{
    [SerializeField]
    List<EnemyGroup> wave;

    [SerializeField]
    int timeBetweenWaves = 0;
}
