using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField]
    List<WaveSO> waves;

    [SerializeField]
    List<float> timeBetweenWaves;

    [SerializeField]
    GameEvent increaseWaveNumber;

    int currentWave = 0;

    int numberOfWaves = 0;

    GameObject waveParent;

    private void Start()
    {
        StartCoroutine(ManageWaves());
    }

    private void OnValidate()
    {
        if (waves.Count != timeBetweenWaves.Count)
        {
            while (timeBetweenWaves.Count < waves.Count)
            {
                timeBetweenWaves.Add(10f);
            }

            while (timeBetweenWaves.Count > waves.Count)
            {
                timeBetweenWaves.RemoveAt(timeBetweenWaves.Count - 1);
            }
        }
    }

    private IEnumerator ManageWaves()
    {
        while (true)
        {
            if (currentWave >= waves.Count) currentWave = 0;

            numberOfWaves += 1;

            Debug.Log($"Inizio wave {numberOfWaves}");
            yield return StartCoroutine(SpawnWave(currentWave));

            if (currentWave < timeBetweenWaves.Count)
            {
                float delay = timeBetweenWaves[currentWave];
                Debug.Log($"Attesa di {delay} secondi prima della prossima wave.");
                yield return new WaitForSeconds(delay);
            }

            currentWave++;
        }
    }

    private IEnumerator SpawnWave(int waveIndex)
    {
        WaveSO currentWaveSO = waves[waveIndex];


        int spawnPointIndex = 0;

        foreach (EnemyGroup currentEnemyGroup in currentWaveSO.enemyGroup)
        {
            for (int i = 0; i < currentEnemyGroup.numberOfEnemy; i++)
            {
                Instantiate(currentEnemyGroup.enemy, currentWaveSO.spawnPoint[spawnPointIndex].transform.position, Quaternion.identity);
            }
            spawnPointIndex++;
        }

        yield return new WaitForSeconds(0);
    }

}
