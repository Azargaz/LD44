using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [HideInInspector]
    public Weapon[] weapons;
    [HideInInspector]
    public Shield[] shields;

    public Transform weaponContainer;
    public Transform shieldContainer;

    public GameObject onHoverObj;

    public GameObject shopItem;

    void Start()
    {
        Weapon[] loadedWeapons = Resources.LoadAll<Weapon>("Data/");
        weapons = loadedWeapons;
        Shield[] loadedShields = Resources.LoadAll<Shield>("Data/");
        shields = loadedShields;
        
        Array.Sort(shields);
        Array.Sort(weapons);

        foreach (Weapon weapon in weapons)
        {
            if(!weapon.purchasable) continue;

            GameObject spawnedWeapon = Instantiate(shopItem, transform.position, Quaternion.identity, weaponContainer);
            spawnedWeapon.GetComponent<OnHoverText>().onHover = onHoverObj;
            spawnedWeapon.GetComponent<OnHoverText>().shopItem = weapon;
            spawnedWeapon.GetComponent<ShopItem>().price = weapon.price;
            spawnedWeapon.GetComponent<ShopItem>().item = weapon;
        }

        foreach (Shield shield in shields)
        {
            if(!shield.purchasable) continue;
            
            GameObject spawnedShield = Instantiate(shopItem, transform.position, Quaternion.identity, shieldContainer);
            spawnedShield.GetComponent<OnHoverText>().onHover = onHoverObj;
            spawnedShield.GetComponent<OnHoverText>().shopItem = shield;
            spawnedShield.GetComponent<ShopItem>().price = shield.price;
            spawnedShield.GetComponent<ShopItem>().item = shield;
        }
    }
}
