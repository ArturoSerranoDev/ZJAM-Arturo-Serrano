using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Game Panel")]
    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] RectTransform PlayButton;
    [SerializeField] RectTransform PauseButton;
    [SerializeField] RectTransform RestartButton;

    public Image LetsGoImage;


    [SerializeField] RectTransform FadeBlackTransition;
    [SerializeField] Image pauseOverlay;

    [Header("Win Lose")]

    public GameObject winLosePanel;
    public GameObject winLoseMenu;
    public GameObject goToNextLevelButtonGO;
    public GameObject restartLevelButtonGO;

    public Image winLoseImage;
    public Sprite winSprite;
    public Sprite loseSprite;

    //[Header("Pause Panel")]
    //[SerializeField] Text pauseHighScoreText;
    //[SerializeField] Text pauseScoreText;

    //[Header("Win/Lose Panel")]
    //[SerializeField] GameObject winLosePanel;
    //[SerializeField] GameObject goToNextLevelButtonGO;
    //[SerializeField] GameObject restartLevelButtonGO;
    //[SerializeField] Text endHighScoreText;
    //[SerializeField] Text endScoreText;
    //[SerializeField] Text endTitleText;

    //public void Init(int playerLives)
    //{
    //    foreach (GameObject livesImage in livesImage)
    //    {
    //        livesImage.SetActive(false);
    //    }

    //    for (int i = 0; i < playerLives; i++)
    //    {
    //        livesImage[i].SetActive(true);
    //    }
    //}


    public void OnEnable()
    {
        LevelController.Instance.onGamePaused += OnGamePaused;

        LevelController.Instance.onLevelLoaded += FadeInPauseMenu;
        LevelController.Instance.onTurnEnded += OnTurnEnded;

        LevelController.Instance.dialogController.onDialogCompleted += FadeOutPauseMenu;

        StartCoroutine(FadeFromBlack());

    }

    public void OnDisable()
    {
        //LevelController.Instance.onGamePaused -= OnGamePaused;

        //LevelController.Instance.onLevelLoaded -= FadeInPauseMenu;
        //LevelController.Instance.onTurnEnded -= OnTurnEnded;

        //LevelController.Instance.dialogController.onDialogCompleted -= FadeOutPauseMenu;
    }

    void OnGamePaused(bool isPaused)
    {
        pauseOverlay.gameObject.SetActive(isPaused);
    }

    public IEnumerator FadeFromBlack()
    {
        FadeBlackTransition.gameObject.SetActive(true);

        Tween fadeTween = FadeBlackTransition.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InOutSine).From(1);

        yield return fadeTween.WaitForCompletion();

        StartCoroutine(LevelController.Instance.LoadLevelCoroutine());

    }

    public IEnumerator StartAfterDialogCoroutine()
    {
        // Go! SFX and UI
        LetsGoImage.gameObject.SetActive(true);
        LetsGoImage.DOFade(1, 0.5f).From(0);

        Tween playTween = LetsGoImage.DOFade(0, 0.5f).From(1).SetDelay(1f);

        yield return playTween.WaitForCompletion();
    }

    void FadeInPauseMenu()
    {
        pauseOverlay.gameObject.SetActive(true);
    }
    void FadeOutPauseMenu()
    {
        pauseOverlay.gameObject.SetActive(false);
    }

    public void OnTurnEnded(int turn)
    {
        turnText.text = turn.ToString();
    }


    void OnGameWon()
    {
        ShowEndLevelScreen(isVictory: true);
    }

    void OnGameLost()
    {
        ShowEndLevelScreen(isVictory: false);
    }

    void ShowEndLevelScreen(bool isVictory)
    {
        winLosePanel.SetActive(true);

        goToNextLevelButtonGO.SetActive(isVictory);
        restartLevelButtonGO.SetActive(!isVictory);

        winLoseImage.sprite = isVictory ? winSprite : loseSprite;


        if (isVictory)
            winLosePanel.transform.DOMoveY(winLosePanel.transform.position.y, 1f).From(winLosePanel.transform.position.y - 1000).SetEase(Ease.InOutSine);
        else
            winLosePanel.transform.DOMoveY((winLosePanel.transform.position.y - 1000), 1f).From(winLosePanel.transform.position.y).SetEase(Ease.InOutSine);
    }



    public IEnumerator PlayPressedCoroutine()
    {
        Tween playTween = PlayButton.DOMoveY(PlayButton.position.y + 200, 0.5f);

        yield return playTween.WaitForCompletion();
    }

  

    public IEnumerator FadeToBlack()
    {
        FadeBlackTransition.gameObject.SetActive(true);

        Tween fadeTween = FadeBlackTransition.DOScale(1f, 1.5f).From(Vector2.zero).SetEase(Ease.InOutSine);

        yield return fadeTween.WaitForCompletion();
    }
}
