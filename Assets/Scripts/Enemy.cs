using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;

    float gravity;
    float jumpVelocity;
    [HideInInspector]
    public Vector3 velocity;
    float velocityXSmoothing;

    Vector2 input;
    bool jump;

    public float findPlayerDelay = 0.5f;
    float findPlayerTime = 0;

    [Header("Attack")]
    public float attackRange = 2.0f;

    [HideInInspector]
    public bool canMove = true;

    Controller2D controller;
    Transform player;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;   
    }

    void Update()
    {
        if(!canMove && player != null)
            return;

        findPlayerTime += Time.deltaTime;
        if(findPlayerTime > findPlayerDelay)
        {
            FindPlayer();
            findPlayerTime = 0;
        }

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (jump && controller.collisions.below)
        {
            velocity.y = jumpVelocity;   
            jump = false;         
        }      

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
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
