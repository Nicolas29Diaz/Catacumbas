using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensajeFinal : MonoBehaviour
{
    public AudioClip startSound;       // Drag the initial sound here in the inspector
    public AudioClip secondSound;      // Drag the second sound here in the inspector
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayStartSound();
        Invoke("PlaySecondSound", 8.5f);
    }

    void PlayStartSound()
    {
        audioSource.clip = startSound;
        audioSource.Play();
    }

    void PlaySecondSound()
    {
        audioSource.clip = secondSound;
        audioSource.Play();
    }
}
