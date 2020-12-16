using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _gunShot;

    private void Awake()
    {
        _gunShot = GetComponent<AudioSource>();
    }

    public void PlayShotAudio()
    {
        _gunShot.Play();
    }
}
