using UnityEngine;

namespace Assets.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public Rigidbody2D Rigidbody;
        public float Speed = 1.0f;
        public GameObject Target;

        private void Update()
        {
            var targetLocation = Target.transform.position;
            var origin = gameObject.transform.position;

            var direction = targetLocation - origin;

            Rigidbody.velocity = direction.normalized * Speed;
        }
    }
}
