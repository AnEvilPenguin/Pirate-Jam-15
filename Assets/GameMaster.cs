using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster
{
    private static GameMaster _instance = new GameMaster();

    public bool TutorialCompleted = false;
    public bool PlayerDied = false;


    // Make sure the constructor is private, so it can only be instantiated here.
    private GameMaster()
    {
    }

    public static GameMaster Instance {
        get { return _instance; } 
    }

    public void LoadFirstGameScene(bool tutorialCompleted)
    {
        TutorialCompleted = tutorialCompleted;

        LoadFirstGameScene();
    }

    public void LoadFirstGameScene()
    {
        var scene = TutorialCompleted ? "Level1" : "Tutorial";

        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void LoadEndScene() =>
        SceneManager.LoadScene("End", LoadSceneMode.Single);

    public void LoadMainMenuScene() =>
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
}
