using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave/Create List of Waves")]
public class WavesSO : ScriptableObject
{
    public List<WaveSO> waves;

    [SerializeField]
    public List<float> timeBetweenWaves;

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
}
