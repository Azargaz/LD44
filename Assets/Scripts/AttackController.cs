using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Attack
{
    public string name;
    public int id;
}

public class AttackController : MonoBehaviour
{
    public int damage = 1;
    public LayerMask targets;

    public int knockbackStrength = 10;

    public int maxHealth = 6;
    public int currentHealth;

    public float invincibilityTime = 0.5f;
    float invincibilityDuration;

    Attack chosenAttack = new Attack() { name = "default", id = 1 };

    Animator anim;
    public Hurtbox hurtbox;

    MovementController movController;

    void Start()
    {
        anim = GetComponent<Animator>();
        movController = GetComponent<MovementController>();
        currentHealth = maxHealth;

        if (hurtbox != null)
        {
            hurtbox.targets |= targets;
            hurtbox.damage = damage;
            hurtbox.knockbackStrength = knockbackStrength;
        }
    }

    void Update()
    {
        invincibilityDuration -= Time.deltaTime;

        if(currentHealth <= 0)
            Death();
    }

    public void PerformAttack()
    {
        anim.SetTrigger("Attack" + chosenAttack.id);
    }

    public void TakeDamage(int dmg, int knockbackForce, int knockbackDirection)
    {
        if (invincibilityDuration > 0)
            return;

        currentHealth -= dmg;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        invincibilityDuration = invincibilityTime;

        movController.Knockback(knockbackForce, knockbackDirection);
    }

    void Death()
    {
        Destroy(gameObject);
    }

}
