using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnPoint 
{
    Lane1,
    Lane2,
    Lane3,
    None
}

[CreateAssetMenu(menuName = "Wave/Create Single Wave")]
public class WaveSO : ScriptableObject
{
    [SerializeField]
    public List<EnemyGroup> enemyGroup;

    [SerializeField]
    public List<SpawnPoint> spawnPoint;

    private void OnValidate()
    {
        if (enemyGroup.Count != spawnPoint.Count)
        {
            while (spawnPoint.Count < enemyGroup.Count)
            {
                spawnPoint.Add(SpawnPoint.None);
            }

            while (spawnPoint.Count > enemyGroup.Count)
            {
                spawnPoint.RemoveAt(spawnPoint.Count - 1);
            }
        }
    }
}
