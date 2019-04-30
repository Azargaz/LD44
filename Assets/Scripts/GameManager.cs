using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator gameOverAnim;
    public Animator transitionAnim;
    public Vector2 respawnPoint;

    public static GameManager instance;
    EnemySpawnerController enemySpawners;

    public int deaths = 0;
    public int kills = 0;
    public int health = 0;

    void Awake()
    {
        instance = this;
        enemySpawners = GetComponent<EnemySpawnerController>();

        if(Options.instance.skipIntro)
        {
            transitionAnim.SetTrigger("SkipIntro");
        }
    }

    void FirstDeath()
    {
        deaths++;
        gameOverAnim.SetTrigger("FirstDeath");
    }

    void Death()
    {
        deaths++;
        gameOverAnim.SetTrigger("SecondDeath");
    }

    public void Respawn()
    {
        if (deaths <= 1)
        {
            gameOverAnim.SetTrigger("Respawn");
            enemySpawners.ResetEnemies();
        }
    }

    public void GameOver()
    {
        if (deaths <= 0)
            FirstDeath();
        else
            Death();
    }

    public void Transition()
    {
        transitionAnim.SetTrigger("Transition");
    }
}
