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

    private AudioSource audioSourceDown;
    private AudioSource audioSourceUp;
    private AudioSource audioSourceEnter;
    private AudioSource audioSourceExit;

    public void Start()
    {
        audioSourceDown = gameObject.AddComponent<AudioSource>();
        audioSourceDown.clip = OnPointerDownSound;
        audioSourceUp = gameObject.AddComponent<AudioSource>();
        audioSourceEnter = gameObject.AddComponent<AudioSource>();
        audioSourceExit = gameObject.AddComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //OnPointerDownSound.Post(gameObject);
        audioSourceDown.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //OnPointerEnterSound.Post(gameObject);
        if(!audioSourceEnter.isPlaying)
            audioSourceEnter.PlayOneShot(OnPointerEnterSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //OnPointerExitSound.Post(gameObject);
        audioSourceExit.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //OnPointerUpSound.Post(gameObject);
        audioSourceUp.Play();
    }
}
