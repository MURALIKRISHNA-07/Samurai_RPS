using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HitPitch
{
    [Range(1, 2)]
    public float highPitch = 1.2f;
    [Range(1, 2)]
    public float midPitch = 1f;
    [Range(0, 1)]
    public float lowPitch = 0.8f;
}
public class AudioManager : MonoBehaviour
{
    public AudioSource countDown;
    public AudioSource hit;
    public HitPitch hitPitch;
    public AudioSource takeDamage;
    public AudioSource draw;
    public AudioSource indicator;

    public AudioSource bgm;
    public AudioSource natureAmbient;

    
    public void PlayHitSound(string attack, float delayTime)
    {
        switch (attack)
        {
            case "high":
                hit.pitch = hitPitch.highPitch;
                break;
            case "mid":
                hit.pitch = hitPitch.midPitch;
                break;
            case "low":
                hit.pitch = hitPitch.lowPitch;
                break;
        }
        hit.PlayDelayed(delayTime);
    }
    
    public void PlayTakeDamageSound(float delayTime)
    {
        takeDamage.PlayDelayed(delayTime);
    }
    
    public void PlayDrawSound(float delayTime)
    {
        draw.PlayDelayed(delayTime);
    }

    public void PlayCountDown(float delayTime)
    {
        countDown.PlayDelayed(delayTime);
    }
    
    public void PlayIndicator(string attack, float delayTime)
    {
        switch (attack)
        {
            case "high":
                indicator.pitch = hitPitch.highPitch;
                break;
            case "mid":
                indicator.pitch = hitPitch.midPitch;
                break;
            case "low":
                indicator.pitch = hitPitch.lowPitch;
                break;
        }
        indicator.PlayDelayed(delayTime);
    }

    private void PlayBackgroundSounds()
    {
        bgm.Play();
        natureAmbient.Play();
    }

    private void Start()
    {
        PlayBackgroundSounds();
    }
}
