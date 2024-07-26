using Pathfinding;
using UnityEngine;

public class EnemySpeed : MonoBehaviour
{
    public MinuteCounter ProgressBar;
    public AIPath Pathing;

    public float SpeedIncrease = 0.05f;
    public int SpeedIncreaseDivisor = 3;

    private void Start()
    {
        ProgressBar.TimerElapsed += OnTimerElapsed;
    }

    private void OnTimerElapsed(System.Object sender, System.EventArgs e)
    {
        Pathing.maxSpeed += SpeedIncrease;

        SpeedIncrease += SpeedIncrease / SpeedIncreaseDivisor;
    }
        
}
