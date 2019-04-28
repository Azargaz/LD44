using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public Image staminaBar;
    public Transform heartsContainer;
    public GameObject hearthObj;

    List<Image> hearts;

    AttackController playerStats;

    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackController>();
        hearts = new List<Image>();
        
        for(int i = 0; i < playerStats.maxHealth / 2; i++)
        {
            GameObject spawnedHeart = Instantiate(hearthObj, transform.position, Quaternion.identity, heartsContainer);
            hearts.Add(spawnedHeart.transform.Find("Img").GetComponent<Image>());
        }
    }

    void Update()
    {
        float newFillAmount = (float)playerStats.currentStamina / playerStats.maxStamina;
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, newFillAmount, 10f * Time.deltaTime);

        for(int i = 0; i < playerStats.maxHealth; i++)
        {
            hearts[i/2].fillAmount = 1;

            if(i > playerStats.currentHealth)
            {
                hearts[i/2].fillAmount = 0;
            }
            if(i == playerStats.currentHealth && i % 2 == 1)
            {
                hearts[i/2].fillAmount = 0.5f;
            }
        }
    }
}
