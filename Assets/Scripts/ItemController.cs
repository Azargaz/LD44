using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemController : MonoBehaviour
{
    public Item[] itemList;

    public Weapon equippedWeapon;

    [Header("SpriteRenderers")]
    public SpriteRenderer weaponSprite;
    public SpriteRenderer shieldSprite;

    public SpriteRenderer weaponEffectSprite1;
    public SpriteRenderer weaponEffectSprite2;

    void Update()
    {
        weaponSprite.sprite = equippedWeapon.sprite;
        weaponEffectSprite1.sprite = equippedWeapon.attackEffects[0];
        weaponEffectSprite2.sprite = equippedWeapon.attackEffects[1];
    }
}