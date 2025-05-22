using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DestroyAbilities : MonoBehaviour
{
    float timer;
    public float timerDestruction;
    public bool missili;
    public GameObject Missili;


    int count;
    private void Update()
    {
        timer += 1 * Time.deltaTime;


        if (timer >= 1 && missili == true && count <= 4)
        {
            if (Missili != null)
                SpawnSphere();
            count++;
            timer = 0;
        }



        if (timer >= timerDestruction && missili == false || count >= 4 && missili == true)
        {
            Destroy(this.gameObject);
        }


    }


    public Vector2 spawnAreaSize;
    public float spawnHeight;


    void SpawnSphere()
    {
        Vector3 spawnPos = transform.position + new Vector3(
        Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
        spawnHeight,
        Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
    );

        GameObject sphere = Instantiate(Missili, spawnPos, Quaternion.identity);
    }

}
