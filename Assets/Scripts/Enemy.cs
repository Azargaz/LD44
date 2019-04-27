using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Enemy : MovementController
{
    public float findPlayerDelay = 0.5f;
    float findPlayerTime = 0;

    [Header("Attack")]
    public float attackRange = 2.0f;

    Transform player;

    override protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        base.Start();
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

        base.Update();
    }

    void FindPlayer()
    {
        Vector2 direction = player.position - transform.position;

        if(Mathf.Abs(direction.x) < attackRange) direction = Vector2.zero;

        input = new Vector2(direction.x > 0 ? 1 : direction.x < 0 ? -1 : 0, 0);

        if(controller.collisions.right || controller.collisions.left) input.y = 1;

        if(input.y > 0)
            jump = true;
    }
}
