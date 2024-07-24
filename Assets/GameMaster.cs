using UnityEngine;

public class GameMaster
{
    private static GameMaster _instance = new GameMaster();

    // make sure the constructor is private, so it can only be instantiated here
    private GameMaster()
    {
    }

    public static GameMaster Instance {
        get { return _instance; } 
    }

    public bool TutorialCompleted = false;
}
