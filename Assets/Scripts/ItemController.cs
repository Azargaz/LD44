using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemController : MonoBehaviour
{
    public Weapon[] weaponList;
    public Shield[] shieldList;

    public Weapon equippedWeapon;
    public Shield equippedShield;

    [Header("SpriteRenderers")]
    public SpriteRenderer weaponSprite;
    public SpriteRenderer shieldSprite;

    public SpriteRenderer weaponEffectSprite1;
    public SpriteRenderer weaponEffectSprite2;

    AttackController attackController;

    public bool randomEquipment;

    void Start()
    {
        attackController = GetComponent<AttackController>();

        if(randomEquipment)
        {
            equippedWeapon = weaponList[Random.Range(0, weaponList.Length)];
            equippedShield = shieldList[Random.Range(0, shieldList.Length)];
        }
    }

    void Update()
    {
        weaponSprite.sprite = equippedWeapon.sprite;
        shieldSprite.sprite = equippedShield.sprite;
        
        weaponEffectSprite1.sprite = equippedWeapon.attackEffects[0];
        weaponEffectSprite2.sprite = equippedWeapon.attackEffects[1];

        attackController.damage = equippedWeapon.damage;
        attackController.knockbackStrength = equippedWeapon.knockbackStrength;
        attackController.hurtboxSize = equippedWeapon.hurtboxSize;
        attackController.jumpingAttack = equippedWeapon.jumpingAttack;
        attackController.staminaCost = equippedWeapon.staminaCost;

        attackController.defense = equippedShield.defense;
        attackController.defenseStaminaMultiplier = equippedShield.staminaPerDefensePoint;
    }
}