using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] tilePrefabs; 
    
    private List<GameObject> activeTiles = new List<GameObject>();
    private int startTiles = 5, spawnPos = 30 ,tileLength = 30;

    void Start()
    {
        for(int i = 0; i < startTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
        if(player.position.z - 40 > spawnPos - (startTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
            
    }

    void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], player.transform.forward * spawnPos, tilePrefabs[tileIndex].transform.rotation);
        activeTiles.Add(nextTile);
        spawnPos += tileLength;
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
