using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TimeTriggeredSFX : MonoBehaviour
{
    AudioSource audioSource;
    ResetTimeout resetTimeout;

    [SerializeField]
    float playTime = 0.0f;

    bool played = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        resetTimeout = FindObjectOfType<ResetTimeout>();
    }

    public void PlaySFX()
    {
        audioSource.Play();
        played = true;
    }

    private void Update()
    {
        playTime -= Time.deltaTime;
        if(playTime <= 0.0f && !played)
        {
            PlaySFX();
        }
    }
}
