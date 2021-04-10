using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TweenAction spawnTween;
    public TweenAction moveTween;
    public TweenAction rotateTween;
    public TweenAction useTween;
    public TweenAction finishTween;


    public IEnumerator ExecuteCommand(CommandView commandView)
    {
        Command command = commandView.command;

        Tween tweenAction;
        switch (command.GetCommandType())
        {
            case CommandType.MoveUp:
            case CommandType.MoveBack:
                // Check if pos is available

                // if not, play stuck anim
                if (!IsNextTileValidMove(commandView.commandType))
                {
                    tweenAction = transform.DOShakePosition(0.5f);

                    yield return tweenAction.WaitForCompletion();
                    break;
                }

                // Else move
                Move moveCommand = command as Move;
                tweenAction = transform.DOMove(transform.position + (moveCommand.dir * transform.forward), 0.5f);

                yield return tweenAction.WaitForCompletion();
                break;
            case CommandType.RotateRigth:
            case CommandType.RotateLeft:
                Rotate rotateCommand = command as Rotate;
                tweenAction = transform.DORotate(transform.eulerAngles + new Vector3(0, 90 * rotateCommand.rotDir, 0), 0.5f);

                yield return tweenAction.WaitForCompletion();
                break;
        }

    }

    public bool IsNextTileValidMove(CommandType command) 
    {
        RaycastHit hit;
        int layerMask = 1 << 8;
        int dir = command == CommandType.MoveUp ? 1 : -1;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.forward * dir, out hit, 1f, layerMask))
            if (hit.collider != null)
            {
                Debug.Log("IsNextTileValidMove false");
                return false;
            }
            else
            {
                Debug.Log("IsNextTileValidMove true ");
                return true;

            }

        return true;
    }
}
