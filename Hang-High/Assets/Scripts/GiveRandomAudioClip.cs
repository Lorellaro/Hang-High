using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GiveRandomAudioClip : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] bool playOnAwake = true;

    private void Start()
    {
        if (playOnAwake)
        {
            changeAndPlayClip();
        }
    }

    public void changeAndPlayClip()
    {
        int randomNum = Random.Range(0, audioClips.Count);
        audioSource.clip = audioClips[randomNum];
        RandomisePitch();
        audioSource.Play();
    }

    private void RandomisePitch()
    {
        float randomPitch = Random.Range(0.75f, 1.25f);
        audioSource.pitch = randomPitch;
    }
}
