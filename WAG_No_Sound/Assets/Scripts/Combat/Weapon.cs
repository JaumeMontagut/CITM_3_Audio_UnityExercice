////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Weapon : MonoBehaviour, IInteractable
{
    [Header("WWISE")]
    //public AK.Wwise.Event WeaponImpact = new AK.Wwise.Event();
    //public AK.Wwise.Switch WeaponTypeSwitch = new AK.Wwise.Switch();
    public List<AudioClip> barrelImpact;
    public List<AudioClip> boxImpact;
    public List<AudioClip> stoneImpact;
    public List<AudioClip> woodImpact;
    public List<AudioClip> dirtImpact;
    public List<AudioClip> grassImpact;
    public List<AudioClip> attacksSound;
    AudioSource audioSource;

    [Header("Combo Actions")]
    //public AK.Wwise.Event ComboEvent = new AK.Wwise.Event();
    //public AK.Wwise.State WeaponCombo1 = new AK.Wwise.State();
    //public AK.Wwise.State WeaponCombo2 = new AK.Wwise.State();
    //public AK.Wwise.State WeaponCombo3 = new AK.Wwise.State();
    [Space(20f)]

    public WeaponTypes weaponType = WeaponTypes.Dagger;
    public WeaponAnimationTypes weaponAnimationType = WeaponAnimationTypes.OneHanded;

    public bool equipped = false;
    public bool playerWeapon = false;

    [Header("Weapon Objects")]
    public GameObject prefab;
    public Collider hitbox;
    public GameObject hitEffect;

    [Header("Weapon Stats")]
    public float BaseDamage;
    public float attackCooldown;
    public float attackFrame;
    public float animationSpeedMultiplier = 1;
    public float knockbackStrength = 1f;
    public bool PickupEventOnPickup = true;

    [Header("Combo Info")]
    public float comboCompletionBonusDamage = 0;
    public int maxComboHits;
    public float postComboCooldown;
    public bool swingDash;
    public float dashAmount;
    public bool TriggerDamageAllowed = false;

    public UnityEvent OnEquip;
    public UnityEvent OnUnequip;

    private List<GameObject> alreadyHitObjects = new List<GameObject>();

    #region hidden public variables
    [HideInInspector]
    public bool applyBonusDamage = false;
    #endregion

    public void EnableHitbox()
    {
        //Reset list of already hit GameObjects (since this is a new swing)
        alreadyHitObjects.Clear();
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    //This method is called by the <Pickup> script
    public void OnInteract()
    {
        if (playerWeapon && !equipped)
        {
            if (!InteractionManager.inConversation)
            {
                // Add to picked up weapons
                if (!PlayerManager.Instance.pickedUpWeapons.Contains(weaponType))
                {
                    if (PickupEventOnPickup)
                    {
                        PlayerManager.Instance.StartPickupEvent();
                    }
                    PlayerManager.Instance.pickedUpWeapons.Add(weaponType);
                    PlayerManager.Instance.pickedUpWeaponObjects.Add(gameObject);
                }

                EquipWeapon();
            }
        }
    }

    void Start()
    {
        prefab = gameObject;
        if (!playerWeapon)
        {
            BaseDamage = GetComponentInParent<Creature>().AttackDamage;
        }
        else
        {
            Physics.IgnoreCollision(hitbox, PlayerManager.Instance.playerCollider);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void EquipWeapon()
    {
        // destroy the pickup script 
        SetPickupEnabled(false);

        prefab.transform.position = PlayerManager.Instance.weaponSlot.transform.position;
        prefab.transform.rotation = PlayerManager.Instance.weaponSlot.transform.rotation;
        PlayerManager.Instance.Inventory_EquipWeapon(gameObject);

        PlayerManager.Instance.equippedWeaponInfo = this;
        PlayerManager.Instance.equippedWeapon = prefab;
        prefab.transform.parent = PlayerManager.Instance.weaponSlot.transform;
        Utility.StripGameObjectFromComponents(gameObject, typeof(Pickup));
        equipped = true;
    }

    public void UnequipWeapon()
    {
        PlayerManager.Instance.Inventory_UnequipWeapon(gameObject);
    }

    void SetPickupEnabled(bool enabled)
    {
        var pickupScript = gameObject.GetComponent<Pickup>();
        if (pickupScript != null)
        {
            pickupScript.enabled = enabled;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (TriggerDamageAllowed && !playerWeapon)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Attack attack = new Attack(BaseDamage, transform.position - PlayerManager.Instance.player.transform.position, knockbackStrength);

                if (!alreadyHitObjects.Contains(col.gameObject))
                {
                    SetAndPlayWeaponImpact(col.gameObject);
                    GameManager.DamageObject(col.gameObject, attack);
                }
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (playerWeapon)
        {
            if (equipped && col.gameObject.tag != "Player")
            {
                Vector3 playerToHitPoint = col.contacts[0].point - PlayerManager.Instance.player.transform.position;
                Attack attack = new Attack(BaseDamage, playerToHitPoint, knockbackStrength, SwingTypes.None, weaponType, col.contacts[0].point);

                int rand_attack = Random.Range(0, 2);
                int rand_light_sound = Random.Range(0, 5);
                int rand_heavy_sound = Random.Range(6, attacksSound.Count);
                AnimatorStateInfo currentAnimation = PlayerManager.Instance.playerAnimator.GetCurrentAnimatorStateInfo(0);
                if (currentAnimation.IsName("Player_RightSwing"))
                {
                    attack.swingType = SwingTypes.Right;
                    //WeaponCombo1.SetValue();
                }
                else if (currentAnimation.IsName("Player_LeftSwing"))
                {
                    attack.swingType = SwingTypes.Left;
                    //WeaponCombo2.SetValue();
                }
                else if (currentAnimation.IsName("Player_TopSwing"))
                {
                    attack.swingType = SwingTypes.Top;
                    //WeaponCombo3.SetValue();
                }

                if (!alreadyHitObjects.Contains(col.gameObject))
                {
                    //get material of the contact point
                    SoundMaterial sm = col.gameObject.GetComponent<SoundMaterial>();
                    if (sm != null)
                    {

                        uint thisSwitch = 0;
                        //TODO WEAPONS
                        //AkSoundEngine.GetSwitch((uint)sm.material.GroupId, transform.parent.gameObject, out thisSwitch);
                        ////print("Current Switch: "+ thisSwitch +", New: "+ sm.material.ID);

                        switch (sm.walkType)
                        {
                            case WalkType.BARREL:
                                    audioSource.PlayOneShot(barrelImpact[(int)weaponType - 1]);
                                break;
                            case WalkType.BOX:
                                    audioSource.PlayOneShot(boxImpact[(int)weaponType - 1]);
                                break;
                            case WalkType.STONE:
                                if(weaponType == WeaponTypes.Sword)
                                    audioSource.PlayOneShot(stoneImpact[1]);
                                else if (weaponType == WeaponTypes.Axe)
                                    audioSource.PlayOneShot(stoneImpact[2]);
                                else
                                    audioSource.PlayOneShot(stoneImpact[0]);
                                break;
                            case WalkType.WOOD:
                                if (weaponType == WeaponTypes.Sword)
                                    audioSource.PlayOneShot(woodImpact[1]);
                                else
                                    audioSource.PlayOneShot(woodImpact[0]);
                                break;
                            case WalkType.DIRT:
                                if (weaponType == WeaponTypes.Sword)
                                    audioSource.PlayOneShot(dirtImpact[1]);
                                else if (weaponType == WeaponTypes.Axe)
                                    audioSource.PlayOneShot(dirtImpact[2]);
                                else
                                    audioSource.PlayOneShot(dirtImpact[0]);
                                break;
                            case WalkType.GRASS:
                                if (weaponType == WeaponTypes.Sword)
                                    audioSource.PlayOneShot(grassImpact[1]);
                                else if (weaponType == WeaponTypes.Axe)
                                    audioSource.PlayOneShot(grassImpact[2]);
                                else
                                    audioSource.PlayOneShot(grassImpact[0]);
                                break;
                            case WalkType.RUBBLE:
                            case WalkType.SAND:
                            default:
                                audioSource.Play();
                                break;

                        }
                    }

                    SetAndPlayWeaponImpact(col.gameObject);
                    GameManager.DamageObject(col.gameObject, attack);

                    GameObject hit = Instantiate(hitEffect, transform.position, Quaternion.identity) as GameObject; //TODO: Pool hit effects
                    Destroy(hit, 5f);

                    if (col.gameObject.layer == LayerMask.NameToLayer("Agent"))
                    {
                        //ComboEvent.Post(transform.parent.gameObject);
                        attack.damage += applyBonusDamage ? comboCompletionBonusDamage : 0;

                        float newTimeScale = applyBonusDamage ? 0.2f : 0.5f;
                        float transitionTime = 0.1f;
                        float holdTime = applyBonusDamage ? 0.2f : 0.1f;
                        float shakeDuration = applyBonusDamage ? 0.3f : 0.15f;
                        float shakeScale = applyBonusDamage ? 0.2f : 0.1f;
                        
                        GameManager.Instance.gameSpeedHandler.SetGameSpeed(gameObject.GetInstanceID(), newTimeScale, transitionTime, transitionTime, holdTime);
                        PlayerManager.Instance.CamShake(new PlayerCamera.CameraShake(shakeScale, shakeDuration));
                    }
                }
            }
        }
        else
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Attack attack = new Attack(BaseDamage, col.contacts[0].point - PlayerManager.Instance.player.transform.position, BaseDamage);
                GameManager.DamageObject(col.gameObject, attack);
                //WeaponTypeSwitch.SetValue(transform.parent.gameObject); // Weapon Type
                //WeaponImpact.Post(transform.parent.gameObject);
            }
        }
    }
    void SetAndPlayWeaponImpact(GameObject HitObj){
        //print("Impact");
        //WeaponTypeSwitch.SetValue(transform.parent.gameObject); // Weapon Type

        switch(weaponType)
        {
            case WeaponTypes.Dagger:
                {
                    
                }
                break;
            case WeaponTypes.Sword:
                break;
            case WeaponTypes.Axe:
                break;
        }

        alreadyHitObjects.Add(HitObj);
        //WeaponImpact.Post(transform.parent.gameObject);

    }

}
