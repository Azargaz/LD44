using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    int steps;
    public int[] thresholds;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;

    public float delay = 0.5f;
    float time;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        time += Time.deltaTime;
    }

    void UpdateSprite()
    {
        for(int i = thresholds.Length-1; i >= 0; i--)
        {
            if(steps >= thresholds[i])
            {
                spriteRenderer.sprite = sprites[i];
                return;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag == "Player" && time > delay)
        {
            steps++;
            time = 0;
            UpdateSprite();
        }
    }

}
