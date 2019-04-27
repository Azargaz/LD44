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
    public GameObject background;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        Vector2 spawnPos = new Vector2(0, 0);
        int roomsLeft = numberOfRooms;
        int lastIndex = 0;
        while(roomsLeft > 0)
        {
            int randomIndex = Random.Range(0, rooms.Length);
            while(randomIndex == lastIndex) randomIndex = Random.Range(0, rooms.Length);
            
            Instantiate(rooms[randomIndex], spawnPos, Quaternion.identity, transform);
        
            spawnPos += new Vector2(8, 0);
            roomsLeft--;
            lastIndex = randomIndex;
        }

        for(int x = -roomsSize - wallsSize; x < numberOfRooms * roomsSize + roomsSize + wallsSize; x++)
        {
            for(int y = -wallsSize; y < wallsSize + roomsSize; y++)
            {
                spawnPos = new Vector2(x, y);
                
                Instantiate(background, spawnPos, Quaternion.identity, transform);

                if(x >= -roomsSize && x < numberOfRooms * roomsSize + roomsSize && y >= 0 && y < roomsSize)
                    continue;

                Instantiate(walls, spawnPos, Quaternion.identity, transform);
            }
        }
    }

}
