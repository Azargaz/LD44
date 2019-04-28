using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Controller2D))]
public class MovementController : MonoBehaviour
{
    [Header("Movement")]
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    [HideInInspector]
    public Vector3 velocity;
    float velocityXSmoothing;

    [HideInInspector]
    public bool canMove = true;
    
    protected bool grounded;
    
    protected Controller2D controller;
    protected AttackController attackController;
    protected Animator anim;

    protected SpriteRenderer sprite;

    protected Vector2 input;
    protected bool jumpUp;
    protected bool jumpDown;
    protected bool jump;
    protected bool attack;
    protected bool guard;

    protected virtual void Start()
    {
        controller = GetComponent<Controller2D>();
        attackController = GetComponent<AttackController>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    protected virtual void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
            controller.jumpDown = false;
        }

        grounded = controller.collisions.below;

        anim.SetBool("Grounded", grounded);
        anim.SetFloat("InputX", Mathf.Abs(input.x));
        sprite.flipX = input.x == 0 ? sprite.flipX : input.x < 0;

        if (input.y < 0)
            controller.jumpDown = true;

        if (jump && grounded)
        {
            velocity.y = maxJumpVelocity;  
            jump = false;          
        }

        if (jumpDown && grounded)
        {
            velocity.y = maxJumpVelocity;    
            jumpDown = false;
        }
        
        if (jumpUp)
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
                jumpUp = false;
            }
        }        

        if((attack && grounded) || (attackController.jumpingAttack && attack))
        {
            attackController.PerformAttack();
            attack = false;
        }

        if(guard && grounded)
        {
            attackController.GuardUp();
        }
        else
        {
            attackController.GuardDown();
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Knockback(int force, int dir)
    {
        velocity.x += dir * force;
    }
}