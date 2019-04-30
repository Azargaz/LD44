using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStation : MonoBehaviour
{
    public float range = 1.5f;
    public bool used;

    Transform player;
    public FloatingText floatingText;

    Animator anim;

    public Color heartColor;
    public Color goldColor;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("Used", used);

        if(GameManager.instance.deaths > 0) used = true;

        if(Vector2.Distance(player.position, transform.position) < range)
        {
            if(Input.GetKeyDown(KeyCode.E) && !used)
            {
                SaveHealthAmount();
            }
        }
    }

    void SaveHealthAmount()
    {
        used = true;
        GameManager.instance.health++;

        FloatingText fltTxt = Instantiate(floatingText.gameObject, transform.position, Quaternion.identity).GetComponent<FloatingText>();
        fltTxt.textToDisplay = "+HEART MULTIPLIER";
        fltTxt.color = heartColor;

        fltTxt = Instantiate(floatingText.gameObject, transform.position, Quaternion.identity).GetComponent<FloatingText>();
        fltTxt.textToDisplay = "\n\n+GOLD";
        fltTxt.color = goldColor;

        player.GetComponent<AttackController>().TakeDamage(1, 0, 1, false);
        MoneyController.instance.ConvertKillsToMoney();
    }
}
