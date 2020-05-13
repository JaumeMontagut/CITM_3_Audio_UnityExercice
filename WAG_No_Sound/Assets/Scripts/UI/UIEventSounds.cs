////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventSounds : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    //public AK.Wwise.Event OnPointerDownSound;
    //public AK.Wwise.Event OnPointerUpSound;
    //public AK.Wwise.Event OnPointerEnterSound;
    //public AK.Wwise.Event OnPointerExitSound;

    //Newly added variables
    public AudioClip OnPointerDownSound;
    public AudioClip OnPointerUpSound;
    public AudioClip OnPointerEnterSound;
    public AudioClip OnPointerExitSound;

    private AudioSource audioSource;

    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //OnPointerDownSound.Post(gameObject);
        audioSource.PlayOneShot(OnPointerDownSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //OnPointerEnterSound.Post(gameObject);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(OnPointerEnterSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //OnPointerExitSound.Post(gameObject);
        audioSource.PlayOneShot(OnPointerExitSound);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //OnPointerUpSound.Post(gameObject);
        audioSource.PlayOneShot(OnPointerUpSound);
    }
}
