using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class DialogController : MonoBehaviour
{
    public GameObject dialogPanel;

    public Image leftSpeakerImage;
    public Image rigthSpeakerImage;

    public TextMeshProUGUI dialogText;
    public DialogData dialogData;

    public int dialogIndex;


    public delegate void OnDialogCompleted();
    public event OnDialogCompleted onDialogCompleted;

    public SpeakerType currentSpeaker;
    public Color speakerColor;
    public Color listenerColor;

    bool tweening;
    public void OnEnable()
    {
        //LevelController.Instance.onGameStart += OnGameStart;
        //LevelController.Instance.onGamePaused += OnGameStart;
        //LevelController.Instance.onGameWon += OnGameStart;
        //LevelController.Instance.onGameLost += OnGameStart;
        LevelController.Instance.onLevelLoaded += StartDialog;

    }

    public void StartDialog()
    {
        dialogPanel.SetActive(true);
        dialogIndex = 0;

        ShowNextDialog();
    }

    public void ShowNextDialog()
    {
        if (tweening)
            return;

        if (dialogIndex >= dialogData.levelDialog.Count)
        {
            dialogPanel.SetActive(false);

            onDialogCompleted?.Invoke();
            return;
        }

        currentSpeaker = dialogData.levelDialog[dialogIndex].speaker;

        // Show dialog typewriter
        StartCoroutine(ShowNextDialogCoroutine());

        dialogIndex += 1;
    }

    IEnumerator ShowNextDialogCoroutine()
    {
        Image speakerImage = currentSpeaker == SpeakerType.LeftSpeaker ? leftSpeakerImage : rigthSpeakerImage;
        Image nonSpeakerImage = currentSpeaker == SpeakerType.LeftSpeaker ? rigthSpeakerImage : leftSpeakerImage;

        leftSpeakerImage.color = currentSpeaker == SpeakerType.LeftSpeaker ? speakerColor : listenerColor;
        rigthSpeakerImage.color = currentSpeaker == SpeakerType.RightSpeaker ? speakerColor : listenerColor;

        Tween dialogTween = speakerImage.transform.DOMoveY(speakerImage.transform.position.y + 100f, 0.125f).SetLoops(2,LoopType.Yoyo);


        // dialog typewriter
        dialogText.text = dialogData.levelDialog[dialogIndex].text;

        yield return dialogTween.WaitForCompletion();

        tweening = false;
    }
}
