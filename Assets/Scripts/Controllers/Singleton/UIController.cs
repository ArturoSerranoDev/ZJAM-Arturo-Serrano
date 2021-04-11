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
    [SerializeField] RectTransform InputRect;
    [SerializeField] TextMeshProUGUI turnText;
    [SerializeField] CanvasGroup PlayButton;
    [SerializeField] CanvasGroup PauseButton;
    [SerializeField] CanvasGroup RestartButton;

    public Image LetsGoImage;


    [SerializeField] RectTransform FadeBlackTransition;
    [SerializeField] Image pauseOverlay;

    [Header("Win Lose")]

    public CanvasGroup winLosePanel;
    public GameObject winLoseMenu;
    public GameObject goToNextLevelButtonGO;
    public GameObject restartLevelButtonGO;

    public Image winLoseImage;
    public Sprite winSprite;
    public Sprite loseSprite;

    public Color winColor;
    public Color loseColor;

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
        LevelController.Instance.onGameWon += OnGameWon;
        LevelController.Instance.onReset += OnReset;
        LevelController.Instance.onGameLost += OnGameLost;
        LevelController.Instance.onGamePaused += OnGamePaused;

        LevelController.Instance.onLevelLoaded += FadeInPauseMenu;
        LevelController.Instance.onLastTurn += OnGameLost;
        LevelController.Instance.onTurnEnded += OnTurnEnded;

        LevelController.Instance.dialogController.onDialogCompleted += FadeOutPauseMenu;

        StartCoroutine(FadeFromBlack());

    }

    private void OnReset()
    {
        Tween playTween = PlayButton.DOFade(1, 0.5f);

        RestartButton.DOFade(0, 0.5f);
        PauseButton.DOFade(0, 0.5f);
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
        InputRect.gameObject.SetActive(true);
        InputRect.transform.DOMoveX(InputRect.transform.position.x, 1f).From(InputRect.transform.position.x - 600).SetEase(Ease.InOutSine);

        PlayButton.gameObject.SetActive(true);
        PlayButton.DOFade(1, 1f).From(0).SetEase(Ease.InOutSine);


        // Go! SFX and UI
        LetsGoImage.gameObject.SetActive(true);
        LetsGoImage.DOFade(1, 0.25f).From(0);

        Tween playTween = LetsGoImage.DOFade(0, 0.25f).From(1).SetDelay(1f);

        yield return playTween.WaitForCompletion();
    }

    void FadeInPauseMenu()
    {
        pauseOverlay.gameObject.SetActive(true);
    }
    void FadeOutPauseMenu()
    {
        StartCoroutine(StartAfterDialogCoroutine());

        pauseOverlay.gameObject.SetActive(false);
    }

    public void OnTurnEnded(int turn)
    {
        turnText.text = "Turn: " + (turn + 0).ToString();
    }


    void OnGameWon()
    {
        ShowEndLevelScreen(isVictory: true);
        Debug.Log("UI OngameWon");
    }

    void OnGameLost()
    {
        ShowEndLevelScreen(isVictory: false);
        Debug.Log("UI Ongamelost");

    }

    void ShowEndLevelScreen(bool isVictory)
    {
        winLosePanel.gameObject.SetActive(true);

        goToNextLevelButtonGO.SetActive(isVictory);
        restartLevelButtonGO.SetActive(!isVictory);

        winLoseImage.sprite = isVictory ? winSprite : loseSprite;

        winLoseMenu.GetComponent<Image>().color = isVictory ? winColor : loseColor;

        winLosePanel.DOFade(1, 0.5f).From(0).SetEase(Ease.InOutSine);

    }

    public void ResetButtonFromDefeat()
    {
        LevelController.Instance.ResetInGame();

        winLosePanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).From(1).SetEase(Ease.InOutSine).OnComplete(() => winLosePanel.gameObject.SetActive(false));

    }

    public void NextLevel()
    {
        LevelController.Instance.LoadNextLevel();

        winLosePanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).From(1).SetEase(Ease.InOutSine).OnComplete(() => winLosePanel.gameObject.SetActive(false));

    }

    public IEnumerator PlayPressedCoroutine()
    {
        Tween playTween = PlayButton.DOFade(0, 0.5f);
        RestartButton.gameObject.SetActive(true);
        PauseButton.gameObject.SetActive(true);

        RestartButton.DOFade(1, 0.5f).From(0).SetEase(Ease.InOutSine);
        PauseButton.DOFade(1, 0.5f).From(0).SetEase(Ease.InOutSine);

        yield return playTween.WaitForCompletion();
    }

  

    public IEnumerator FadeToBlack()
    {
        FadeBlackTransition.gameObject.SetActive(true);

        Tween fadeTween = FadeBlackTransition.DOScale(1f, 1.5f).From(Vector2.zero).SetEase(Ease.InOutSine);

        yield return fadeTween.WaitForCompletion();
    }
}
