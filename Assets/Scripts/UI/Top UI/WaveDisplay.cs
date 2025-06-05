using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField] GameObject WaveIconTemplate;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] List<Transform> IconSlots;


    List<GameObject> currentIcons = new List<GameObject>();

    EnemyWaveManager wM;

    WaveSO wave;

    bool dartOnList = false, maceOnList = false, javelinOnList = false, gladiusOnList = false;

    private void Start()
    {
        wM = FindObjectOfType<EnemyWaveManager>();
    }

    public void ChangeDisplay()
    {
        wave = wM.nextWaveSO;

        ClearDisplay();

        SetImages();
        SetNumbers();
        SetDitesctions();
    }


    void ClearDisplay()
    {
        dartOnList = false;
        gladiusOnList = false;
        javelinOnList = false;
        maceOnList = false;

        foreach (GameObject obj in currentIcons)
        {
            Destroy(obj);
        }

        currentIcons.Clear();
    }

    void SetImages()
    {
        foreach (EnemyGroup group in wave.enemyGroup)
        {
            if (group.unitID == 0 && !dartOnList)
            {
                CreateWaveIcon(0);
                dartOnList = true;
            }
            else if (group.unitID == 1 && !gladiusOnList)
            {
                CreateWaveIcon(1);
                gladiusOnList = true;
            }
            else if (group.unitID == 2 && !javelinOnList)
            {
                CreateWaveIcon(2);
                javelinOnList = true;
            }
            else if (group.unitID == 3 && !maceOnList)
            {
                CreateWaveIcon(3);
                maceOnList = true;
            }
        }
    }

    void SetNumbers()
    {
        foreach (GameObject obj in currentIcons)
        {
            WaveIconParts parts = obj.GetComponent<WaveIconParts>();

            foreach (EnemyGroup group in wave.enemyGroup)
            {
                if (group.unitID == parts.unitID)
                {
                    parts.groupsInWave++;
                }
            }

            parts.waveCount.text = parts.groupsInWave.ToString();
        }
    }

    void SetDitesctions()
    {
        foreach (GameObject obj in currentIcons)
        {
            WaveIconParts parts = obj.GetComponent<WaveIconParts>();

            for (int i = 0; i < wave.enemyGroup.Count; i++)
            {
                EnemyGroup group = wave.enemyGroup[i];
                SpawnPoint lane = wave.spawnPoint[i];

                if (group.unitID == parts.unitID)
                {
                    switch (lane)
                    {
                        case SpawnPoint.Lane1:
                            if (parts.leftArrow.enabled == false)
                            {
                                parts.leftArrow.enabled = true;
                            }
                            break;
                        case SpawnPoint.Lane2:
                            if (parts.topArrow.enabled == false)
                            {
                                parts.topArrow.enabled = true;
                            }
                            break;
                        case SpawnPoint.Lane3:
                            if (parts.rightArrow.enabled == false)
                            {
                                parts.rightArrow.enabled = true;
                            }
                            break;
                        case SpawnPoint.None:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    void CreateWaveIcon(int ID)
    {
        GameObject temp;
        WaveIconParts parts;

        temp = Instantiate(WaveIconTemplate, IconSlots[currentIcons.Count]);
        currentIcons.Add(temp);
        parts = temp.GetComponent<WaveIconParts>();
        parts.unitID = ID;
        parts.iconSprite.sprite = sprites[ID];
    }

}
