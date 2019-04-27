using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    [System.Serializable]
    public class SpawnableTiles
    {
        public string name;
        [HideInInspector]
        public List<Transform> spawnPoints;
        public GameObject[] tiles;
        public Transform container;
        public Transform spawns;

        public void Initialize()
        {
            spawnPoints.Clear();
            foreach (Transform child in spawns)
            {
                spawnPoints.Add(child);
            }
        }

        public void SpawnTiles()
        {
            foreach (Transform spawn in spawnPoints)
            {
                int randomIndex = Random.Range(0, tiles.Length);
                Instantiate(tiles[randomIndex], spawn.position, Quaternion.identity, container);
            }
        }
    }

    public SpawnableTiles[] spawnableTiles;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        foreach (SpawnableTiles tiles in spawnableTiles)
        {
            tiles.Initialize();
            tiles.SpawnTiles();
        }
    }
}
