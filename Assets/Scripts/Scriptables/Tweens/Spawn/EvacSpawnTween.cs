using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine;


public class EvacSpawnTween : TweenAction
{
    public override IEnumerator ExecuteTween()
    {
        tweenAction = target.transform.DOScale(1, 0.1f).From(0);
        target.transform.DORotate(new Vector3(-90, 360, 0), 0.25f);

        yield return tweenAction.WaitForCompletion();

        Debug.Log("Tween Completed");
    }
}
