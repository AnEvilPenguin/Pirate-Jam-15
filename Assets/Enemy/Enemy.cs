using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public float BackOff = 5f;
        public float KillDistance = 0.5f;
        public Rigidbody2D Rigidbody;
        public bool Silent = false;
        public Player.Player Target;

        [SerializeField] 
        private List<AudioClip> Growls;
        [SerializeField]
        private AudioClip Attack;
        [SerializeField]
        private List<AudioClip> Ambient;

        private float _cooldown = 4f;
        private float _killDelay;

        private void Update()
        {
            if(Target.playerState == Player.PlayerState.Dead)
            {
                KillPlayer();
                return;
            }

            var targetPosition = Target.gameObject.transform.position;
            var position = gameObject.transform.position;

            var direction = GetDirection(targetPosition, position);

            if (direction.magnitude < KillDistance && Target.playerState != Player.PlayerState.Dead)
            {
                Target.KillPlayer();
                _killDelay = SoundEffectsManager.instance.PlaySoundEffect(Attack, transform, 1f);
            }
            else if (direction.magnitude < 5f && !Silent)
                Growl();
            else
                Ambiance();
        }

        private Vector3 GetDirection(Vector3 targetPosition, Vector3 position) =>
            targetPosition - position;

        private void Growl()
            => PlaySFX(Growls, 1f);

        private void Ambiance()
            => PlaySFX(Ambient, 0.2f);

        private void PlaySFX(List<AudioClip> clips, float volume)
        {
            if (_cooldown == 0)
                _cooldown = SoundEffectsManager.instance.PlayRandomSoundEffect(clips, transform, volume) + BackOff;
            else if (_cooldown > 0)
                _cooldown -= Time.deltaTime;
            else if (_cooldown < 0)
                _cooldown = 0;
        }

        private void KillPlayer()
        {       
            if (_killDelay <= 0)
                GameMaster.Instance.LoadEndScene();

            _killDelay -= Time.deltaTime;
        }
    }
}
