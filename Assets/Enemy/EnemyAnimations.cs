using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public EnemyMovement Movement;

    #region Sprites
    public List<Sprite> idleSpritesSouth = new List<Sprite>();
    public List<Sprite> idleSpritesSouthWest = new List<Sprite>();
    public List<Sprite> idleSpritesWest = new List<Sprite>();
    public List<Sprite> idleSpritesNorthWest = new List<Sprite>();
    public List<Sprite> idleSpritesNorth = new List<Sprite>();
    public List<Sprite> idleSpritesNorthEast = new List<Sprite>();
    public List<Sprite> idleSpritesEast = new List<Sprite>();
    public List<Sprite> idleSpritesSouthEast = new List<Sprite>();

    public List<Sprite> walkingSpritesSouth = new List<Sprite>();
    public List<Sprite> walkingSpritesSouthWest = new List<Sprite>();
    public List<Sprite> walkingSpritesWest = new List<Sprite>();
    public List<Sprite> walkingSpritesNorthWest = new List<Sprite>();
    public List<Sprite> walkingSpritesNorth = new List<Sprite>();
    public List<Sprite> walkingSpritesNorthEast = new List<Sprite>();
    public List<Sprite> walkingSpritesEast = new List<Sprite>();
    public List<Sprite> walkingSpritesSouthEast = new List<Sprite>();
    #endregion
    List<Sprite> selectedSprites = new List<Sprite>();

    private int frameRate = 12;

    // Update is called once per frame
    void Update()
    {
        if (Movement.IsMoving)
            PlayMovementAnimation();
        else
            PlayIdleAnimation();
    }

    private void PlayMovementAnimation()
    {
        selectedSprites = Movement.Direction switch
        {
            CompassDirection.North => walkingSpritesNorth,
            CompassDirection.NorthEast => walkingSpritesNorthEast,
            CompassDirection.East => walkingSpritesEast,
            CompassDirection.SouthEast => walkingSpritesSouthEast,
            CompassDirection.South => walkingSpritesSouth,
            CompassDirection.SouthWest => walkingSpritesSouthWest,
            CompassDirection.West => walkingSpritesWest,
            CompassDirection.NorthWest => walkingSpritesNorthWest,
            _ => walkingSpritesSouth
        };

        PlayAnimation();
    }

    private void PlayIdleAnimation()
    {
        selectedSprites = Movement.Direction switch
        {
            CompassDirection.North => idleSpritesNorth,
            CompassDirection.NorthEast => idleSpritesNorthEast,
            CompassDirection.East => idleSpritesEast,
            CompassDirection.SouthEast => idleSpritesSouthEast,
            CompassDirection.South => idleSpritesSouth,
            CompassDirection.SouthWest => idleSpritesSouthWest,
            CompassDirection.West => idleSpritesWest,
            CompassDirection.NorthWest => idleSpritesNorthWest,
            _ => idleSpritesSouth
        };

        PlayAnimation();
    }

    private void PlayAnimation()
    {
        float playTime = (Time.time - Time.deltaTime) / 1.5f;
        int totalFrames = (int)(playTime * frameRate);
        int frame = totalFrames % selectedSprites.Count;

        SpriteRenderer.sprite = selectedSprites[frame];
    }
}
