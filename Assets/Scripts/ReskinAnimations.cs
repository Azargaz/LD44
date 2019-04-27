using System;
using UnityEngine;

public class ReskinAnimations : MonoBehaviour
{
    public string spriteSheetName;

    void LateUpdate() 
    {
        Sprite[] subSprites = Resources.LoadAll<Sprite>("Sprites/" + spriteSheetName);

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        string spriteName = renderer.sprite.name;
        Sprite newSprite = Array.Find(subSprites, item => item.name == spriteName);
        
        if(newSprite)
            renderer.sprite = newSprite;
    }
}
