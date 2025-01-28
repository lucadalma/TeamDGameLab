using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField]
    List<WavesSO> waves;

    [SerializeField]
    GameEvent increaseWaveNumber;

    int currentWave = 0;

    int numberOfWaves = 0;

    public Transform targetPoint;  // Punto finale per le unità

    GameObject waveParent;

    [SerializeField]
    FloatVariable timeBetweenSpawn;

    [SerializeField]
    IntVariable difficulty;

    WavesSO currentSetOfWave;


    private void Start()
    {
        StartCoroutine(ManageWaves());
    }

    //private void OnValidate()
    //{
    //    if (waves.Count != timeBetweenWaves.Count)
    //    {
    //        while (timeBetweenWaves.Count < waves.Count)
    //        {
    //            timeBetweenWaves.Add(10f);
    //        }

    //        while (timeBetweenWaves.Count > waves.Count)
    //        {
    //            timeBetweenWaves.RemoveAt(timeBetweenWaves.Count - 1);
    //        }
    //    }
    //}

    private IEnumerator ManageWaves()
    {
        while (true)
        {
            if (difficulty.Value >= waves.Count) difficulty.Value = waves.Count - 1;
            currentSetOfWave = waves[difficulty.Value];

            if (currentWave >= currentSetOfWave.waves.Count) currentWave = 0;

            numberOfWaves += 1;

            Debug.Log($"Inizio wave {numberOfWaves}");
            yield return StartCoroutine(SpawnWave(currentWave));

            if (currentWave < currentSetOfWave.timeBetweenWaves.Count)
            {
                float delay = currentSetOfWave.timeBetweenWaves[currentWave];
                Debug.Log($"Attesa di {delay} secondi prima della prossima wave.");
                yield return new WaitForSeconds(delay);
            }

            currentWave++;
        }
    }

    private IEnumerator SpawnWave(int waveIndex)
    {
        WaveSO currentWaveSO = currentSetOfWave.waves[waveIndex];


        int spawnPointIndex = 0;

        foreach (EnemyGroup currentEnemyGroup in currentWaveSO.enemyGroup)
        {
            for (int i = 0; i < currentEnemyGroup.numberOfEnemy; i++)
            {
                GameObject unit = Instantiate(currentEnemyGroup.enemy.unitPrefab, currentWaveSO.spawnPoint[spawnPointIndex].transform.position, Quaternion.identity);
                EnemyBehaviour behavior = unit.GetComponent<EnemyBehaviour>();
                if (behavior != null)
                {
                    behavior.Initialize(currentEnemyGroup.enemy, targetPoint);
                }
                yield return new WaitForSeconds(timeBetweenSpawn.Value);
            }
            spawnPointIndex++;
        }

    }

}
