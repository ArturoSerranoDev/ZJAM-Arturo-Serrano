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
    }

    public void AddCommand(string commandType)
    {
        if (commands.Count >= selectedCommandsSlots.Count)
            return;



        GameObject newCommand = null;
        switch (commandType)
        {
            case "MoveUp":
                newCommand = PoolManager.Instance.Spawn(moveUpCommandPrf,
                    selectedCommandsSlots[commands.Count].transform.position, Quaternion.identity, commandsParent.transform);
                break;
            case "MoveDown":
                newCommand = PoolManager.Instance.Spawn(moveDownCommandPrf,
                    selectedCommandsSlots[commands.Count].transform.position, Quaternion.identity, commandsParent.transform);
                break;

            case "RotLeft":
                newCommand = PoolManager.Instance.Spawn(RotateLeftCommandPrf,
                    selectedCommandsSlots[commands.Count].transform.position, Quaternion.identity, commandsParent.transform);
                break;

            case "RotRigth":
                newCommand = PoolManager.Instance.Spawn(rotateRigthCommandPrf,
                    selectedCommandsSlots[commands.Count].transform.position, Quaternion.identity, commandsParent.transform);
                break;

            case "Use":
                newCommand = PoolManager.Instance.Spawn(useCommandPrf,
                    selectedCommandsSlots[commands.Count].transform.position, Quaternion.identity, commandsParent.transform);
                break;

            case "Finish":
                newCommand = PoolManager.Instance.Spawn(finishCommandPrf,
                    selectedCommandsSlots[commands.Count].transform.position, Quaternion.identity, commandsParent.transform);
                break;

        }

        newCommand.GetComponent<RectTransform>().position = selectedCommandsSlots[commands.Count].GetComponent<RectTransform>().position;



        if (LevelController.Instance.currentLevel == 1 && commandType == "MoveUp")
        {
            int distance = LevelController.Instance.playerController.GetDistanceToForwardCollider();

            for (int i = 0; i < distance; i++)
            {
                commands.Add(newCommand.GetComponent<CommandView>());
            }
            return;
        }
        else
            commands.Add(newCommand.GetComponent<CommandView>());
    }

    public void RemoveLastCommand()
    {
        // Remove command animation

        // Remove last
        PoolManager.Instance.Despawn(commands[commands.Count - 1].gameObject);
        commands.RemoveAt(commands.Count - 1);

    }

    public void InsertCheatcode()
    {
        int level = LevelController.Instance.currentLevel;

        foreach (CommandView command in commands)
        {
            PoolManager.Instance.Despawn(command.gameObject);
        }
        commands.Clear();

        foreach (string cheatInput in LevelController.Instance.levelData.cheatCode)
        {
            AddCommand(cheatInput);
        }


    }




}
