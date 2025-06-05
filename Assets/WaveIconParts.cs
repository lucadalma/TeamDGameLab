using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveIconParts : MonoBehaviour
{
    [NonSerialized] public int unitID;
    [NonSerialized] public int groupsInWave = 0;


    public Image iconSprite;

    public Image leftArrow;
    public Image topArrow;
    public Image rightArrow;

    public TextMeshProUGUI waveCount;

}
