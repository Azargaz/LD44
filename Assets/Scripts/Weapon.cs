using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/Weapon", order = 2)]
public class Weapon : Item
{
    public int damage;

    public Sprite[] attackEffects;
}
