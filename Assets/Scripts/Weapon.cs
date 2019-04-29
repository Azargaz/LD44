using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/Weapon", order = 2)]
public class Weapon : Item
{
    public int damage;
    public int knockbackStrength = 10;
    public int staminaCost = 10;

    public Vector2 hurtboxSize = new Vector2(0.5f, 0.5f);

    public bool jumpingAttack = false;

    public Sprite[] attackEffects;
}
