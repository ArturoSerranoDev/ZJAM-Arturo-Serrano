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
                Move moveCommand = command as Move;
                tweenAction = transform.DOMoveZ(transform.position.z + moveCommand.dir, 0.5f);

                yield return tweenAction.WaitForCompletion();
                break;
            case CommandType.RotateRigth:
            case CommandType.RotateLeft:
                Rotate rotateCommand = command as Rotate;
                tweenAction = transform.DORotate(new Vector3(0, 90 * rotateCommand.rotDir, 0), 0.5f);

                yield return tweenAction.WaitForCompletion();
                break;
        }
        yield return new WaitForEndOfFrame();
    }
}
