using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/ActiveItem", order = 2)]
public class ActiveItem : Item
{
    public float cooldown;

}
