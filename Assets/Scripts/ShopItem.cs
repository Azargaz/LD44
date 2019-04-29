using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int price;
    public Item item;

    bool bought = false;

    ItemController playerItemController;
    OnHoverText onHoverTxt;

    void Start()
    {
        playerItemController = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemController>();
        onHoverTxt = GetComponent<OnHoverText>();
    }

    public void OnClick()
    {
        if(!bought)
        {
            if(MoneyController.instance.RemoveAmount(price))
            {
                bought = true;
                onHoverTxt.itemBought = true;
                EquipItem();
            }
            else
            {
                InsufficientFunds();
            }
        }
        else
        {
            EquipItem();
        }
    }

    void InsufficientFunds()
    {

    }

    void EquipItem()
    {
        if(item.GetType() == typeof(Weapon))
        {
            Weapon weapon = item as Weapon;
            playerItemController.equippedWeapon = weapon;
            return;
        }

        if(item.GetType() == typeof(Shield))
        {
            Shield shield = item as Shield;
            playerItemController.equippedShield = shield;
            return;
        }
    }
}
