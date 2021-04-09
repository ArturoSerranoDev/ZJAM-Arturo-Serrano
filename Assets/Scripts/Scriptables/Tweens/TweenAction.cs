using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public abstract class TweenAction : MonoBehaviour
{
    public Transform target;
    public Tween tweenAction;
    public abstract IEnumerator ExecuteTween();
}

