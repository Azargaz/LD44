using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public Text text;
    public Color color;
    public string textToDisplay;
    public float duration;

    void Update()
    {
        text.text = textToDisplay;
        text.color = color;
        duration -= Time.deltaTime;

        if(duration <= 0)
            Destroy(gameObject);
    }
}
