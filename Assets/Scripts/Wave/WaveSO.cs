using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave/Create Single Wave")]
public class WaveSO : ScriptableObject
{
    [SerializeField]
    public List<EnemyGroup> enemyGroup;

    [SerializeField]
    public List<GameObject> spawnPoint;

    private void OnValidate()
    {
        if (enemyGroup.Count != spawnPoint.Count)
        {
            while (spawnPoint.Count < enemyGroup.Count)
            {
                spawnPoint.Add(null);
            }

            while (spawnPoint.Count > enemyGroup.Count)
            {
                spawnPoint.RemoveAt(spawnPoint.Count - 1);
            }
        }
    }
}
