using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    //[Header("Game Panel")]
    //[SerializeField] Text playScoreText;
    //[SerializeField] GameObject closeHighScoreGO;
    //[SerializeField] GameObject newHighScoreTextGO;
    //[SerializeField] List<GameObject> livesImage;

    //[Header("Pause Panel")]
    //[SerializeField] GameObject pausePanel;
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

    //public void OnEnable()
    //{
    //    LevelManager.onGameWon += OnGameWon;
    //    LevelManager.onGameLost += OnGameLost;
    //    LevelManager.onGamePaused += PauseGame;
    //    LevelManager.onHighScoreReached += OnHighScoreReached;
    //    LevelManager.onCloseToHighScoreReached += OnCloseHighScoreReached;
    //}

    //public void OnDisable()
    //{
    //    LevelManager.onGameWon -= OnGameWon;
    //    LevelManager.onGameLost -= OnGameLost;
    //    LevelManager.onGamePaused -= PauseGame;
    //    LevelManager.onHighScoreReached -= OnHighScoreReached;
    //    LevelManager.onCloseToHighScoreReached -= OnCloseHighScoreReached;
    //}

    //void PauseGame(bool isPaused)
    //{
    //    if (winLosePanel.activeInHierarchy)
    //        return;

    //    pausePanel.SetActive(isPaused);
    //    pauseHighScoreText.text = "High Score: " + LevelManager.HighScore;
    //    pauseScoreText.text = "Your Score: " + LevelManager.Score;
    //}

    //void OnGameWon()
    //{
    //    ShowEndLevelScreen(isVictory: true);
    //}

    //void OnGameLost()
    //{
    //    playScoreText.text = string.Empty;
    //    ShowEndLevelScreen(isVictory: false);
    //}

    //public void OnPlayerHit(int lives)
    //{
    //    livesImage[lives].SetActive(false);
    //}

    //void OnHighScoreReached(int highScore)
    //{
    //    StartCoroutine(PlayHighScoreAnimation());
    //}

    //void OnCloseHighScoreReached()
    //{
    //    StartCoroutine(PlayHighScoreCloseAnimation());
    //}

    //IEnumerator PlayHighScoreCloseAnimation()
    //{
    //    closeHighScoreGO.SetActive(true);
    //    yield return new WaitForSeconds(2f);
    //    closeHighScoreGO.SetActive(false);
    //}

    //IEnumerator PlayHighScoreAnimation()
    //{
    //    newHighScoreTextGO.SetActive(true);
    //    yield return new WaitForSeconds(2f);
    //    newHighScoreTextGO.SetActive(false);
    //}

    //void ShowEndLevelScreen(bool isVictory)
    //{
    //    winLosePanel.SetActive(true);

    //    goToNextLevelButtonGO.SetActive(isVictory);
    //    restartLevelButtonGO.SetActive(!isVictory);

    //    endHighScoreText.text = "High Score: " + LevelManager.HighScore;
    //    endScoreText.text = "Your Score: " + LevelManager.Score;
    //    endTitleText.text = isVictory ? "VICTORY" : "DEFEAT";
    //}

    //public void UpdateScore(int score)
    //{
    //    playScoreText.text = score.ToString();
    //}

    //public void ShowEndChapterScreen()
    //{
    //    endTitleText.text = "Congratulations, you completed this chapter!";

    //    winLosePanel.SetActive(true);
    //    goToNextLevelButtonGO.SetActive(false);
    //    restartLevelButtonGO.SetActive(false);
    //}

    //public void OnGameStart()
    //{
    //    pausePanel.SetActive(false);
    //    winLosePanel.SetActive(false);
    //    newHighScoreTextGO.SetActive(false);

    //    playScoreText.text = string.Empty;
    //}
}
