using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Controller2D))]
public class Player : MovementController
{
    Transform weaponSprites;
    ReskinAnimations reskin;
    bool dead = false;

    override protected void Start()
    {
        weaponSprites = transform.Find("Sprites");
        reskin = GetComponent<ReskinAnimations>();

        base.Start();
    }

    override protected void Update()
    {        
        if(attackController.currentHealth <= 0)
        {
            input = Vector2.zero;                
            jumpDown = false;
            jumpUp = false;
            attack = false;
            guard = false;
            base.Update();
            Physics2D.SyncTransforms();

            if(Input.GetKeyDown(KeyCode.R) && dead)
                Respawn();

            return;
        }

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        jumpDown = false;
        jumpUp = false;
        attack = false;
        guard = false;

        if(Input.GetButtonDown("Jump"))
            jumpDown = true;        
        if(Input.GetButtonUp("Jump"))
            jumpUp = true;
        if(Input.GetButtonDown("Fire1"))
            attack = true;

        if(Input.GetButton("Fire2"))
            guard = true;
        if(Input.GetButtonUp("Fire2"))
            guard = false;        

        if(guard)
            attackController.GuardUp();
        else
            attackController.GuardDown();

        if(guard || anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
            input = Vector2.zero;
        
        base.Update();

        weaponSprites.localScale = new Vector3(input.x == 0 ? weaponSprites.localScale.x : Mathf.Sign(input.x), 1, 1);

        Physics2D.SyncTransforms();
    }

    void Respawn()
    {
        GameManager.instance.Respawn();

        if(GameManager.instance.deaths > 1) return;
        
        dead = false;
        anim.SetTrigger("Respawn");
        anim.ResetTrigger("Death");
        attackController.currentHealth = attackController.maxHealth;
        attackController.dead = false;
        reskin.spriteSheetName = "skeleton";

        transform.position = GameManager.instance.respawnPoint;
    }

    void AnimDeath()
    {
        dead = true;
        GameManager.instance.GameOver();
    }
}