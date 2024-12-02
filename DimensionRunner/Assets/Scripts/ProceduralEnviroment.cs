using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralEnvironment : MonoBehaviour
{
    public GameObject[] tiles; // Array to hold tiles
    private GameObject TileToSpawn; // Tile to spawn
    private Vector3 spawnPosition = Vector3.zero; // Origin
    private float tileLength = 10f; // Length of each tile
    private int tilesToSpawn = 10; // Number of tiles to spawn

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tilesToSpawn; i++)
        {
            SpawnTile();
        }
    }

    void SpawnTile()
    {
        // Random tile to spawn
        TileToSpawn = tiles[Random.Range(0, tiles.Length)];

        // Instantiate a new tile
        Instantiate(TileToSpawn, spawnPosition, TileToSpawn.transform.rotation);
        
        // Move the spawn position forward for the next tile
        spawnPosition += new Vector3(0, 0, tileLength);
    }
}