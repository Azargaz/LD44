using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public Transform heartsContainer;
    public GameObject hearthObj;

    List<Image> hearts;

    AttackController boss;

    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<AttackController>();
        hearts = new List<Image>();
        
        for(int i = 0; i < boss.maxHealth / 2; i++)
        {
            GameObject spawnedHeart = Instantiate(hearthObj, transform.position, Quaternion.identity, heartsContainer);
            hearts.Add(spawnedHeart.transform.Find("Img").GetComponent<Image>());
        }
    }

    void Update()
    {
        if(boss == null) 
        {
            boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<AttackController>();
            return;            
        }

        for(int i = 0; i < boss.maxHealth; i++)
        {
            hearts[i/2].fillAmount = 1;

            if(i > boss.currentHealth)
            {
                hearts[i/2].fillAmount = 0;
            }
            if(i == boss.currentHealth && i % 2 == 1)
            {
                hearts[i/2].fillAmount = 0.5f;
            }
        }
    }
}
