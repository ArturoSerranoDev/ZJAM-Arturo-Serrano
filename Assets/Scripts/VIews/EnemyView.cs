using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyView : MonoBehaviour
{
    public List<Command> enemyCommands = new List<Command>();

    public Vector3 startingPos;

    public EnemyLoopType enemyLoopType = EnemyLoopType.Reverse;
    public int turn;
    public float animSpeed;

    //public Command GetNextCommandAction()
    //{
    //    //if end, reverse commands and execute
    //    if (turn >= enemyCommands.Count)
    //    {
    //        enemyCommands.Reverse();
    //        turn = 0;
    //    }

    //    return enemyCommands[turn];
    //}

    public IEnumerator ExecuteCommand()
    {
        // If shooter, check if player is in range and do animation

        // set player to destroyed

        // break




        Command turnCommand = enemyCommands[turn];

        Tween tweenAction;
        Debug.Log(turnCommand.GetCommandType());

        switch (turnCommand.GetCommandType())
        {
            case CommandType.MoveUp:
            case CommandType.MoveBack:
       
                Move moveCommand = turnCommand as Move;
                tweenAction = transform.DOMove(transform.position + (moveCommand.dir * transform.forward), 0.5f * animSpeed);

                yield return tweenAction.WaitForCompletion();
                break;
            case CommandType.RotateRigth:
            case CommandType.RotateLeft:
                Rotate rotateCommand = turnCommand as Rotate;
                tweenAction = transform.DORotate(transform.eulerAngles + new Vector3(0, 90 * rotateCommand.rotDir, 0), 0.5f * animSpeed);

                yield return tweenAction.WaitForCompletion();
                break;
        }


        turn++;
        //if end, reverse commands and execute
        if (turn >= enemyCommands.Count)
        {
            if (enemyLoopType == EnemyLoopType.Reverse)
                enemyCommands.Reverse();

            turn = 0;
        }

    }

    public void Destroy()
    {

    }

    public void Reset()
    {
        turn = 0;
        enemyCommands.Clear();
    }
}
