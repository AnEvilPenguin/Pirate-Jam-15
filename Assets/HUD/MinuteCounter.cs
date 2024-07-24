using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinuteCounter : MonoBehaviour
{
    public ProgressBar ProgressBar;
    public List<Color> ColorList;

    private float _maxTimer = 60;
    private float _timer;


    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
        ProgressBar.Maximum = (int)_maxTimer;
        ProgressBar.Current = (int)_maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
            ResetTimer();

        UpdateProgressBar();
    }

    private void ResetTimer()
    {
        _timer = 60;
    }

    private void UpdateProgressBar()
    {
        ProgressBar.Current = (int)_timer;

        var colorIndex = (int)((_timer / _maxTimer) * (ColorList.Count - 1));
        ProgressBar.Color = ColorList[colorIndex];
    }
}
