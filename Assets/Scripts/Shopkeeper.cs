using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string text;
        public int chance;
    }

    Transform player;
    SpriteRenderer sprite;
    Animator anim;
    
    public Dialogue[] textsBeforeDeath;
    public Dialogue[] textsAfterDeath;
    public FloatingText floatingTxt;

    public float minDistanceForInteraction;
    public float delayBetweenTexts;
    float delayText;

    public bool shopOpen = false;
    public GameObject shopUI;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        delayText = delayBetweenTexts;
    }

    void Update()
    {
        sprite.flipX = player.position.x - transform.position.x < 0;

        delayText -= Time.deltaTime;

        if(Vector2.Distance(player.position, transform.position) < minDistanceForInteraction)
        {
            if(delayText <= 0)
            {
                DisplayText();
                delayText = delayBetweenTexts;
            }
            
            if(shopOpen)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    shopUI.SetActive(true);
                }
            }
        }
        else
        {
            if(shopUI.activeInHierarchy)
                shopUI.SetActive(false);
        }

        if(GameManager.instance.deaths > 0)
        {
            shopOpen = true;
            anim.SetBool("Death", true);
        }

        if(Input.GetKeyDown(KeyCode.Escape)) shopUI.SetActive(false);
    }

    void DisplayText()
    {
        FloatingText fltTxt = Instantiate(floatingTxt, transform.position, Quaternion.identity).GetComponent<FloatingText>();

        Dialogue[] texts = shopOpen ? textsAfterDeath : textsBeforeDeath;
        int sumOfChances = 0;
        for(int i = 0; i < texts.Length; i++)
        {
            sumOfChances += texts[i].chance;
        }    

        int randomValue = Random.Range(0, sumOfChances+1);
        int currentChance = 0;
        for(int i = 0; i < texts.Length; i++)
        {
            currentChance += texts[i].chance;
            if(randomValue <= currentChance)
            {
                fltTxt.textToDisplay = texts[i].text;
                break;
            }
        }  
    }
}
