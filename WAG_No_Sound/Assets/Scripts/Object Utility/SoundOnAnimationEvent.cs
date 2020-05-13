////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnAnimationEvent : MonoBehaviour {

    //public List<AK.Wwise.Event> Sounds = new List<AK.Wwise.Event>();
    public List<AudioClip> Sounds = new List<AudioClip>();
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundWithIdx(int idx){
        //Sounds[idx].Post(gameObject);
        audioSource.PlayOneShot(Sounds[idx]);

    }
}
