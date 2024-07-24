using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TimedSpriteScript;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class HoldEnter : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public List<Sprite> SpritesList = new List<Sprite>();

    private int _frame = 0;
    private int _frameRate = 12;
    private float _timer;

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (_frame == SpritesList.Count - 1)
            {
                Debug.Log("Move to next scene");
                GameMaster.Instance.TutorialCompleted = true;
            }
                
            PlayAnimation();
        }
        else
        {
            SpriteRenderer.sprite = SpritesList[0];
        }
    }

    private void PlayAnimation()
    {
        float playTime = (Time.time - Time.deltaTime) / 1.5f;
        int totalFrames = (int)(playTime * _frameRate);
        _frame = totalFrames % SpritesList.Count;
        SpriteRenderer.sprite = SpritesList[_frame];
    }
}
