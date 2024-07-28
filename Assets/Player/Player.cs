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
        [HideInInspector] public bool cauldrenInteractable = false;


        private void Update()
        {

        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Potion>() != null)
                potionInRange = collision.gameObject.GetComponent<Potion>();
            else if (collision.gameObject.GetComponent<Cauldren>() != null)
                cauldrenInteractable = true;
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Potion>() != null)
                potionInRange = null;
            else if (collision.gameObject.GetComponent<Cauldren>() != null)
                cauldrenInteractable = false;
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
                gatheredPotions = new List<Potion>();
                gameLogic.SetNextBrewLevel();
                return true;
            }
            return false;
        }

        public void KillPlayer()
        {
            // FIXME some sort of death animation?
            GameMaster.Instance.LoadEndScene();
        }
    }
}
