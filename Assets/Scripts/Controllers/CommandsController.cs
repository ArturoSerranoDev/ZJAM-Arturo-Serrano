using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CommandsController : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject moveUpCommandPrf;
    public GameObject moveDownCommandPrf;
    public GameObject rotateRigthCommandPrf;
    public GameObject RotateLeftCommandPrf;
    public GameObject pickCommandPrf;
    public GameObject useCommandPrf;
    public GameObject finishCommandPrf;

    public GameObject commandsParent;

    public List<CommandView> commands = new List<CommandView>();
    public List<CommandSlot> selectedCommandsSlots = new List<CommandSlot>();

    public GameObject selectedCommandGO;

    void Awake()
    {
        //foreach (CommandView commandView in availableCommands)
        //{
        //    commandView.onCommandPicked += OnCommandPicked;
        //    commandView.onCommandDropped += OnCommandDropped;
        //}
    }

    public void AddCommand(CommandType commandType)
    {
        GameObject newCommand = null;
        switch (commandType)
        {
            case CommandType.MoveUp:
                newCommand = PoolManager.Instance.Spawn(moveUpCommandPrf,
                    selectedCommandsSlots[selectedCommandsSlots.Count].transform.position, Quaternion.identity, commandsParent.transform);
                break;
            case CommandType.MoveBack:
                newCommand = PoolManager.Instance.Spawn(moveDownCommandPrf,
                    selectedCommandsSlots[selectedCommandsSlots.Count].transform.position, Quaternion.identity, commandsParent.transform);
                break;

            case CommandType.RotateLeft:
                newCommand = PoolManager.Instance.Spawn(RotateLeftCommandPrf,
                    selectedCommandsSlots[selectedCommandsSlots.Count].transform.position, Quaternion.identity, commandsParent.transform);
                break;

            case CommandType.RotateRigth:
                newCommand = PoolManager.Instance.Spawn(rotateRigthCommandPrf,
                    selectedCommandsSlots[selectedCommandsSlots.Count].transform.position, Quaternion.identity, commandsParent.transform);
                break;

            case CommandType.Use:
                newCommand = PoolManager.Instance.Spawn(useCommandPrf,
                    selectedCommandsSlots[selectedCommandsSlots.Count].transform.position, Quaternion.identity, commandsParent.transform);
                break;

            case CommandType.Pick:
                newCommand = PoolManager.Instance.Spawn(moveUpCommandPrf,
                    selectedCommandsSlots[selectedCommandsSlots.Count].transform.position, Quaternion.identity, commandsParent.transform);
                break;

                break;
            case CommandType.Finish:
                newCommand = PoolManager.Instance.Spawn(finishCommandPrf,
                    selectedCommandsSlots[selectedCommandsSlots.Count].transform.position, Quaternion.identity, commandsParent.transform);
                break;

        }

        commands.Add(newCommand.GetComponent<CommandView>());
    }

    public void RemoveLastCommand()
    {
        // Remove command animation

        // Remove last
        PoolManager.Instance.Despawn(commands[commands.Count - 1].gameObject);
        commands.RemoveAt(commands.Count - 1);

    }




}
