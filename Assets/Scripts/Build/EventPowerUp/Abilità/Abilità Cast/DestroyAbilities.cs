using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DestroyAbilities : MonoBehaviour
{
    float timer;
    private void Update()
    {
        timer += 1 * Time.deltaTime;

        if (timer >= 0.5f)
        {
            Destroy(this.gameObject);
        }
    }

}
