using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Player;

public class MovementAnimations : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public PlayerDirection Direction;
    public bool IsIdle = true;

    public List<Sprite> idleSpritesSouth = new List<Sprite>();

    public List<Sprite> walkingSpritesSouth = new List<Sprite>();
    public List<Sprite> walkingSpritesWest = new List<Sprite>();
    public List<Sprite> walkingSpritesNorth = new List<Sprite>();
    public List<Sprite> walkingSpritesEast = new List<Sprite>();

    private int frameRate = 12;
    private List<Sprite> selectedSprites = new List<Sprite>();

    void Update()
    {
        ApplyAnimation();
    }

    public void SetDirection(PlayerDirection direction) =>
        Direction = direction;

    public void SetIdle(bool isIdle) => 
        IsIdle = isIdle;

    private void ApplyAnimation()
    {
        if (IsIdle)
            selectedSprites = idleSpritesSouth;
        else
            switch (Direction)
            {
                case PlayerDirection.North:
                    selectedSprites = walkingSpritesNorth;
                    break;

                case PlayerDirection.South:
                    selectedSprites = walkingSpritesSouth;
                    break;

                case PlayerDirection.West:
                    selectedSprites = walkingSpritesWest;
                    break;

                case PlayerDirection.East:
                    selectedSprites = walkingSpritesEast;
                    break;
            }

        PlayAnimation();
    }

    private void PlayAnimation()
    {
        float playTime = (Time.time - Time.deltaTime) / 1.5f;
        int totalFrames = (int)(playTime * frameRate);
        int frame = totalFrames % selectedSprites.Count;

        spriteRenderer.sprite = selectedSprites[frame];
    }
}
