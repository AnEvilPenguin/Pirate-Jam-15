using Assets.Potions;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Player
{
    public class Player : MonoBehaviour
    {
        public Rigidbody2D playerRigidbody;

        [HideInInspector] public PlayerState playerState = PlayerState.Idle;

        List<Potion> gatheredPotions = new List<Potion>();


        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Potion>() != null)
            {

            }
            else if (collision.gameObject.GetComponent<Potion>() != null)
            {

            }
        }
    }
}
