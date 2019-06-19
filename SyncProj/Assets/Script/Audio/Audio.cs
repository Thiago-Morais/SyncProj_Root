using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public void PlayAudio()
    {
        audioSource.Play();
    }
    public void StopAudio()
    {
        audioSource.Stop();
    }
}
