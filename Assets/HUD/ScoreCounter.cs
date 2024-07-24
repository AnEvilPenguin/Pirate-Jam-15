using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public ProgressBar ProgressBar;
    public TMP_Text ScoreLabel;
    public int InitialTimeSeconds = 10;

    private int _lastIncrease = 0;
    private int _thisIncrease = 1;
    private float _timer;
    private int _targetTime;

    private int _score;

    void Start()
    {
        _targetTime = InitialTimeSeconds;
        ResetTimer();

        ProgressBar.Minimum = 0;
        UpdateProgressBar();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _targetTime + 1)
        {
            ResetTimer();
            UpdateScore();
            SetNewTarget();
        }
            
        UpdateProgressBar();
    }

    private void ResetTimer() =>
        _timer = 0;

    private void SetNewTarget()
    {
        var nextIncrease = _lastIncrease + _thisIncrease;

        _lastIncrease = _thisIncrease;
        _thisIncrease = nextIncrease;

        _targetTime += nextIncrease;
    }

    private void UpdateProgressBar()
    {
        ProgressBar.Maximum = _targetTime;
        ProgressBar.Current = (int)_timer;
    }

    private void UpdateScore()
    {
        _score++;
        ScoreLabel.text = $"Score: {_score}";
    }
}
