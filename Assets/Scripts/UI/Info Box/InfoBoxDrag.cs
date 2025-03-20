using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class InfoBoxDrag : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    Vector3 MoveOffset;
    Transform ItemBoxFolder;

    public void OnBeginDrag(PointerEventData eventData)
    {
        MoveOffset = transform.parent.position - Input.mousePosition;
        ItemBoxFolder = transform.parent.transform.parent;
        transform.parent.SetSiblingIndex(ItemBoxFolder.childCount - 1);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.parent.position = Input.mousePosition + MoveOffset;
    }
}
