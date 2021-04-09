using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CommandsController : MonoBehaviour
{
    public List<CommandView> availableCommands = new List<CommandView>();
    public List<CommandView> selectedCommands = new List<CommandView>();

    public GameObject selectedCommand;

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
        selectedCommand = commandView.gameObject;
        Debug.Log("CommandsController: OnCommandPicked");
    }

    public void OnCommandDropped(CommandView commandView)
    {
        // If it has item slot set it there

        // If not, tween to previous position
        selectedCommand.transform.DOMove(commandView.commandSlot.transform.position, 0.5f);

        selectedCommand = null;
        Debug.Log("CommandsController: OnCommandDropped");

    }
}
