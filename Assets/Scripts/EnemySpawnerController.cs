using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public static List<EnemySpawner> enemySpawnerList;
    
    void Awake()
    {
        enemySpawnerList = new List<EnemySpawner>();
    }

    public void ResetEnemies()
    {
        foreach (EnemySpawner spawner in enemySpawnerList)
        {
            spawner.Respawn();
        }
    }
}
