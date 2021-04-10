using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CommandsController : MonoBehaviour
{
    public List<CommandView> availableCommands = new List<CommandView>();
    public List<CommandView> selectedCommands = new List<CommandView>();
    public List<int> selectedCommandsIndex = new List<int>();

    public List<CommandSlot> selectedCommandsSlots = new List<CommandSlot>();


    Dictionary<int, CommandView> commandByIndex = new Dictionary<int, CommandView>();
    Dictionary<int, CommandView> commandByIndexSorted = new Dictionary<int, CommandView>();


    public GameObject selectedCommandGO;

    void Awake()
    {
        foreach (CommandView commandView in availableCommands)
        {
            commandView.onCommandPicked += OnCommandPicked;
            commandView.onCommandDropped += OnCommandDropped;
        }
    }

    public Dictionary<int, CommandView> GetCommandsByIndex()
    {
        commandByIndex.Clear();
        commandByIndexSorted.Clear();

        foreach (CommandView commandView in selectedCommands)
        {
            commandByIndex[commandView.commandSlot.index] = commandView;

            Debug.Log("CommandsIndex: At index " + commandView.commandSlot.index + " is the command " + commandView.command.GetCommandType().ToString());
        }

        selectedCommandsIndex.Sort();

        for (int i = 0; i < selectedCommandsIndex.Count; i++)
        {
            commandByIndexSorted[i] = commandByIndex[selectedCommandsIndex[i]];
            commandByIndexSorted[i].commandSlot = selectedCommandsSlots[i];

            // Move to right pos
            if (i != selectedCommandsIndex[i])
            {
                commandByIndexSorted[i].transform.DOMove(selectedCommandsSlots[i].transform.position, 0.5f);
            }
        }

        return commandByIndexSorted;
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
            selectedCommandsIndex.Remove(commandView.commandSlot.index);
            availableCommands.Add(commandView);
        }
        else if (commandView.commandSlot.slotType == SlotType.Selected && !selectedCommands.Contains(commandView))
        {
            selectedCommands.Add(commandView);
            selectedCommandsIndex.Add(commandView.commandSlot.index);
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

    public void SortInputIfNeeded()
    {
        selectedCommandsIndex.Sort();

        int lastIndex = 0;
        int difference = 0;
        for (int i = 0; i < selectedCommandsIndex[selectedCommandsIndex.Count - 1]; i++)
        {
            while (!selectedCommandsIndex.Contains(lastIndex + difference))
            {
                difference++;
            }

            commandByIndexSorted[i] = commandByIndex[i + difference];
            commandByIndexSorted[i].commandSlot = selectedCommandsSlots[i];

            // Move to right pos
            if (difference != 0)
            {
                commandByIndexSorted[i].transform.DOMove(selectedCommandsSlots[i].transform.position, 0.5f);
            }

            lastIndex = i;
            difference = 0;
        }
    }
}
