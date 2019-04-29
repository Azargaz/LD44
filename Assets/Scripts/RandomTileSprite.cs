using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileSprite : MonoBehaviour
{
    public Sprite[] tileSprites;

    public Sprite[] decorationSprites;

    public LayerMask blockDecorationsMask;
    public float chanceToSpawnDecoration = 0.1f;

    public SpriteRenderer tile;
    public SpriteRenderer decoration;

    void Start()
    {
        tile.sprite = tileSprites[Random.Range(0, tileSprites.Length)];

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.6f), Vector2.up, 0.5f, blockDecorationsMask);

        if(!hit && Random.value < chanceToSpawnDecoration)
        {
            decoration.sprite = decorationSprites[Random.Range(0, decorationSprites.Length)];
        }
    }

}
