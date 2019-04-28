using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/Shield", order = 2)]
public class Shield : Item
{
    public int defense;
    public int staminaPerDefensePoint = 20;
}
