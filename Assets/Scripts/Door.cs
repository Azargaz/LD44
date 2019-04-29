using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    public Door otherDoor;
    public LayerMask player;
    public int dir = 1;
    public float delay = 0.5f;

    Vector2 teleportDestination;
    Transform target;
    bool busy;

    SpriteRenderer sprite;
    public Sprite openDoor;
    public Sprite blockedDoor;

    public bool blockOtherDoor;

    public bool saveHealthAmount = false;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        Open();
        if(otherDoor != null)
            otherDoor.otherDoor = this;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(player == (player | (1 << other.gameObject.layer)) && !busy)
        {
            busy = true;
            target = other.transform;
            teleportDestination = otherDoor.transform.position + new Vector3(dir, 0);
            GameManager.instance.Transition();
            Invoke("Teleport", delay);
        }
    }

    void Teleport()
    {
        if(blockOtherDoor)
        {
            otherDoor.busy = true;
            otherDoor.Block();
        }
        target.position = teleportDestination;
        busy = false;

        if(saveHealthAmount) GameManager.instance.health = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackController>().currentHealth;
    }

    void Open()
    {
        sprite.sprite = openDoor;
    }

    void Block()
    {
        sprite.sprite = blockedDoor;
    }
}
