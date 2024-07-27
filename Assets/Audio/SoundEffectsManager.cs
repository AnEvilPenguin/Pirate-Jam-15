using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager instance;

    [SerializeField] private AudioSource soundEffectObject;
    public bool PitchShift = true;
    public float MaxPitchShift = 0.1f;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public float PlaySoundEffect(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundEffectObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        if (PitchShift)
            audioSource.pitch = audioSource.pitch + Random.Range(-1 * MaxPitchShift, MaxPitchShift);

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);

        return clipLength;
    }

    public float PlayRandomSoundEffect (List<AudioClip> audioClips, Transform spawnTransform, float volume)
    {
        var rand = Random.Range(0, audioClips.Count);

        return PlaySoundEffect(audioClips[rand], spawnTransform, volume);
    }
}
