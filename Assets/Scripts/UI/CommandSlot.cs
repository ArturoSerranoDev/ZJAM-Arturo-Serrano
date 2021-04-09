using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType { Input, Selected}

public class CommandSlot : MonoBehaviour, IDropHandler
{
    public int itemAmount;
    public SlotType slotType;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            eventData.pointerDrag.GetComponent<CommandView>().commandSlot = this;
        }
    }

}
