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
            var targetPosition = Target.transform.position;
            var position = gameObject.transform.position;

            var direction = GetDirection(targetPosition, position);

            MoveTowardsTarget(direction);

            var direction = targetLocation - origin;

        private void MoveTowardsTarget(Vector3 direction) =>
            Rigidbody.velocity = direction.normalized * Speed;

        private Vector3 GetDirection(Vector3 targetPosition, Vector3 position) =>
            targetPosition - position;
    }
}
