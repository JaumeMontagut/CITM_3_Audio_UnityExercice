////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPickup : MonoBehaviour {

    public bool playSpawnSoundAtSpawn = true;
    //public AK.Wwise.Event spawnSound;
	public AudioClip spawnSound;
	AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
        if (playSpawnSoundAtSpawn){
			audioSource.PlayOneShot(spawnSound);
            //spawnSound.Post(gameObject);
        }
	}

	public void AddCoinToCoinHandler(){
		InteractionManager.SetCanInteract(this.gameObject, false);
		GameManager.Instance.coinHandler.AddCoin ();
		//Destroy (gameObject, 0.1f); //TODO: Pool instead?
	}
}
