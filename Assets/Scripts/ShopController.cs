using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public Weapon[] weapons;
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
            GameObject spawnedWeapon = Instantiate(shopItem, transform.position, Quaternion.identity, weaponContainer);
            spawnedWeapon.GetComponent<OnHoverText>().onHover = onHoverObj;
            spawnedWeapon.GetComponent<OnHoverText>().shopItem = weapon;
        }

        foreach (Shield shield in shields)
        {
            GameObject spawnedShield = Instantiate(shopItem, transform.position, Quaternion.identity, shieldContainer);
            spawnedShield.GetComponent<OnHoverText>().onHover = onHoverObj;
            spawnedShield.GetComponent<OnHoverText>().shopItem = shield;
        }
    }
}
