using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    Animator anim;
    AttackController boss;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(boss == null)
        {
            boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<AttackController>();
            return;
        }

        if(boss.currentHealth <= 0)
        {
            anim.SetTrigger("Win");
        }
    }
}
