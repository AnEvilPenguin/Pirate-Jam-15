using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public MinuteCounter ProgressBar;
    public CircleCollider2D Collider;
    public RectTransform Mask;
    public int ReductionPercent = 20;

    private float _reductionAmount;

    void Start()
    {
        _reductionAmount = Mask.sizeDelta.x * (ReductionPercent / 100f);
        
        ProgressBar.TimerElapsed += OnTimerElapsed;
    }

    private void OnTimerElapsed(System.Object sender, System.EventArgs e)
    {
        var current = Mask.sizeDelta.x - _reductionAmount;
        Mask.sizeDelta = new Vector2(current, current);

        Collider.radius -= _reductionAmount / 2;
    }
}
