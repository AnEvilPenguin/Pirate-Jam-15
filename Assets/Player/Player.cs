using UnityEngine;

namespace Assets.Player
{
    public class Player : MonoBehaviour
    {
        public Rigidbody2D Rigidbody;

        [HideInInspector] public PlayerState playerState = PlayerState.Idle;
    }
}
