using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI textRef;

    public void OnPointerEnter(PointerEventData eventData)
    {
        textRef.color = new Color(255, 255, 0, 255); // Change back to yellow
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textRef.color = new Color(255, 255, 255, 255); // Change to white
    }
}

