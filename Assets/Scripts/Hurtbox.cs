using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [HideInInspector]
    public LayerMask targets;
    [HideInInspector]
    public int damage = 1;
    
    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.transform.root == transform.root)
            return;

        if(targets == (targets | (1 << other.gameObject.layer)))
        {
            AttackController target = other.GetComponent<AttackController>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
        }   
    }
}
