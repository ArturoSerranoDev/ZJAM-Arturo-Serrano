using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CommandsController : MonoBehaviour
{
    public List<CommandView> availableCommands = new List<CommandView>();
    public List<CommandView> selectedCommands = new List<CommandView>();

    public GameObject selectedCommandGO;

    void Awake()
    {
        foreach (CommandView commandView in availableCommands)
        {
            commandView.onCommandPicked += OnCommandPicked;
            commandView.onCommandDropped += OnCommandDropped;
        }
    }

    public void OnCommandPicked(CommandView commandView)
    {
        selectedCommandGO = commandView.gameObject;
        Debug.Log("CommandsController: OnCommandPicked");
    }

    public void OnCommandDropped(CommandView commandView)
    {
        if(commandView.commandSlot == null)
        {
            Debug.LogWarning("Never a commandviewshould be empty of commandslot");
        }

        if (commandView.commandSlot.slotType == SlotType.Input && !availableCommands.Contains(commandView))
        {
            selectedCommands.Remove(commandView);
            availableCommands.Add(commandView);
        }
        else if (commandView.commandSlot.slotType == SlotType.Selected && !selectedCommands.Contains(commandView))
        {
            selectedCommands.Add(commandView);
            availableCommands.Remove(commandView);
        }
        else
        {
            // If not, tween to previous position
            selectedCommandGO.transform.DOMove(commandView.commandSlot.transform.position, 0.5f);
        }

    

        selectedCommandGO = null;
        Debug.Log("CommandsController: OnCommandDropped");

    }
}
