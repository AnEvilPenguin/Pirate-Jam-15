using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets;

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

        CompassDirection spriteDirection = CompassDirection.South;
        CompassDirection movementDirection = CompassDirection.South;

        List<MovementKeys> currentlyPressedMovementKeys = new List<MovementKeys>();
        List<MovementKeys> previouslyPressedMovementKeys = new List<MovementKeys>();
        int frameRate = 12;
        float movementSpeed = 1.5f;
        float idleTime;

        Vector2 movementDirectionVector;
        

        private void Update()
        {
            if (player.playerState == PlayerState.Dead) 
                return;

            if (Input.GetKeyDown(KeyCode.Space))
                PerformInteraction();

            GetPlayerInput();

            if (player.playerState == PlayerState.Moving)
                ApplyPlayerMovement();

            if (player.playerState == PlayerState.Idle)
                PlayIdleAnimation();
        }

        void FixedUpdate()
        {
            if (player.playerState != PlayerState.Moving)
            {
                player.playerRigidbody.velocity = Vector2.zero;
                return;
            }

            movementDirectionVector = movementDirection switch
            {
                CompassDirection.North => new Vector2(0, 1),
                CompassDirection.NorthEast => new Vector2(1, 1),
                CompassDirection.East => new Vector2(1, 0),
                CompassDirection.SouthEast => new Vector2(1, -1),
                CompassDirection.South => new Vector2(0, -1),
                CompassDirection.SouthWest => new Vector2(-1, -1),
                CompassDirection.West => new Vector2(-1, 0),
                CompassDirection.NorthWest => new Vector2(-1, 1),
                _ => Vector2.zero
            };
            movementSpeed = movementDirection.PlayerDirectionIsDiagonal() ? 1.15f : 1.5f;
            player.playerRigidbody.velocity = movementDirectionVector * movementSpeed;
        }

        bool playerIsInteracting = false;
        void PerformInteraction()
        {
            if ((player.potionInRange == null && !player.cauldrenInteractable) || playerIsInteracting)
                return;

            playerIsInteracting = true;
            player.playerState = PlayerState.Interracting;
            //previouslyPressedMovementKeys = currentlyPressedMovementKeys;
            //currentlyPressedMovementKeys = new List<MovementKeys>();

            if (player.potionInRange != null)
            {
                player.PickUpPotion();
            }
            else if (player.cauldrenInteractable)
            {
                var brewInCauldren = player.BrewInCauldren();

                if (player.cauldrenInteractable && !brewInCauldren)
                {
                    playerIsInteracting = false;
                    player.playerState = PlayerState.Idle;
                    return;
                }
            }


            selectedSprites = movementDirection switch
            {
                CompassDirection.North => pickupSpritesNorth,
                CompassDirection.NorthEast => pickupSpritesNorthEast,
                CompassDirection.East => pickupSpritesEast,
                CompassDirection.SouthEast => pickupSpritesSouthEast,
                CompassDirection.South => pickupSpritesSouth,
                CompassDirection.SouthWest => pickupSpritesSouthWest,
                CompassDirection.West => pickupSpritesWest,
                CompassDirection.NorthWest => pickupSpritesNorthWest,
                _ => pickupSpritesSouth
            };

            StopCoroutine("AnimationCoroutine");
            animationCoroutineRunning = false;
            StartCoroutine("AnimationCoroutine", selectedSprites);
        }

        //float directionUpdateTimer;
        void GetPlayerInput()
        {
            if (Input.GetKeyDown(KeyCode.W) && !currentlyPressedMovementKeys.Contains(MovementKeys.Down))
                currentlyPressedMovementKeys.Add(MovementKeys.Up);

            if (!Input.GetKey(KeyCode.W))
                currentlyPressedMovementKeys.Remove(MovementKeys.Up);


            if (Input.GetKeyDown(KeyCode.A) && !currentlyPressedMovementKeys.Contains(MovementKeys.Right))
                currentlyPressedMovementKeys.Add(MovementKeys.Left);

            if (!Input.GetKey(KeyCode.A))
                currentlyPressedMovementKeys.Remove(MovementKeys.Left);


            if (Input.GetKeyDown(KeyCode.S) && !currentlyPressedMovementKeys.Contains(MovementKeys.Up))
                currentlyPressedMovementKeys.Add(MovementKeys.Down);

            if (!Input.GetKey(KeyCode.S))
                currentlyPressedMovementKeys.Remove(MovementKeys.Down);


            if (Input.GetKeyDown(KeyCode.D) && !currentlyPressedMovementKeys.Contains(MovementKeys.Left))
                currentlyPressedMovementKeys.Add(MovementKeys.Right);

            if (!Input.GetKey(KeyCode.D))
                currentlyPressedMovementKeys.Remove(MovementKeys.Right);



            if (playerIsInteracting)
                return;

            //directionUpdateTimer += Time.deltaTime;

            if (currentlyPressedMovementKeys.Any())
            {
                if (animationCoroutineRunning)
                {
                    StopCoroutine("AnimationCoroutine");
                    animationCoroutineRunning = false;
                }

                player.playerState = PlayerState.Moving;

                //if (directionUpdateTimer >= 0.01f)
                //{
                    movementDirection = currentlyPressedMovementKeys.GetPlayerDirectionFromCurrentKeyPresses(movementDirection);
                    //directionUpdateTimer = 0f;
                //}
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

            if (!animationCoroutineRunning)
                StartCoroutine("AnimationCoroutine", selectedSprites);
        }

        bool animationCoroutineRunning = false;
        IEnumerator AnimationCoroutine(List<Sprite> selectedSprites)
        {
            animationCoroutineRunning = true;
            selectedSprites = selectedSprites.OrderBy(sprite => sprite.name).ToList();

            for (int i = 0; i < selectedSprites.Count; i++)
            {
                spriteRenderer.sprite = selectedSprites[i];
                yield return new WaitForSeconds(0.2f);
            }
            animationCoroutineRunning = false;

            if (playerIsInteracting)
            {
                //previouslyPressedMovementKeys.ForEach(key => currentlyPressedMovementKeys.Add(key));
                //previouslyPressedMovementKeys = new List<MovementKeys>();
                playerIsInteracting = false;
            }

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

    static class MovementExtensions
    {
        public static CompassDirection GetPlayerDirectionFromCurrentKeyPresses(this List<MovementKeys> currentKeys, CompassDirection currentPlayerDirection)
        {
            CompassDirection playerDirection = currentPlayerDirection;

            if (currentKeys.Count == 1)
            {
                if (currentKeys.Contains(MovementKeys.Up))
                    playerDirection = CompassDirection.North;

                if (currentKeys.Contains(MovementKeys.Right))
                    playerDirection = CompassDirection.East;

                if (currentKeys.Contains(MovementKeys.Down))
                    playerDirection = CompassDirection.South;

                if (currentKeys.Contains(MovementKeys.Left))
                    playerDirection = CompassDirection.West;
            }
            else if (currentKeys.Count == 2)
            {
                if (currentKeys.Contains(MovementKeys.Up) && currentKeys.Contains(MovementKeys.Right))
                    playerDirection = CompassDirection.NorthEast;

                if (currentKeys.Contains(MovementKeys.Up) && currentKeys.Contains(MovementKeys.Left))
                    playerDirection = CompassDirection.NorthWest;

                if (currentKeys.Contains(MovementKeys.Down) && currentKeys.Contains(MovementKeys.Right))
                    playerDirection = CompassDirection.SouthEast;

                if (currentKeys.Contains(MovementKeys.Down) && currentKeys.Contains(MovementKeys.Left))
                    playerDirection = CompassDirection.SouthWest;
            }

            return playerDirection;
        }

        public static bool PlayerDirectionIsDiagonal(this CompassDirection currentDirection)
            => !(new List<CompassDirection>() { CompassDirection.North, CompassDirection.East, CompassDirection.South, CompassDirection.West })
                .Contains(currentDirection);
    }
}
