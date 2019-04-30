using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipBoard : MonoBehaviour
{
    public float range = 1.5f;

    public GameObject floatingText;

    public GameObject tips;

    Transform player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(Vector2.Distance(player.position, transform.position) < range)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                DisplayTips();
            }

            floatingText.SetActive(true);
        }
        else
        {
            floatingText.SetActive(false);
            tips.SetActive(false);
        }
    }

    void DisplayTips()
    {
        tips.SetActive(!tips.activeInHierarchy);
    }
}
