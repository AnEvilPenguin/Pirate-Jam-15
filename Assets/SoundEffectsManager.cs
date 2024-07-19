using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager instance;

    [SerializeField] private AudioSource soundEffectObject;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void PlaySoundEffect(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundEffectObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundEffect (List<AudioClip> audioClips, Transform spawnTransform, float volume)
    {
        var rand = Random.Range(0, audioClips.Count);

        PlaySoundEffect(audioClips[rand], spawnTransform, volume);
    }
}
