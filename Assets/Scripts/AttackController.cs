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
    public Vector2 hurtboxSize = new Vector2(0.5f, 0.5f);
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
    public int defenseStaminaMultiplier = 20;

    public float invincibilityTime = 0.5f;
    float invincibilityDuration;

    public Color damageNumbersColor;
    public FloatingNumbers damageNumbersObj;

    Attack chosenAttack = new Attack() { name = "default", id = 1 };

    public bool dead;

    Animator anim;
    SpriteRenderer spriteRenderer;
    Color spriteColor;
    public Hurtbox hurtbox;

    MovementController movController;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteColor = spriteRenderer.color;
        movController = GetComponent<MovementController>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    void Update()
    {
        invincibilityDuration -= Time.deltaTime;

        if(invincibilityDuration > 0)
            spriteRenderer.color = spriteColor - new Color(-0.2f, -0.2f, -0.2f, 0.5f);
        else
            spriteRenderer.color = spriteColor;

        if(currentHealth <= 0 && !dead)
        {
            dead = true;
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

        if(!anim.GetBool("Defend"))
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
            staminaRegenDelay = 0;
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
        if (invincibilityDuration > 0 || currentHealth <= 0)
            return;

        if(anim.GetBool("Defend"))
        {            
            int dmgDefended = dmg > defense ? defense : dmg;
            int staminaDiff = currentStamina - (dmgDefended * defenseStaminaMultiplier);

            if(staminaDiff >= 0)
            {                
                currentStamina -= dmgDefended * defenseStaminaMultiplier;
                dmg -= defense;
                dmg = Mathf.Clamp(dmg, 0, int.MaxValue);    
            }
            else
            {
                int newDefense = defense + (staminaDiff / defenseStaminaMultiplier);
                currentStamina -= newDefense * defenseStaminaMultiplier;
                dmg -= newDefense;
                dmg = Mathf.Clamp(dmg, 0, int.MaxValue);
            }   

            staminaRegenDelay = 0;
        }

        FloatingNumbers spawnedNumbers = Instantiate(damageNumbersObj.gameObject, transform.position, Quaternion.identity).GetComponent<FloatingNumbers>();
        spawnedNumbers.color = damageNumbersColor;
        spawnedNumbers.number = dmg;
        currentHealth -= dmg;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        invincibilityDuration = invincibilityTime;

        movController.Knockback(knockbackForce, knockbackDirection);
    }

    void Death()
    {
        anim.SetTrigger("Death");        

        if(gameObject.tag != "Player") GameManager.instance.kills++;
    }

    void AnimDestroy()
    {
        Destroy(gameObject);
    }

}
