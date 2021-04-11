using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : UnitySingletonPersistent<GameManager>
{
    public UIMenuController UIMenuController;

    [SerializeField] private string levelScene;
    [SerializeField] private string homeScene;

    string currentScene = "MenuScene";

    protected void OnEnable()
    {
        base.Awake();
        // TODO: Check playerprefs to get last level, unlockables and newgameContinue text


        if (currentScene == "MenuScene")
            CheckPlayerCompletion();
    }

    public void CheckPlayerCompletion()
    {

        Debug.Log("CheckPlayerCOmpletion");

        if (!PlayerPrefs.HasKey("LevelsUnlocked"))
        {
            UIMenuController.StartingPanelCanvasGroup.gameObject.SetActive(true);
            UIMenuController.mainMenuPanelCanvasGroup.gameObject.SetActive(false);

            UIMenuController.newGameText.SetActive(true);
            UIMenuController.continueGameText.SetActive(false);

            for (int i = 0; i < UIMenuController.levelsButton.Count; i++)
            {
                Button buttonLevel = UIMenuController.levelsButton[i];
                buttonLevel.interactable = false;
            }
        }
        else
        {
            UIMenuController.StartingPanelCanvasGroup.gameObject.SetActive(false);
            UIMenuController.mainMenuPanelCanvasGroup.gameObject.SetActive(true);


            int lastChapterUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);
            UIMenuController.newGameText.SetActive(false);
            UIMenuController.continueGameText.SetActive(true);

            for (int i = 0; i < UIMenuController.levelsButton.Count; i++)
            {
                Button buttonLevel = UIMenuController.levelsButton[i];
                buttonLevel.interactable = i > lastChapterUnlocked ? false : true;
            }
        }
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
        currentScene = "MenuScene";

        SceneManager.LoadScene(homeScene);
    }

    private IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Loading game!");

        // Fade to black
        yield return StartCoroutine(UIMenuController.FadeToBlack());

        currentScene = "LevelScene";
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}