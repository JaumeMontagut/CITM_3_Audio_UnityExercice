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

    public void PlayFootstepSound()
    {
        if (!inWater)
        {
            walkType = materialChecker.GetMaterial();
            //materialChecker.CheckMaterial(gameObject); //This also sets the material if a SoundMaterial is found!
        }

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        int rand;

        switch (walkType)
        {
            case WalkType.DIRT:
                {
                    if(PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepDirtSound.Count);

                    audioSource.PlayOneShot(footstepDirtSound[rand]);
                }
                break;
            case WalkType.GRASS:
                {
                    if (PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepGrassSound.Count);

                    audioSource.PlayOneShot(footstepGrassSound[rand]);
                }
                break;
            case WalkType.RUBBLE:
                {
                    if (PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepRubbleSound.Count);

                    audioSource.PlayOneShot(footstepRubbleSound[rand]);
                }
                break;
            case WalkType.SAND:
                {
                    if (PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepSandSound.Count);

                    audioSource.PlayOneShot(footstepSandSound[rand]);
                }
                break;
            case WalkType.STONE:
                {
                    if (PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepStoneSound.Count);

                    audioSource.PlayOneShot(footstepStoneSound[rand]);
                }
                break;
            case WalkType.WATER:
                {
                    if (PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepWaterSound.Count);

                    audioSource.PlayOneShot(footstepWaterSound[rand]);
                }
                break;
            case WalkType.WOOD:
                {
                    if (PlayerManager.Instance.isSprinting)
                        rand = Random.Range(0, 5);
                    else
                        rand = Random.Range(6, footstepWoodSound.Count);

                    audioSource.PlayOneShot(footstepWoodSound[rand]);
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
