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
        ResetTimer(false);
        ProgressBar.Maximum = (int)MaxTimer;
        ProgressBar.Current = (int)MaxTimer;
    }

    void Update()
    {
        _timer = Invert ? _timer + Time.deltaTime : _timer - Time.deltaTime;

        if (Invert ? _timer >= MaxTimer : _timer <= 0)
            ResetTimer(true);

        UpdateProgressBar();
    }

    private void ResetTimer(bool sendEvent)
    {
        _timer = Invert ? 0 : MaxTimer;

        if (!sendEvent)
            return;

        var raiseEvent = TimerElapsed;

        if (raiseEvent != null)
            raiseEvent(this, new EventArgs());
    }

    private void UpdateProgressBar()
    {
        ProgressBar.Current = (int)_timer;

        var colorIndex = (int)((_timer / MaxTimer) * (ColorList.Count - 1));
        ProgressBar.Color = ColorList[colorIndex];
    }
}
