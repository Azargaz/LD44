using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [HideInInspector]
    public LayerMask targets;
    [HideInInspector]
    public int damage = 1;
    [HideInInspector]
    public int knockbackStrength;
    [HideInInspector]
    public float hurtboxSize = 0.5f;
    public float defaultHurtboxSize = 0.5f;
    public Vector2 defaultHurtboxOffset = new Vector2(0, 0.5f);

    BoxCollider2D coll;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    void Update() 
    {
        coll.size = new Vector2(hurtboxSize, hurtboxSize);
        coll.offset = defaultHurtboxOffset + new Vector2(Mathf.Abs(defaultHurtboxSize - hurtboxSize) / 2.0f, Mathf.Abs(defaultHurtboxSize - hurtboxSize) / 2.0f);
    }
    
    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.transform.root == transform.root)
            return;

        if(targets == (targets | (1 << other.gameObject.layer)))
        {
            AttackController target = other.GetComponent<AttackController>();
            if(target != null)
            {
                target.TakeDamage(damage, knockbackStrength, (int)Mathf.Sign(other.transform.position.x - transform.root.position.x));
            }
        }   
    }
}
