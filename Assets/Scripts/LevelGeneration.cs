using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public int numberOfRooms;

    public GameObject[] rooms;
    public int roomsSize = 8;

    public int wallsSize = 4;
    public GameObject walls;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        Vector2 spawnPos = new Vector2(0, 0);
        int roomsLeft = numberOfRooms;
        while(roomsLeft > 0)
        {
            int randomIndex = Random.Range(0, rooms.Length);
            Instantiate(rooms[randomIndex], spawnPos, Quaternion.identity, transform);
        
            spawnPos += new Vector2(8, 0);
            roomsLeft--;
        }

        for(int x = -wallsSize; x < numberOfRooms * roomsSize + wallsSize; x++)
        {
            for(int y = -wallsSize; y < wallsSize + roomsSize; y++)
            {
                if(x >= 0 && x < numberOfRooms * roomsSize && y >= 0 && y < roomsSize)
                    continue;

                spawnPos = new Vector2(x, y);
                Instantiate(walls, spawnPos, Quaternion.identity, transform);
            }
        }
    }

}
