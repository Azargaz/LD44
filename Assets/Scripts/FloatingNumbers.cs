using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour
{
    public Text text;
    public Color color;
    public int number;
    public float duration;

    void Update()
    {
        text.text = number.ToString();
        text.color = color;
        duration -= Time.deltaTime;

        if(duration <= 0)
            Destroy(gameObject);
    }
}
