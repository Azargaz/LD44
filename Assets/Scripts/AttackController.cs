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
    public float hurtboxSize = 0.5f;
    public bool jumpingAttack;
    public int staminaCost = 10;

    public int defense = 0;

    public int maxHealth = 6;
    public int currentHealth;

    public int maxStamina = 50;
    public int currentStamina;
    public float staminaRegenInterval = 0.1f;
    float staminaRegenDelay = 0;
    public int staminaRegenerated = 1;

    public float invincibilityTime = 0.5f;
    float invincibilityDuration;

    Attack chosenAttack = new Attack() { name = "default", id = 1 };

    Animator anim;
    public Hurtbox hurtbox;

    MovementController movController;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movController = GetComponent<MovementController>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    void Update()
    {
        invincibilityDuration -= Time.deltaTime;

        if(currentHealth <= 0)
        {
            Death();
            return;
        }

        if (hurtbox != null)
        {
            hurtbox.targets |= targets;
            hurtbox.damage = damage;
            hurtbox.knockbackStrength = knockbackStrength;
            hurtbox.hurtboxSize = hurtboxSize;
        }

        anim.SetBool("JumpingAttack", jumpingAttack);

        staminaRegenDelay += Time.deltaTime;
        if(staminaRegenDelay > staminaRegenInterval)
        {
            staminaRegenDelay = 0;
            currentStamina += staminaRegenerated;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }
    }

    public void PerformAttack()
    {
        if(currentStamina >= staminaCost)
        {
            anim.SetTrigger("Attack" + chosenAttack.id);
            currentStamina -= staminaCost;
        }
    }

    public void GuardUp()
    {
        anim.SetBool("Defend", true);
    }

    public void GuardDown()
    {
        anim.SetBool("Defend", false);
    }

    public void TakeDamage(int dmg, int knockbackForce, int knockbackDirection)
    {
        if (invincibilityDuration > 0)
            return;

        if(anim.GetBool("Defend"))
        {            
            int staminaDiff = currentStamina - (defense * 10);

            if(staminaDiff >= 0)
            {                
                currentStamina -= defense * 10;
                dmg -= defense;
                dmg = Mathf.Clamp(dmg, 0, int.MaxValue);    
            }
            else
            {
                int newDefense = defense - (staminaDiff / 10);
                currentStamina -= newDefense * 10;
                dmg -= newDefense;
                dmg = Mathf.Clamp(dmg, 0, int.MaxValue);    
            }   
        }

        if(dmg == 0) return;

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
