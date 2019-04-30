using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public LayerMask targets;
    public int damage = 1;
    public int knockbackStrength;
    [HideInInspector]
    public Vector2 hurtboxSize = new Vector2(0.5f, 0.5f);
    public Vector2 defaultHurtboxSize = new Vector2(0.5f, 0.5f);
    public Vector2 defaultHurtboxOffset = new Vector2(0, 0.5f);

    public bool manualHurtboxSize = false;

    BoxCollider2D coll;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    void Update() 
    {
        if(!manualHurtboxSize)
        {
            coll.size = hurtboxSize;
            coll.offset = defaultHurtboxOffset + new Vector2(Mathf.Abs(defaultHurtboxSize.x - hurtboxSize.x) / 2.0f, Mathf.Abs(defaultHurtboxSize.y - hurtboxSize.y) / 2.0f);
        }
    }
    
    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.transform.parent == transform)
            return;

        if(targets == (targets | (1 << other.gameObject.layer)))
        {
            AttackController target = other.GetComponent<AttackController>();
            if(target != null)
            {
                target.TakeDamage(damage, knockbackStrength, (int)Mathf.Sign(other.transform.position.x - transform.parent.position.x), true);
            }
        }   
    }
}
