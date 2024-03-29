﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    public int money;
    float displayMoney;
    public Text moneyText;

    public static MoneyController instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        displayMoney = Mathf.Lerp(displayMoney, money, 10.0f * Time.deltaTime);
        moneyText.text = Mathf.RoundToInt(displayMoney).ToString();
    }

    public void ConvertKillsToMoney()
    {
        int health = GameManager.instance.health;
        int kills = GameManager.instance.kills;
        GameManager.instance.kills = 0;

        int amount = (health + 1) * (kills + 1) * 100;
        AddAmount(amount);
    }

    public bool RemoveAmount(int amount)
    {
        if(amount > money) return false;
        money -= amount;
        return true;
    }

    public void AddAmount(int amount)
    {
        money += amount;
    }
}
