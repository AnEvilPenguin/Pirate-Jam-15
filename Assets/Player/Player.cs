using Assets.Potions;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Player
{
    public class Player : MonoBehaviour
    {
        public Rigidbody2D playerRigidbody;
        public GameLogic gameLogic;

        [HideInInspector] public PlayerState playerState = PlayerState.Idle;

        [HideInInspector] public List<Potion> gatheredPotions = new List<Potion>();
        [HideInInspector] public Potion potionInRange = null;
        [HideInInspector] public Cauldren cauldren;

        private void Start()
        {
            GameMaster.Instance.PlayerDied = false;
        }

        private void Update()
        {

        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Potion>() != null)
                potionInRange = collision.gameObject.GetComponent<Potion>();
            else if (collision.gameObject.GetComponent<Cauldren>() != null && !cauldren)
                cauldren = collision.gameObject.GetComponent<Cauldren>();
                
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Potion>() != null)
                potionInRange = null;
            else if (collision.gameObject.GetComponent<Cauldren>() != null && cauldren)
            {
                cauldren.isCooking = false;
                cauldren = null;
            }
                
        }

        public void PickUpPotion()
        {
            gatheredPotions.Add(potionInRange);
            gameLogic.userInterface.SetPotionToSlot(potionInRange);
            Destroy(potionInRange.gameObject);
        }

        public bool BrewInCauldren()
        {
            if (gatheredPotions.Count == gameLogic.potionsRequiredForCurrentBrew.Count)
            {
                SoundEffectsManager.instance.PlaySoundEffect(cauldren.Bubble, cauldren.gameObject.transform, 1f);
                cauldren.isCooking = true;
                gatheredPotions = new List<Potion>();
                gameLogic.SetNextBrewLevel();
                gameLogic.IncreaseLightLevel();
                return true;
            }
            return false;
        }

        public void KillPlayer()
        {
            GameMaster.Instance.PlayerDied = true;
            playerState = PlayerState.Dead;
        }
    }
}
