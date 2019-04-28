﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Enemy : MovementController
{
    public float findPlayerDelay = 0.5f;
    float findPlayerTime = 0;
    public float aggroRange = 10;

    [Header("Attack")]
    public float attackRange = 2.0f;
    public float attackCooldown;
    float attackTime;

    Transform player;

    Transform hurtboxTransform;
    public int facingDirection = 1;

    override protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        base.Start();

        hurtboxTransform = attackController.hurtbox.transform;
    }

    override protected void Update()
    {
        if(!canMove && player != null)
            return;

        findPlayerTime += Time.deltaTime;
        if(findPlayerTime > findPlayerDelay)
        {
            FindPlayer();
            findPlayerTime = 0;
        }

        attackTime -= Time.deltaTime;

        base.Update();

        if(attackController.hurtbox != null)
            hurtboxTransform.localScale = new Vector3(facingDirection, 1, 1);
    }

    void FindPlayer()
    {
        if(player == null) return;

        Vector2 direction = player.position - transform.position;

        facingDirection = direction.x > 0 ? 1 : -1;

        if(Mathf.Abs(direction.x) > aggroRange)
        {
            input = Vector2.zero; 
            return;
        }

        if(Mathf.Abs(direction.x) < attackRange)
        {
            direction = Vector2.zero;

            if(attackTime <= 0)
            {
                attack = true;
                attackTime = attackCooldown;
            }
        }

        input = new Vector2(direction.x > 0 ? 1 : direction.x < 0 ? -1 : 0, 0);

        if(controller.collisions.right || controller.collisions.left) input.y = 1;

        if(input.y > 0)
            jump = true;
    }
}
