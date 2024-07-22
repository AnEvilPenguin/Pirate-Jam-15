using System.Collections.Generic;
using UnityEngine;

public class TimedSpriteScript : MonoBehaviour
{
    [System.Serializable]
    public class ListOfSprite
    {
        public List<Sprite> sprites;
    }

    public SpriteRenderer SpriteRenderer;
    public List<ListOfSprite> SpritesList = new List<ListOfSprite>();

    public int TimerIntervalSeconds = 5;

    private List<Sprite> _selectedSprites;
    private int _frameRate = 12;
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

        ApplySprites();
    }

    private void ResetTimer()
    {
        _timer = TimerIntervalSeconds * SpritesList.Count;
    }

    private void ApplySprites()
    {
        var ceil = (int)Mathf.Ceil(_timer);

        if (ceil % TimerIntervalSeconds == 0)
        {
            var index = ceil == 0 ? 
                0 : (ceil / TimerIntervalSeconds) - 1;

            _selectedSprites = SpritesList[index].sprites;
        }
            

        PlayAnimation();
    }

    private void PlayAnimation()
    {
        float playTime = (Time.time - Time.deltaTime) / 1.5f;
        int totalFrames = (int)(playTime * _frameRate);
        int frame = totalFrames % _selectedSprites.Count;

        SpriteRenderer.sprite = _selectedSprites[frame];
    }
}
