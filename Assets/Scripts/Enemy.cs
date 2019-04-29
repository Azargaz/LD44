using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Enemy : MovementController
{
    public float findPlayerDelay = 0.5f;
    float findPlayerTime = 0;
    public float aggroRange = 10;
    public float jumpRange = 1.0f;

    [Header("Attack")]
    public float attackRange = 2.0f;
    public float attackCooldown;
    float attackCD;

    public float defendCooldown;
    float defendCD;
    public float defendDuration;
    float defendTime;

    public bool hasWeapons = false;

    Transform player;

    Transform hurtboxTransform;
    public Transform sprites;
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

        attackCD -= Time.deltaTime;
        defendCD -= Time.deltaTime;
        defendTime -= Time.deltaTime;

        if(defendTime <= 0)
            guard = false;
        
        if(guard)
        {
            input = Vector2.zero;
            attackController.GuardUp();
        }
        else
        {
            attackController.GuardDown();
        }

        base.Update();

        if(sprites != null)
            sprites.localScale = new Vector3(facingDirection, 1, 1);
        else if(attackController.hurtbox != null)
            hurtboxTransform.localScale = new Vector3(facingDirection, 1, 1);

        sprite.flipX = facingDirection < 0;
    }

    void FindPlayer()
    {
        if(player == null) return;

        Vector2 direction = player.position - transform.position;

        facingDirection = direction.x > 0 ? 1 : -1;

        if(Vector2.Distance(player.position, transform.position) > aggroRange)
        {
            input = Vector2.zero; 
            return;
        }

        if(Vector2.Distance(player.position, transform.position) < attackRange + Random.Range(0, 0.25f) && !guard)
        {
            direction = Vector2.zero;

            if(attackCD <= 0)
            {
                attack = true;
                attackCD = attackCooldown + Random.Range(0, 0.5f);
            }
            else if(defendCD <= 0 && !anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
            {
                guard = true;
                defendTime = defendDuration + Random.Range(0, 0.5f);
                defendCD = defendCooldown + Random.Range(0, 0.5f);
            }
        }
        else if(Vector2.Distance(player.position, transform.position) > attackRange + 1.0f)
        {
            defendTime = 0;
        }

        input = new Vector2(direction.x > 0 ? 1 : direction.x < 0 ? -1 : 0, direction.y > jumpRange ? 1 : direction.y < -jumpRange ? -1 : 0);
        // if(Mathf.Abs(direction.x) > attackRange) input.y = 0;

        if((controller.collisions.right && facingDirection == 1) || (controller.collisions.left && facingDirection == -1)) input.y = 1;

        if(input.y > 0)
            jump = true;
    }
}
