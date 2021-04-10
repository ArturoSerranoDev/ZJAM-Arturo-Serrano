using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType { Input, Selected}

public class CommandSlot : MonoBehaviour
{
    public int index;

    public SlotType slotType;
    public CommandType itemCommandType = CommandType.None;
    
}
