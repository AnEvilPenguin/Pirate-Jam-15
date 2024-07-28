using System.Collections.Generic;
using UnityEngine;

namespace Assets.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public float GrowlBackOff = 5f;
        public float KillDistance = 0.5f;
        public Rigidbody2D Rigidbody;
        public bool Silent = false;
        public Player.Player Target;

        [SerializeField] 
        private List<AudioClip> Growls;

        private float _growlCooldown;

        private void Update()
        {
            var targetPosition = Target.gameObject.transform.position;
            var position = gameObject.transform.position;

            var direction = GetDirection(targetPosition, position);

            if (direction.magnitude < 5f && !Silent)
                Growl();
            // TODO consider different effects when in really close.

            if (direction.magnitude < KillDistance)
                Target.KillPlayer();

        }

        private Vector3 GetDirection(Vector3 targetPosition, Vector3 position) =>
            targetPosition - position;

        private void Growl()
        {
            if (_growlCooldown == 0)
                _growlCooldown = SoundEffectsManager.instance.PlayRandomSoundEffect(Growls, transform, 1f) + GrowlBackOff;
            else if (_growlCooldown > 0)
                _growlCooldown -= Time.deltaTime;
            else if (_growlCooldown < 0)
                _growlCooldown = 0;
        }
    }
}
