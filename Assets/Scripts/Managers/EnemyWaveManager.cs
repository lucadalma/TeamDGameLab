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

    bool doneFirstTime;

    int currentWave = 0;

    public WaveSO nextWaveSO;

    public float nextWaveTimer;

    int numberOfWaves = 0;

    public Transform targetPoint;  // Punto finale per le unità

    public BoolVariable pause;

    GameObject waveParent;

    [SerializeField]
    FloatVariable timeBetweenSpawn;

    [SerializeField]
    List<Transform> enemySpawnPoints;

    [SerializeField]
    IntVariable difficulty;

    WavesSO currentSetOfWave;

    WaveTimer timerDisplay;

    float areaWidth = 5f;
    float areaLength = 5f;

    private void Start()
    {
        doneFirstTime = false;
        timerDisplay = FindObjectOfType<WaveTimer>();
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

            if (doneFirstTime == false)
            {
                nextWaveSO = currentSetOfWave.waves[0];
                nextWaveTimer = currentSetOfWave.timeBetweenWaves[0];
                timerDisplay.setTimer(nextWaveTimer);
                doneFirstTime = true;
            }
            if (currentWave + 1 >= currentSetOfWave.waves.Count)
            {
                nextWaveSO = currentSetOfWave.waves[0];
                nextWaveTimer = currentSetOfWave.timeBetweenWaves[0];
            }
            else
            {
                nextWaveSO = currentSetOfWave.waves[currentWave + 1];
                nextWaveTimer = currentSetOfWave.timeBetweenWaves[currentWave + 1];
            }

            while (pause.Value)
            {
                yield return null;
            }


            if (currentWave < currentSetOfWave.timeBetweenWaves.Count)
            {
                float delay = currentSetOfWave.timeBetweenWaves[currentWave];
                yield return StartCoroutine(WaitWhileNotPaused(delay));
            }

            yield return StartCoroutine(SpawnWave(currentWave));

            currentWave++;
        }
    }

    private IEnumerator SpawnWave(int waveIndex)
    {
        WaveSO currentWaveSO = currentSetOfWave.waves[waveIndex];

        //nextWaveSO = currentSetOfWave.waves[waveIndex + 1];


        int spawnPointIndex = 0;

        foreach (EnemyGroup currentEnemyGroup in currentWaveSO.enemyGroup)
        {
            for (int i = 0; i < currentEnemyGroup.numberOfEnemy; i++)
            {
                float offsetX = Random.Range(-areaWidth / 2, areaWidth / 2);
                float offsetZ = Random.Range(-areaLength / 2, areaLength / 2);

                Vector3 spawnPosition = Vector3.zero;
                if (currentWaveSO.spawnPoint[spawnPointIndex] == SpawnPoint.Lane1)
                {
                    spawnPosition = enemySpawnPoints[0].position + new Vector3(offsetX, 0, 0);
                }
                else if (currentWaveSO.spawnPoint[spawnPointIndex] == SpawnPoint.Lane2)
                {
                    spawnPosition = enemySpawnPoints[1].position + new Vector3(offsetX, 0, 0);

                }
                else if (currentWaveSO.spawnPoint[spawnPointIndex] == SpawnPoint.Lane3)
                {
                    spawnPosition = enemySpawnPoints[2].position + new Vector3(offsetX, 0, 0);

                }

                spawnPosition.z = EnemyBase.position.z + offsetZ;

                GameObject unit = Instantiate(currentEnemyGroup.enemy.unitPrefab, spawnPosition, Quaternion.Euler(0, 180, 0));
                TanksBehavior behavior = unit.GetComponent<TanksBehavior>();
                if (behavior != null)
                {
                    behavior.Initialize(currentEnemyGroup.enemy, targetPoint);
                    behavior.isEnemy = true;
                    if (currentWaveSO.spawnPoint[spawnPointIndex] == SpawnPoint.Lane1)
                    {
                        behavior.lane = Lane.Lane3;
                    }
                    else if (currentWaveSO.spawnPoint[spawnPointIndex] == SpawnPoint.Lane2)
                    {
                        behavior.lane = Lane.Lane2;
                    }
                    else
                    {
                        behavior.lane = Lane.Lane1;
                    }
                }
            }
            spawnPointIndex++;
        }

        timerDisplay.setTimer(nextWaveTimer);

        yield return StartCoroutine(WaitWhileNotPaused(timeBetweenSpawn.Value));

    }


    private IEnumerator WaitWhileNotPaused(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            if (!pause.Value)
            {
                timer += Time.deltaTime;
            }
            yield return null;
        }
    }

}
