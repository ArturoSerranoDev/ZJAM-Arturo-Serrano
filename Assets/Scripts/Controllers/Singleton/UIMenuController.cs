using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class UIMenuController : MonoBehaviour
{
    public CanvasGroup StartingPanelCanvasGroup;
    public GameObject mainMenuPanel;
    public CanvasGroup mainMenuPanelCanvasGroup;
    public CanvasGroup levelSelectCanvasGroup;
    public CanvasGroup artGalleryCanvasGroup;

    public TextMeshProUGUI creditsText;
    public TextMeshProUGUI creditsMadeByText;

    public RectTransform fadeBlackRect;


    bool tweeningCredits;

    void Awake()
    {
        fadeBlackRect.gameObject.SetActive(false);

    }

    public void GoToMainMenu()
    {
        StartingPanelCanvasGroup.DOFade(0, 0.25f).From(1).SetEase(Ease.InOutSine).OnComplete(() => StartingPanelCanvasGroup.gameObject.SetActive(false));

        mainMenuPanel.SetActive(true);

        mainMenuPanelCanvasGroup.DOFade(1, 0.25f).From(0).SetDelay(0.25f).SetEase(Ease.InOutSine);
    }

    public void GoToMainMenuFromLevelSelect()
    {
        levelSelectCanvasGroup.DOFade(0, 0.25f).From(1).OnComplete(() => levelSelectCanvasGroup.gameObject.SetActive(false)).SetEase(Ease.InOutSine);

        mainMenuPanel.SetActive(true);

        mainMenuPanelCanvasGroup.DOFade(1, 0.25f).From(0).SetDelay(0.25f).SetEase(Ease.InOutSine);
    }

 

    public void GoToMainMenuFromArtGallery()
    {
        artGalleryCanvasGroup.DOFade(0, 0.25f).From(1).OnComplete(() => artGalleryCanvasGroup.gameObject.SetActive(false)).SetEase(Ease.InOutSine);

        mainMenuPanel.SetActive(true);

        mainMenuPanelCanvasGroup.DOFade(1, 0.25f).From(0).SetDelay(0.25f).SetEase(Ease.InOutSine);
    }

    public void GoToLevelSelect()
    {
        mainMenuPanelCanvasGroup.DOFade(0, 0.25f).From(1).OnComplete(() => mainMenuPanelCanvasGroup.gameObject.SetActive(false)).SetEase(Ease.InOutSine);

        levelSelectCanvasGroup.gameObject.SetActive(true);

        levelSelectCanvasGroup.DOFade(1, 0.25f).From(0).SetDelay(0.25f).SetEase(Ease.InOutSine);
    }

    public void GoToArtGallery()
    {
        mainMenuPanelCanvasGroup.DOFade(0, 0.25f).From(1).OnComplete(() => mainMenuPanelCanvasGroup.gameObject.SetActive(false)).SetEase(Ease.InOutSine);

        artGalleryCanvasGroup.gameObject.SetActive(true);

        artGalleryCanvasGroup.DOFade(1, 0.25f).From(0).SetDelay(0.25f).SetEase(Ease.InOutSine);
    }

    public void ShowCredits()
    {
        if (tweeningCredits)
            return;

        tweeningCredits = true;
        creditsText.DOFade(0, 0.25f).From(1);

        creditsMadeByText.DOFade(1, 0.25f).From(0).SetDelay(0.25f);

        creditsMadeByText.DOFade(0, 0.25f).SetDelay(0.25f+1f).OnComplete(() => tweeningCredits = false);

        creditsText.DOFade(1, 0.25f).SetDelay(0.25f +1f+0.5f);

    }

    public IEnumerator FadeToBlack()
    {
        Vector3 endRect = fadeBlackRect.localScale;
        fadeBlackRect.gameObject.SetActive(true);

        Tween fadeTween = fadeBlackRect.DOScale(endRect, 1.5f).From(Vector2.zero).SetEase(Ease.InOutSine);

        yield return fadeTween.WaitForCompletion();
    }
}
