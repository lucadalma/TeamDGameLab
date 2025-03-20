using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBox : MonoBehaviour
{
    [SerializeField] Image ItemIcon;
    [SerializeField] TextMeshProUGUI ItemName;
    [SerializeField] TextMeshProUGUI ItemDescription;

    public void CloseItemBox() 
    {
        Destroy(gameObject);
    }

    public void SetItemBox(Sprite itemIconSprite, string itemNameString, string itemDescriptionString)
    {
        ItemIcon.sprite = itemIconSprite;
        ItemName.text = itemNameString;
        ItemDescription.text = itemDescriptionString;
    }
}
