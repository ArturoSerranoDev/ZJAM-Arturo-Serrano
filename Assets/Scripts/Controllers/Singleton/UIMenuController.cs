using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;
using UnityEngine.UI;

public class UIMenuController : MonoBehaviour
{
    public CanvasGroup StartingPanelCanvasGroup;
    public GameObject mainMenuPanel;
    public CanvasGroup mainMenuPanelCanvasGroup;
    public CanvasGroup levelSelectCanvasGroup;
    public CanvasGroup artGalleryCanvasGroup;

    public TextMeshProUGUI creditsText;
    public TextMeshProUGUI creditsMadeByText;

    public GameObject newGameText;
    public GameObject continueGameText;

    public RectTransform fadeBlackRect;
    public Image artworkImage;

    public List<Button> levelsButton = new List<Button>();
    public List<Sprite> artworksSprites = new List<Sprite>();

    bool tweeningCredits;

    int galleryIndex = 0;
    void Awake()
    {
        fadeBlackRect.gameObject.SetActive(false);


        if (ContextManager.Instance.mustFade)
        {
            ContextManager.Instance.mustFade = false;
            StartCoroutine(FadeFromBlack());

        }
        else
        {
            StartingPanelCanvasGroup.DOFade(1, 0.75f).From(0).SetEase(Ease.InOutSine);
        }

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
        galleryIndex = 0;

        mainMenuPanelCanvasGroup.DOFade(0, 0.25f).From(1).OnComplete(() => mainMenuPanelCanvasGroup.gameObject.SetActive(false)).SetEase(Ease.InOutSine);

        artGalleryCanvasGroup.gameObject.SetActive(true);

        artGalleryCanvasGroup.DOFade(1, 0.25f).From(0).SetDelay(0.25f).SetEase(Ease.InOutSine);

        artworkImage.sprite = artworksSprites[galleryIndex];
    }

    public void MoveToNextArtwork(int rigth)
    {
        if (galleryIndex <= 0 && rigth == -1)
            return;
        if (galleryIndex >= artworksSprites.Count - 1 && rigth == 1)
            return;

        galleryIndex += rigth;

        artworkImage.sprite = artworksSprites[galleryIndex];
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
        fadeBlackRect.gameObject.SetActive(true);

        Tween fadeTween = fadeBlackRect.DOScale(1, 1.5f).From(Vector2.zero).SetEase(Ease.InOutSine);

        yield return fadeTween.WaitForCompletion();
    }

    public IEnumerator FadeFromBlack()
    {
        fadeBlackRect.gameObject.SetActive(true);

        Tween fadeTween = fadeBlackRect.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InOutSine).From(1);

        yield return fadeTween.WaitForCompletion();

    }
}
