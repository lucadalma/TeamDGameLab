using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissiliEsplosione : MonoBehaviour
{
    public float fallSpeed;
    public float timeEspolione;
    float timer;
    public GameObject Missile;
    public GameObject MissileNO;


    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        timer += 1 * Time.deltaTime;

        if (timer >= timeEspolione)
        {
            Missile.SetActive(true);
            MissileNO.SetActive(false);
        }

        if (timer >= timeEspolione + 0.1f)
            Destroy(this.gameObject);
    }
}
