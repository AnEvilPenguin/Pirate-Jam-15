using Assets;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public KeyAnimations WKey;
    public KeyAnimations AKey;
    public KeyAnimations SKey;
    public KeyAnimations DKey;

    public MovementAnimations Movement;
    public float TimerDuration = 5f * 5;

    private float _timer;

    void Start()
    {
        ResetTimer();
    }
   
    void Update()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;
        else
            ResetTimer();

        ApplyMovement();
    }

    private void ResetTimer()
    {
        _timer = TimerDuration;
    }

    private void ApplyMovement()
    {
        if (_timer > 20)
        {
            SKey.SetAnimationState(0);
            Movement.SetIdle(true);
        }
        else if (_timer > 15)
        {
            Movement.SetIdle(false);
            Movement.SetDirection(CompassDirection.East);
            DKey.SetAnimationState(1);
        }
        else if (_timer > 10)
        {
            DKey.SetAnimationState(0);
            Movement.SetDirection(CompassDirection.North);
            WKey.SetAnimationState(1);
        }
        else if (_timer > 5)
        {
            WKey.SetAnimationState(0);
            Movement.SetDirection(CompassDirection.West);
            AKey.SetAnimationState(1);
        }
        else if (_timer > 0)
        {
            AKey.SetAnimationState(0);
            Movement.SetDirection(CompassDirection.South);
            SKey.SetAnimationState(1);
        }
    }
}
