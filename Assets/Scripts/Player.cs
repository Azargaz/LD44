using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
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
    
    Controller2D controller;
    Animator anim;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        anim = GetComponent<Animator>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
            controller.jumpDown = false;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        anim.SetFloat("InputX", Mathf.Abs(input.x));

        if (input.y < 0)
            controller.jumpDown = true;

        if (Input.GetButtonDown("Jump") && controller.collisions.below)
        {
            if (controller.collisions.below)
            {
                velocity.y = maxJumpVelocity;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }        

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}