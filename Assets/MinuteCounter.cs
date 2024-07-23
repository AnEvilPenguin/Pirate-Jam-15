using UnityEngine;

public class MinuteCounter : MonoBehaviour
{
    public ProgressBar ProgressBar;

    private float _maxTimer = 60;
    private float _timer;


    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
        ProgressBar.Maximum = (int)_maxTimer;
        ProgressBar.Current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer <= 0)
            ResetTimer();

        _timer -= Time.deltaTime;

        ProgressBar.Current = (int)_timer;
    }

    private void ResetTimer()
    {
        _timer = 60;
    }
}
