using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public MinuteCounter ProgressBar;
    public RectTransform Collider;
    public RectTransform Mask;
    public int ReductionPercent = 20;
    public float ReductionAmount = 0.25f;

    private float _maskReduction;
    private float _colliderReduction;
    private float _maskTarget;

    void Start()
    {
        _maskReduction = Mask.localScale.x * (ReductionPercent / 100f);
        _maskTarget = Mask.localScale.x;

        ProgressBar.TimerElapsed += OnTimerElapsed;
    }

    private void Update()
    {
        var scaleX = Mask.localScale.x;

        if (scaleX > _maskTarget)
        {
            var current = scaleX - ReductionAmount * Time.deltaTime;
            var scale = new Vector2(current, current);

            Mask.localScale = scale;
            Collider.localScale = scale;
        }
        else if (scaleX < _maskTarget)
        {
            var current = scaleX + ReductionAmount * Time.deltaTime;
            var scale = new Vector2(current, current);

            Mask.localScale = scale;
            Collider.localScale = scale;
        }

    }

    public void IncreaseLightTimes(int times)
    {
        _maskTarget = Mask.localScale.x + (_maskReduction * times);
        ProgressBar.ResetTimer();
    }

    private void OnTimerElapsed(System.Object sender, System.EventArgs e)
    {
        _maskTarget = Mask.localScale.x - _maskReduction;
    }
}
