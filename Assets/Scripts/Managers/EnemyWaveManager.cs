using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField]
    List<WavesSO> waves;

    [SerializeField]
    GameEvent increaseWaveNumber;

    [SerializeField]
    Transform EnemyBase;

    int currentWave = 0;

    public WaveSO nextWaveSO;

    int numberOfWaves = 0;

    public Transform targetPoint;  // Punto finale per le unità

    public BoolVariable pause;

    GameObject waveParent;

    [SerializeField]
    FloatVariable timeBetweenSpawn;

    [SerializeField]
    IntVariable difficulty;

    WavesSO currentSetOfWave;

    float areaWidth = 5f;
    float areaLength = 5f;

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

            while (pause.Value)
            {
                yield return null;
            }

            //Debug.Log($"Inizio wave {numberOfWaves}");
            yield return StartCoroutine(SpawnWave(currentWave));

            if (currentWave < currentSetOfWave.timeBetweenWaves.Count)
            {
                float delay = currentSetOfWave.timeBetweenWaves[currentWave];
                // Debug.Log($"Attesa di {delay} secondi prima della prossima wave.");
                yield return new WaitForSeconds(delay);
            }

            currentWave++;
        }
    }

    private IEnumerator SpawnWave(int waveIndex)
    {
        WaveSO currentWaveSO = currentSetOfWave.waves[waveIndex];
        nextWaveSO = currentSetOfWave.waves[waveIndex + 1];

        int spawnPointIndex = 0;

        foreach (EnemyGroup currentEnemyGroup in currentWaveSO.enemyGroup)
        {
            for (int i = 0; i < currentEnemyGroup.numberOfEnemy; i++)
            {
                float offsetX = Random.Range(-areaWidth / 2, areaWidth / 2);
                float offsetZ = Random.Range(-areaLength / 2, areaLength / 2);
                Vector3 spawnPosition = currentWaveSO.spawnPoint[spawnPointIndex].transform.position + new Vector3(offsetX, 0, 0);

                spawnPosition.z = EnemyBase.position.z + offsetZ;

                GameObject unit = Instantiate(currentEnemyGroup.enemy.unitPrefab, spawnPosition, Quaternion.Euler(0, 180, 0));
                TanksBehavior behavior = unit.GetComponent<TanksBehavior>();
                if (behavior != null)
                {
                    behavior.Initialize(currentEnemyGroup.enemy, targetPoint);
                    behavior.isEnemy = true;
                }
            }
            spawnPointIndex++;
        }
        yield return new WaitForSeconds(timeBetweenSpawn.Value);
    }


}
