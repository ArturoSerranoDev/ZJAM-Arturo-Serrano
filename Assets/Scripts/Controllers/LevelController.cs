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
    public ChapterConfig chapterConfig;

    public List<CommandView> commandsOrdered = new List<CommandView>();

    // EVENTS
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

    public int currentLevel = 0;
    public int currentTurn = 0;

    PlayerController playerController;
    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        LevelData levelData = chapterConfig.levels[currentLevel];

        levelBuilder.BuildLevel(levelData);
        playerController = levelBuilder.player.GetComponent<PlayerController>();

    }

    public IEnumerator LoadLevelCoroutine()
    {
        yield return StartCoroutine(levelBuilder.LoadLevelCoroutine());

        onGameStart?.Invoke();
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
        StartCoroutine(PlayTurnCoroutine());
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

        // yield any special movement

        // Check Win/Lose Condition

        // Add one to turn and tick UI
        currentTurn += 1;
        onTurnEnded?.Invoke(currentTurn);

        // Next One
        if (currentTurn < commandsOrdered.Count)
            StartCoroutine(PlayTurnCoroutine());

        Debug.Log("Turn COroutine, turn " + currentTurn);
        yield return new WaitForEndOfFrame();
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;
        onGamePaused?.Invoke(isPaused);

    }
}
