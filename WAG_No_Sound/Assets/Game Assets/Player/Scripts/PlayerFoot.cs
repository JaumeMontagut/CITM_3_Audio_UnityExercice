////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    public MaterialChecker materialChecker;
    public List<AudioClip> footstepDirtSound;
    public List<AudioClip> footstepGrassSound;
    public List<AudioClip> footstepRubbleSound;
    public List<AudioClip> footstepSandSound;
    public List<AudioClip> footstepStoneSound;
    public List<AudioClip> footstepWaterSound;
    public List<AudioClip> footstepWoodSound;
    AudioSource audioSource;
    //public AK.Wwise.Event FootstepSound;
    WalkType walkType;

    #region private variables
    private bool inWater;
    #endregion

    //Newly added variables
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void PlayFootstepSound()
    {
        if (!inWater)
        {
            walkType = materialChecker.GetMaterial();
            //materialChecker.CheckMaterial(gameObject); //This also sets the material if a SoundMaterial is found!
        }
        else
            walkType = WalkType.WATER;

        if (audioSource == null)
            audioSource = PlayerManager.Instance.audioSource;

        int rand;

        switch (walkType)
        {
            case WalkType.DIRT:
                {
                    if(!PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepDirtSound.Count);

                    audioSource.PlayOneShot(footstepDirtSound[rand], playerMovement.GetVolumeBySpeed(0.3f));
                }
                break;
            case WalkType.GRASS:
                {
                    if (!PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepGrassSound.Count);

                    audioSource.PlayOneShot(footstepGrassSound[rand], playerMovement.GetVolumeBySpeed(0.3f));
                }
                break;
            case WalkType.RUBBLE:
                {
                    if (!PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepRubbleSound.Count);

                    audioSource.PlayOneShot(footstepRubbleSound[rand], playerMovement.GetVolumeBySpeed(0.3f));
                }
                break;
            case WalkType.SAND:
                {
                    if (!PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepSandSound.Count);

                    audioSource.PlayOneShot(footstepSandSound[rand], playerMovement.GetVolumeBySpeed(0.3f));
                }
                break;
            case WalkType.STONE:
                {
                    if (!PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepStoneSound.Count);

                    audioSource.PlayOneShot(footstepStoneSound[rand], playerMovement.GetVolumeBySpeed(0.3f));
                }
                break;
            case WalkType.WATER:
                {
                    if (!PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepWaterSound.Count);

                    audioSource.PlayOneShot(footstepWaterSound[rand], playerMovement.GetVolumeBySpeed(0.3f));
                }
                break;
            case WalkType.WOOD:
                {
                    if (!PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepWoodSound.Count);

                    audioSource.PlayOneShot(footstepWoodSound[rand], playerMovement.GetVolumeBySpeed(0.3f));
                }
                break;
        }

        //FootstepSound.Post(gameObject);
    }

    public void EnterWaterZone()
    {
        inWater = true;
    }

    public void ExitWaterZone()
    {
        inWater = false;
    }

}
