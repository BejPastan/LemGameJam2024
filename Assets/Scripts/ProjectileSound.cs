using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProjectileSound : MonoBehaviour
{
    [Header("Sound Settings")]
    private AudioSource audioSource;
    public float volume = 1;

    void Awake()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        PlaySound(audioSource.clip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            Debug.Log("Sound played.");
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        }
    }
}
