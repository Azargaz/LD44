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

    public int maxHealth = 6;
    public int currentHealth;

    public float invincibilityTime = 0.5f;
    float invincibilityDuration;    

    Animator anim;
    public Hurtbox hurtbox;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        
        if(hurtbox != null)
        {                
            hurtbox.targets |= targets;
            hurtbox.damage = damage;
        }
    }

    void Update()
    {
        invincibilityDuration -= Time.deltaTime;
    }

    public void PerformAttack(Attack attack)
    {
        anim.SetTrigger("Attack" + attack.id);
    }

    public void TakeDamage(int dmg)
    {
        if(invincibilityDuration > 0)
            return;
        
        currentHealth -= dmg;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        invincibilityDuration = invincibilityTime;
    }

}
