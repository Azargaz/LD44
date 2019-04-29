using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterDeathEffects : MonoBehaviour
{
    public SpriteRenderer body;
    public Animator anim;

    void Update()
    {
        if(GameManager.instance.deaths > 0)
        {
            body.enabled = true;
            anim.SetTrigger("Coin");
        }
    }
}
