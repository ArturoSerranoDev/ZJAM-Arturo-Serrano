using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : UnitySingletonPersistent<GameManager>
{
    public UIMenuController UIMenuController;

    [SerializeField] private string levelScene;
    [SerializeField] private string homeScene;


    private void Awake()
    {
        // TODO: Check playerprefs to get last level, unlockables and newgameContinue text
    }

    public void LoadLastChapter()
    {
        StartCoroutine(LoadScene(levelScene));
    }

    public void LoadLevelSelected(int levelSelected)
    {
        ContextManager.Instance.currentLevel = levelSelected;
        StartCoroutine(LoadScene(levelScene));
    }

    public void GoToHomeMenu()
    {
        StartCoroutine(LoadScene(homeScene));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Loading game!");

        // Fade to black
        yield return StartCoroutine(UIMenuController.FadeToBlack());


        SceneManager.LoadScene(sceneName);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}