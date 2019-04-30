using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBossUI : MonoBehaviour
{
    public LayerMask player;

    public GameObject bossUI;

    bool firstDeath = true;

    void Update()
    {
        if(firstDeath && GameManager.instance.deaths > 0)
        {
            bossUI.SetActive(false);
            firstDeath = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(player == (player | (1 << other.gameObject.layer)))
        {
            bossUI.SetActive(true);
        }
    }
}
