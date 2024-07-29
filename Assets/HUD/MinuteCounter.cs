using System;
using System.Collections.Generic;
using UnityEngine;

public class MinuteCounter : MonoBehaviour
{
    public event System.EventHandler TimerElapsed;

    public delegate void TimerElapsedEventHandler(EventArgs e);

    public ProgressBar ProgressBar;
    public List<Color> ColorList;
    public bool Invert;
    public float MaxTimer = 60;
    
    private float _timer;

    void Start()
    {
        ResetTimer();
        ProgressBar.Maximum = (int)MaxTimer;
        ProgressBar.Current = (int)MaxTimer;
    }

    void Update()
    {
        _timer = Invert ? _timer + Time.deltaTime : _timer - Time.deltaTime;

        if (Invert ? _timer >= MaxTimer : _timer <= 0)
        {
            ResetTimer();
            RaiseEvent();
        }
        
        UpdateProgressBar();
    }

    public void ResetTimer() =>
        _timer = Invert ? 0 : MaxTimer;

    private void UpdateProgressBar()
    {
        ProgressBar.Current = (int)_timer;

        var colorIndex = (int)((_timer / MaxTimer) * (ColorList.Count - 1));
        ProgressBar.Color = ColorList[colorIndex];
    }

    private void RaiseEvent()
    {
        var raiseEvent = TimerElapsed;

        if (raiseEvent != null)
            raiseEvent(this, new EventArgs());
    }
}
