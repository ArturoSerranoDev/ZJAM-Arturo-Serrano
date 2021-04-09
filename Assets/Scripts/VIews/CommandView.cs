using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandView : MonoBehaviour
{
    public DragDrop commandDragDrop;
    public CommandSlot commandSlot;



    public delegate void OnCommandPicked(CommandView command);
    public event OnCommandPicked onCommandPicked;
    public delegate void OnCommandDropped(CommandView command);
    public event OnCommandDropped onCommandDropped;

    public Command command;

    private void Awake()
    {
        commandDragDrop.onPicked += OnPicked;
        commandDragDrop.onDropped += OnDropped;
    }

    public void OnPicked()
    {
        onCommandPicked?.Invoke(this);

        Debug.Log("OnPicked");
    }

    public void OnDropped()
    {
        onCommandDropped?.Invoke(this);

        Debug.Log("OnDropped");

    }
}
