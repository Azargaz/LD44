using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    GameObject spawnedEnemy;
    int randomizedIndex;

    void Start()
    {
        int randomIndex = Random.Range(0, enemies.Length);
        randomizedIndex = randomIndex;
        spawnedEnemy = Instantiate(enemies[randomIndex], transform.position, Quaternion.identity, transform);

        EnemySpawnerController.enemySpawnerList.Add(this);
    }

    public void Respawn()
    {
        if(spawnedEnemy) Destroy(spawnedEnemy);
        spawnedEnemy = Instantiate(enemies[randomizedIndex], transform.position, Quaternion.identity, transform);
    }
}
