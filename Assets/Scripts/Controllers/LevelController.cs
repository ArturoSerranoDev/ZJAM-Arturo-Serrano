using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : UnitySingletonPersistent<LevelController>
{
    [Header("Controllers")]
    public LevelBuilder levelBuilder;
    public CommandsController commandsController;
    public UIController uiController;
    public DialogController dialogController;

    public ChapterConfig chapterConfig;
    public LevelData levelData;

    public List<CommandView> commandsOrdered = new List<CommandView>();
    public List<EnemyView> enemiesInLevel = new List<EnemyView>();
    //public List<EnemyView> buildingsView = new List<EnemyView>();

    // EVENTS
    public delegate void OnLevelLoaded();
    public event OnLevelLoaded onLevelLoaded;
    public delegate void OnGameStart();
    public event OnGameStart onGameStart;
    public delegate void OnTurnEnded(int turn);
    public event OnTurnEnded onTurnEnded;
    public delegate void OnGamePaused(bool isPaused);
    public event OnGamePaused onGamePaused;
    public delegate void OnGameWon();
    public event OnGameWon onGameWon;
    public delegate void OnGameLost();
    public event OnGameLost onGameLost;
    public delegate void OnLastTurn();
    public event OnLastTurn onLastTurn;

    public int currentLevel = 0;
    public int currentTurn = 0;

    PlayerController playerController;
    bool isPaused;

    ObjectiveType levelObjective = ObjectiveType.Escape;
    int levelTurnLimit = 100;
    Coroutine playTurnCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    public void Reset()
    {
        currentTurn = 0;
        isPaused = false;
        levelObjective = ObjectiveType.Escape;
        levelTurnLimit = 100;

        commandsOrdered.Clear();
        enemiesInLevel.Clear();
    }

    public void ResetInGame()
    {
        // Stop Coroutine
        if (playTurnCoroutine != null)
        {
            StopCoroutine(playTurnCoroutine);
        }

        // Reset Player pos and status

        // Reset enemies pos and status

        // Reset moved/Destroyed buildings

        currentTurn = 0;
        isPaused = false;
    }

    public void LoadNextLevel()
    {

    }

    public void LoadLevel()
    {
        levelData = chapterConfig.levels[currentLevel];

        dialogController.dialogData = levelData.levelDialog;
        levelObjective = levelData.levelObjective;
        levelTurnLimit = levelData.turnLimit;

        levelBuilder.BuildLevel(levelData);

        enemiesInLevel = levelBuilder.enemiesInLevel;
        playerController = levelBuilder.player.GetComponent<PlayerController>();

    }

    public IEnumerator LoadLevelCoroutine()
    {
        yield return StartCoroutine(levelBuilder.LoadLevelCoroutine());

        onLevelLoaded?.Invoke();
    }

    public void PlayButtonPressed()
    {
        // Get/Set all necessary values before playing
        commandsOrdered = GetCommandsOrderedByIndex();

        if (commandsOrdered.Count <= 0)
        {
            // Do not allow, play SFX of error 

            return;
        }


        StartCoroutine(PlayPressedCoroutine());
    }

    public IEnumerator PlayPressedCoroutine()
    {
        yield return StartCoroutine(uiController.PlayPressedCoroutine());

        Play();
    }

    public void Play()
    {


        // Start Play Coroutine
        playTurnCoroutine = StartCoroutine(PlayTurnCoroutine());
    }

    List<CommandView> GetCommandsOrderedByIndex()
    {
        return commandsController.commands;
    }



    IEnumerator PlayTurnCoroutine()
    {
        if (currentTurn > 100)
            yield return new WaitForEndOfFrame();

        // yield all player movements
        yield return StartCoroutine(playerController.ExecuteCommand(commandsOrdered[currentTurn]));

        // yield all enemies movement
        foreach (EnemyView enemyView in enemiesInLevel)
        {
            yield return StartCoroutine(enemyView.ExecuteCommand());

        }

        // yield any special movement

        // Check Win/Lose Condition
        bool isWon = CheckWinCondition();

        if (isWon)
        {
            // Show win panel

            // Star hiding elements of scene

            // Check if should save last chapter unlocked
            CheckSavingLastChapter();

            onGameWon?.Invoke();
            yield break;
        }

        bool isLost = CheckLoseCondition();

        if (isLost)
        {
            // Show lose panel

            // Star hiding elements of scene

            onGameLost?.Invoke();
            yield break;
        }

        // Add one to turn and tick UI
        currentTurn += 1;
        onTurnEnded?.Invoke(currentTurn);

        // Next One
        if (currentTurn < commandsOrdered.Count)
            StartCoroutine(PlayTurnCoroutine());
        else
        {
            //Last command, lose, force to reset
            onLastTurn?.Invoke();
        }

        Debug.Log("Turn COroutine, turn " + currentTurn);
        yield return new WaitForEndOfFrame();
    }

    void CheckSavingLastChapter()
    {
        int lastChapterUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);

        if (lastChapterUnlocked >= currentLevel)
            return;

        PlayerPrefs.SetInt("LevelsUnlocked", currentLevel);

        PlayerPrefs.Save();
    }

    bool CheckLoseCondition()
    {
        return playerController.isDestroyed;
    }

    bool CheckWinCondition()
    {
        switch (levelObjective)
        {
            case ObjectiveType.Escape:

                // Check if player is in evac
                return playerController.isInEvac;
            case ObjectiveType.Kill:

                // Check if all enemies are dead
                return enemiesInLevel.Count == 0;
            case ObjectiveType.Survive:

                // Check if turn is over and player is alive
                return !playerController.isDestroyed && currentTurn >= levelTurnLimit;
        }

        return false;
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;

        onGamePaused?.Invoke(isPaused);

    }

    public void ChangeTurnSpeed (float amount)
    {
        levelBuilder.turnSpeed = amount;
        playerController.animSpeed = amount;

        // yield all enemies movement
        foreach (EnemyView enemyView in enemiesInLevel)
        {
            enemyView.animSpeed = amount;

        }
    }

    public void GoToMainMenu()
    {
        StartCoroutine(GoToMainMenuCoroutine());
    }

    private IEnumerator GoToMainMenuCoroutine()
    {
        Debug.Log("Loading game!");

        // Fade to black
        yield return StartCoroutine(uiController.FadeToBlack());

        GameManager.Instance.GoToHomeMenu();
    }
}
