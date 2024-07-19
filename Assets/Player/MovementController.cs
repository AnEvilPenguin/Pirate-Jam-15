using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Player
{
    public class MovementController : MonoBehaviour
    {
        public Player player;
        public SpriteRenderer spriteRenderer;

        #region PlayerSprites
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

        public List<Sprite> pickupSpritesSouth = new List<Sprite>();
        public List<Sprite> pickupSpritesSouthWest = new List<Sprite>();
        public List<Sprite> pickupSpritesWest = new List<Sprite>();
        public List<Sprite> pickupSpritesNorthWest = new List<Sprite>();
        public List<Sprite> pickupSpritesNorth = new List<Sprite>();
        public List<Sprite> pickupSpritesNorthEast = new List<Sprite>();
        public List<Sprite> pickupSpritesEast = new List<Sprite>();
        public List<Sprite> pickupSpritesSouthEast = new List<Sprite>();
        #endregion
        List<Sprite> selectedSprites = new List<Sprite>();

        PlayerDirection spriteDirection = PlayerDirection.South;
        PlayerDirection movementDirection = PlayerDirection.South;

        List<MovementKeys> currentlyPressedMovementKeys = new List<MovementKeys>();
        int frameRate = 12;
        float idleTime;

        private void Update()
        {
            if (player.playerState == PlayerState.Dead) return;

            GetPlayerInput();

            if (player.playerState == PlayerState.Moving)
                ApplyPlayerMovement();

            if (player.playerState == PlayerState.Idle)
                PlayIdleAnimation();
        }

        void GetPlayerInput()
        {
            if (Input.GetKeyDown(KeyCode.W) && !currentlyPressedMovementKeys.Contains(MovementKeys.Down))
                currentlyPressedMovementKeys.Add(MovementKeys.Up);

            if (Input.GetKeyUp(KeyCode.W))
                currentlyPressedMovementKeys.Remove(MovementKeys.Up);


            if (Input.GetKeyDown(KeyCode.A) && !currentlyPressedMovementKeys.Contains(MovementKeys.Right))
                currentlyPressedMovementKeys.Add(MovementKeys.Left);

            if (Input.GetKeyUp(KeyCode.A))
                currentlyPressedMovementKeys.Remove(MovementKeys.Left);


            if (Input.GetKeyDown(KeyCode.S) && !currentlyPressedMovementKeys.Contains(MovementKeys.Up))
                currentlyPressedMovementKeys.Add(MovementKeys.Down);

            if (Input.GetKeyUp(KeyCode.S))
                currentlyPressedMovementKeys.Remove(MovementKeys.Down);


            if (Input.GetKeyDown(KeyCode.D) && !currentlyPressedMovementKeys.Contains(MovementKeys.Left))
                currentlyPressedMovementKeys.Add(MovementKeys.Right);

            if (Input.GetKeyUp(KeyCode.D))
                currentlyPressedMovementKeys.Remove(MovementKeys.Right);



            if (currentlyPressedMovementKeys.Any())
            {
                StopCoroutine("AnimationCoroutine");
                idleAnimationCoroutineRunning = false;

                player.playerState = PlayerState.Moving;
                movementDirection = currentlyPressedMovementKeys.GetPlayerDirectionFromCurrentKeyPresses(movementDirection);
            }
            else
            {
                player.playerState = PlayerState.Idle;
            }

        }

        void ApplyPlayerMovement()
        {
            selectedSprites = movementDirection switch
            {
                PlayerDirection.North => walkingSpritesNorth,
                PlayerDirection.NorthEast => walkingSpritesNorthEast,
                PlayerDirection.East => walkingSpritesEast,
                PlayerDirection.SouthEast => walkingSpritesSouthEast,
                PlayerDirection.South => walkingSpritesSouth,
                PlayerDirection.SouthWest => walkingSpritesSouthWest,
                PlayerDirection.West => walkingSpritesWest,
                PlayerDirection.NorthWest => walkingSpritesNorthWest,
                _ => walkingSpritesSouth
            };

            PlayAnimation(selectedSprites);
        }

        void PlayAnimation(List<Sprite> selectedSprites)
        {
            float playTime = (Time.time - Time.deltaTime) / 1.5f;
            int totalFrames = (int)(playTime * frameRate);
            int frame = totalFrames % selectedSprites.Count;

            spriteRenderer.sprite = selectedSprites[frame];
        }

        void PlayIdleAnimation()
        {
            selectedSprites = movementDirection switch
            {
                PlayerDirection.North => idleSpritesNorth,
                PlayerDirection.NorthEast => idleSpritesNorthEast,
                PlayerDirection.East => idleSpritesEast,
                PlayerDirection.SouthEast => idleSpritesSouthEast,
                PlayerDirection.South => idleSpritesSouth,
                PlayerDirection.SouthWest => idleSpritesSouthWest,
                PlayerDirection.West => idleSpritesWest,
                PlayerDirection.NorthWest => idleSpritesNorthWest,
                _ => idleSpritesSouth
            };

            if (!idleAnimationCoroutineRunning)
                StartCoroutine("AnimationCoroutine", selectedSprites);
        }

        bool idleAnimationCoroutineRunning = false;
        IEnumerator AnimationCoroutine(List<Sprite> selectedSprites)
        {
            idleAnimationCoroutineRunning = true;
            selectedSprites = selectedSprites.OrderBy(sprite => sprite.name).ToList();

            for (int i = 0; i < selectedSprites.Count; i++)
            {
                spriteRenderer.sprite = selectedSprites[i];
                yield return new WaitForSeconds(0.2f);
            }
            idleAnimationCoroutineRunning = false;
            yield break;
        }
    }

    enum MovementKeys
    {
        Up,
        Down,
        Left,
        Right
    }

    static class MovementKeysExtensions
    {
        public static PlayerDirection GetPlayerDirectionFromCurrentKeyPresses(this List<MovementKeys> currentKeys, PlayerDirection currentPlayerDirection)
        {
            PlayerDirection playerDirection = currentPlayerDirection;

            if (currentKeys.Count == 1)
            {
                if (currentKeys.Contains(MovementKeys.Up))
                    playerDirection = PlayerDirection.North;

                if (currentKeys.Contains(MovementKeys.Right))
                    playerDirection = PlayerDirection.East;

                if (currentKeys.Contains(MovementKeys.Down))
                    playerDirection = PlayerDirection.South;

                if (currentKeys.Contains(MovementKeys.Left))
                    playerDirection = PlayerDirection.West;
            }
            else if (currentKeys.Count == 2)
            {
                if (currentKeys.Contains(MovementKeys.Up) && currentKeys.Contains(MovementKeys.Right))
                    playerDirection = PlayerDirection.NorthEast;

                if (currentKeys.Contains(MovementKeys.Up) && currentKeys.Contains(MovementKeys.Left))
                    playerDirection = PlayerDirection.NorthWest;

                if (currentKeys.Contains(MovementKeys.Down) && currentKeys.Contains(MovementKeys.Right))
                    playerDirection = PlayerDirection.SouthEast;

                if (currentKeys.Contains(MovementKeys.Down) && currentKeys.Contains(MovementKeys.Left))
                    playerDirection = PlayerDirection.SouthWest;
            }

            return playerDirection;
        }
    }
}
