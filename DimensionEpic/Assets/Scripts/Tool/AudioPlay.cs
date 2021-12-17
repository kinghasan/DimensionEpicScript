using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

public class AudioPlay : MonoBehaviour
{
    public AudioClip m_UseAudio;
    public AudioSource m_PlaySource;

    private void Awake()
    {
        m_PlaySource = gameObject.GetComponent<AudioSource>();
        m_PlaySource.clip = m_UseAudio;
        Button button = gameObject.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(delegate ()
           {
               m_PlaySource.clip = m_UseAudio;
               m_PlaySource.Play();
           });
        }
    }

    public void Play(AudioClip clip)
    {
        m_PlaySource.clip = clip;
        m_PlaySource.Play();
    }
}
