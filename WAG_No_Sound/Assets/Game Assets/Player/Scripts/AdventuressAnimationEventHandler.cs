////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdventuressAnimationEventHandler : MonoBehaviour
{
    [Header("Wwise")]
    private AudioSource audioSource;
    public List<AudioClip> swingAudios;
    public AudioClip GetItem;

    //public AK.Wwise.Event Swing = new AK.Wwise.Event();
    //public AK.Wwise.Event GetItem = new AK.Wwise.Event();
    //public AK.Wwise.Trigger GetItemStinger = new AK.Wwise.Trigger();

    [Header("Object Links")]
    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private GameObject runParticles;

    private PlayerFoot foot_L;
    private PlayerFoot foot_R;

    #region private variables
    private bool hasPausedMovement;
    private readonly int canShootMagicHash = Animator.StringToHash("CanShootMagic");
    private readonly int isAttackingHash = Animator.StringToHash("IsAttacking");
    #endregion

    private void Awake()
    {
        GameObject L = GameObject.Find("toe_left");
        GameObject R = GameObject.Find("toe_right");
        if (L != null)
        {
            foot_L = L.GetComponent<PlayerFoot>();
        }
        else {
            print("Left foot missing");
        }
        if (R != null)
        {
            foot_R = R.GetComponent<PlayerFoot>();
        }
        else
        {
            print("Right foot missing");
        }

        audioSource = GetComponent<AudioSource>();
    }


    void enableWeaponCollider()
    {
        if (PlayerManager.Instance != null && PlayerManager.Instance.equippedWeaponInfo != null)
        {
            PlayerManager.Instance.equippedWeaponInfo.EnableHitbox();
        }
    }

    void disableWeaponCollider()
    {
        if (PlayerManager.Instance != null && PlayerManager.Instance.equippedWeaponInfo != null)
        {
            PlayerManager.Instance.equippedWeaponInfo.DisableHitbox();
        }

    }

    void ScreenShake()
    {
        PlayerManager.Instance.cameraScript.CamShake(new PlayerCamera.CameraShake(0.4f, 0.7f));
    }

    bool onCooldown = false;
    public enum FootSide { left, right };
    public void TakeFootstep(FootSide side)
    {
        if (foot_L != null && foot_R != null) {
            if (!PlayerManager.Instance.inAir && !onCooldown)
            {
                Vector3 particlePosition;

                if (side == FootSide.left )
                {
                    //if (foot_L.FootstepSound/*.Validate()*/)
                    
                        foot_L.PlayFootstepSound();
                        particlePosition = foot_L.transform.position;
                        FootstepParticles(particlePosition);
                   
                }
                else
                {
                    //if (foot_R.FootstepSound/*.Validate()*/)
                    //{
                        foot_R.PlayFootstepSound();
                        particlePosition = foot_R.transform.position;
                        FootstepParticles(particlePosition);
                   
                }
            }
        }
    }

    void FootstepParticles(Vector3 particlePosition) {
        GameObject p = Instantiate(runParticles, particlePosition + Vector3.up * 0.1f, Quaternion.identity) as GameObject;
        p.transform.parent = SceneStructure.Instance.TemporaryInstantiations.transform;
        Destroy(p, 5f);
        StartCoroutine(FootstepCooldown());
    }

    IEnumerator FootstepCooldown()
    {
        onCooldown = true;
        yield return new WaitForSecondsRealtime(0.2f);
        onCooldown = false;
    }

    void ReadyToShootMagic()
    {
        PlayerManager.Instance.playerAnimator.SetBool(canShootMagicHash, true);
    }

    public enum AttackState { NotAttacking, Attacking };
    void SetIsAttacking(AttackState state)
    {
        if (state == AttackState.NotAttacking)
        {
            playerAnimator.SetBool(isAttackingHash, false);
        }
        else
        {
            playerAnimator.SetBool(isAttackingHash, true);
        }
    }

    public void Weapon_SwingEvent()
    {
        // PLAY SOUND

        Weapon W = PlayerManager.Instance.equippedWeaponInfo;
        int random;

        if (W.weaponAnimationType == WeaponAnimationTypes.OneHanded)
            random = Random.Range(0, 5);
        else
            random = Random.Range(5, swingAudios.Count);

        if(Random.Range(0,3) != 0)
            audioSource.PlayOneShot(swingAudios[random]);
        //W.WeaponTypeSwitch.SetValue(PlayerManager.Instance.weaponSlot);
        //Swing.Post(PlayerManager.Instance.weaponSlot);
    }

    public void PauseMovement()
    {
        if (!hasPausedMovement)
        {
            hasPausedMovement = true;
            PlayerManager.Instance.motor.SlowMovement();
        }
    }

    public void ResumeMovement()
    {
        if (hasPausedMovement)
        {
            hasPausedMovement = false;
            PlayerManager.Instance.motor.UnslowMovement();
        }
    }

    public void FreezeMotor()
    {
        StartCoroutine(PickupEvent());
    }

    private IEnumerator PickupEvent()
    {
        PlayerManager.Instance.PauseMovement(gameObject);
        yield return new WaitForSeconds(2f);
        PlayerManager.Instance.ResumeMovement(gameObject);
    }

    public void PickUpItem()
    {
        audioSource.PlayOneShot(GetItem);
        //PlayerManager.Instance.PickUpEvent();
        //GetItem.Post(this.gameObject);
        //GetItemStinger.Post(GameManager.Instance.MusicGameObject);
    }

    public void WeaponSound()
    {
        Weapon EquippedWeapon = PlayerManager.Instance.equippedWeaponInfo;
        //EquippedWeapon.WeaponImpact.Post(EquippedWeapon.transform.parent.gameObject);
    }
}
