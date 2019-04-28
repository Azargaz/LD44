using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/Item", order = 2)]
[System.Serializable]
public class Item : ScriptableObject, IComparable
{
    public int shopOrder;
    public string objectName;
    public string description;
    public int price;

    public Sprite sprite;
    public Sprite shopSprite;

    public int CompareTo(object obj)
    {
        if(obj == null) return 1;

        Item item = obj as Item;
        if(item != null)
            return this.shopOrder.CompareTo(item.shopOrder);
        else
            throw new ArgumentException("Object is not an Item!");
    }
}