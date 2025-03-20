using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoForInfoBox : MonoBehaviour
{
    [SerializeField] string infoName = "Insert Name here";
    [TextArea(14, 10)] [SerializeField] string infoDescription = "insert Description Here";
    [SerializeField] Sprite infoIcon;

    private void Start()
    {
        if (infoIcon == null)
        {
            infoIcon = Resources.Load<Sprite>("placeholder_icon_missing");
        }

    }


    public string InfoName { get { return infoName; } }
    public string InfoDescription { get { return infoDescription; } }
    public Sprite InfoIcon { get { return infoIcon; } }
}
