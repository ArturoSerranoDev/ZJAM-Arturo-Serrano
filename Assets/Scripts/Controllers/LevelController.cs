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
    public delegate void OnReset();
    public event OnReset onReset;

    public int currentLevel = 0;
    public int currentTurn = 0;

    PlayerController playerController;
    bool isPaused;

    ObjectiveType levelObjective = ObjectiveType.Escape;
    int levelTurnLimit = 100;
    Vector3 levelEvacPos = Vector3.zero;
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
        levelEvacPos = Vector3.zero;

        commandsOrdered.Clear();
        enemiesInLevel.Clear();
    }

    public void ResetInGame()
    {
        StopAllCoroutines();
        DG.Tweening.DOTween.KillAll();
        // Stop Coroutine
        if (playTurnCoroutine != null)
        {
            StopCoroutine(playTurnCoroutine);
        }

        // Reset Player pos and status
        playerController.StopAllCoroutines();
        playerController.transform.position = levelData.playerStartingPos;
        playerController.transform.rotation = Quaternion.identity;
        playerController.Reset();


        // Reset enemies pos and status
        for (int i = 0; i < enemiesInLevel.Count; i++)
        {
            EnemyView enemyView = enemiesInLevel[i];

            enemyView.StopAllCoroutines();
            enemyView.Reset(levelData.enemiesData[i]);
            enemyView.transform.position = levelData.enemiesData[i].enemyPos;
        }

        // Reset moved/Destroyed buildings

        currentTurn = 0;
        isPaused = false;
        levelEvacPos = Vector3.zero;

        onReset?.Invoke();
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

        if (levelData.levelObjective == ObjectiveType.Escape)
        {
            
        }

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
        Debug.Log("CheckSaving");
        if (!PlayerPrefs.HasKey("LevelsUnlocked"))
        {
            PlayerPrefs.SetInt("LevelsUnlocked", currentLevel);
            PlayerPrefs.Save();
            return;
        }


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
                return playerController.IsInEvac();
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
        ContextManager.Instance.mustFade = true;

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
