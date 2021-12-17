using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCrotroller : MonoBehaviour
{
    //UI音效
    public AudioSource m_UIAudio;

    private void Awake()
    {
        m_UIAudio = Camera.main.GetComponent<AudioSource>();
    }

    public void Play()
    {
        m_UIAudio.Play();
    }
}
