using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : UnitySingletonPersistent<LevelController>
{
    public LevelBuilder levelBuilder;

    public ChapterConfig chapterConfig;

    public delegate void OnGameStart();
    public event OnGameStart onGameStart;
    public delegate void OnGamePaused(bool isPaused);
    public event OnGamePaused onGamePaused;
    public delegate void OnGameWon();
    public event OnGameWon onGameWon;
    public delegate void OnGameLost();
    public event OnGameLost onGameLost;

    public int currentLevel = 0;
    bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        LevelData levelData = chapterConfig.levels[currentLevel];

        levelBuilder.BuildLevel(levelData);
    }

    public IEnumerator LoadLevelCoroutine()
    {
        yield return StartCoroutine(levelBuilder.LoadLevelCoroutine());

        onGameStart?.Invoke();
    }


    public void Play()
    {
        // Get/Set all necessary values before playing

        // Start Play Coroutine
        StartCoroutine(PlayTurnCoroutine());
    }

    IEnumerator PlayTurnCoroutine()
    {
        // yield all player movements

        // yield all enemies movement

        // yield any special movement
        yield return new WaitForEndOfFrame();
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;
        onGamePaused?.Invoke(isPaused);

    }
}
