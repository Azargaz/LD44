using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Controller2D))]
public class Player : MovementController
{
    Transform weaponSprites;

    override protected void Start()
    {
        weaponSprites = transform.Find("Sprites");

        base.Start();
    }

    override protected void Update()
    {        
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
}