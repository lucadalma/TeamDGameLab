using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyIncreaser : MonoBehaviour
{
    [SerializeField]
    public float interval = 2f;

    [SerializeField]
    IntVariable difficulty;

    [SerializeField]
    List<WavesSO> ListWaves;

    void Start()
    {
        difficulty.Value = 0;
        StartCoroutine(ChangeVariableEveryXSeconds());
    }

    private IEnumerator ChangeVariableEveryXSeconds()
    {
        while (true)
        {
            if (difficulty.Value >= ListWaves.Count) difficulty.Value = ListWaves.Count - 1;

            yield return new WaitForSeconds(interval);
            difficulty.Value++;
        }
    }
}
