using System.Collections.Generic;
using UnityEngine;

namespace Assets.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public float GrowlBackOff = 5f;
        public Rigidbody2D Rigidbody;
        public float Speed = 1.0f;
        public GameObject Target;

        [SerializeField] private List<AudioClip> Growls;
        private float _growlCooldown;

        private void Update()
        {
            var targetPosition = Target.transform.position;
            var position = gameObject.transform.position;

            var direction = GetDirection(targetPosition, position);

            MoveTowardsTarget(direction);

            if (direction.magnitude < 5f)
                Growl();
            // TODO consider different effects when in really close.
        }

        private void MoveTowardsTarget(Vector3 direction) =>
            Rigidbody.velocity = direction.normalized * Speed;

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
