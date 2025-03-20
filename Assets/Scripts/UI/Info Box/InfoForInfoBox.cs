using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class InfoForInfoBox : MonoBehaviour
{
    [SerializeField] string infoName = "Insert Name here";
    [SerializeField] string infoDescription = "Insert Description Here";
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
