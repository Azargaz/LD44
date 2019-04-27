using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/Item", order = 2)]
[System.Serializable]
public class Item : ScriptableObject
{
    public string objectName;
    public string description;
    public int price;

    public Sprite sprite;
}