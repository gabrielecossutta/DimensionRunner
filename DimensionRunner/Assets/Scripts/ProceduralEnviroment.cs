using UnityEngine;

public class ProceduralEnvironment : MonoBehaviour
{
    // Array to hold tiles
    [SerializeField]
    private GameObject[] tilesPool;

    // Array to know the tiles that are already spawned
    private bool[] idTileAlreadySpawned;

    // Preffixed tiles
    [SerializeField]
    private GameObject startTile;
    [SerializeField]
    private GameObject preStartTile;
    [SerializeField]
    private GameObject preEndTile;
    [SerializeField]
    private GameObject endTile;

    // Postion to spawn the tiles
    private Vector3 spawnPosition = Vector3.zero;

    // Length of each tile
    private float tileLength = 10f;

    // Number of tiles to spawn
    private int numberTilesToSpawn = 50;

    // Random tile
    private int randomTile;

    void Start()
    {
        fillArray();
        // Loop through the tiles
        for (int i = 0; i < numberTilesToSpawn; i++)
        {
            // Create the scenario
            MoveTiles(i);
        }
    }

    private void OnEnable()
    {
        // Subscribe to the event
        RunForward.PortalPassed += ResetTiles;
        TileScript.OrganizeTiles += DeactivateTile;
    }

    private void OnDisable()
    {
        // Unsubscribe to the event
        RunForward.PortalPassed -= ResetTiles;
        TileScript.OrganizeTiles -= DeactivateTile;
    }

    public void MoveTiles(int i)
    {
        // Preffixed tiles
        if (i == 0)
        {
            startTile.transform.position = spawnPosition;
        }
        else if (i == 1)
        {
            preStartTile.transform.position = spawnPosition;
        }
        else if (i == 48)
        {
            preEndTile.transform.position = spawnPosition;
        }
        else if (i == 49)
        {
            endTile.transform.position = spawnPosition;
        }
        else
        {
            // Check if the tile is already spawned
            do
            {
                // Randomly select a tile
                randomTile = Random.Range(0, tilesPool.Length);

            } while (!idTileAlreadySpawned[randomTile]);

            // Set the bool to true, so the tile is already moved
            idTileAlreadySpawned[randomTile] = false;

            // Set the tile position
            tilesPool[randomTile].transform.position = spawnPosition;
        }

        // Move the spawn position forward for the next tile
        spawnPosition += new Vector3(0, 0, tileLength);
    }



    private void organizeTiles(int i)
    {
        // calculate the x and z position of the tile
        int x = i % 10;
        int z = i / 10;

        // Move the tile to the new Organized position
        tilesPool[i].transform.position = new Vector3(x * -12, 0, z * -12);
    }

    public void ResetTiles()
    {
        //Play the portal Effect
        AudioScript.Instance.PlayEffect(AudioScript.Effects.Portals);

        // Reset the spawn position
        spawnPosition = Vector3.zero;

        // Reset the array
        fillArray();

        // Activate all the tiles
        ActivateAllTiles();

        // Loop through the tiles
        for (int i = 0; i < numberTilesToSpawn; i++)
        {
            MoveTiles(i);
        }
    }

    private void fillArray()
    {
        // Initialize the array to the TilesPool length
        idTileAlreadySpawned = new bool[tilesPool.Length];
        // Loop through the tiles
        for (int i = 0; i < idTileAlreadySpawned.Length; i++)
        {
            // Set bool to true, so the tile is already spawned
            idTileAlreadySpawned[i] = true;
        }
    }
    private void ActivateAllTiles()
    {
        // Loop through the tiles
        for (int i = 0; i < idTileAlreadySpawned.Length; i++)
        {
            // Activate the tile
            tilesPool[i].gameObject.SetActive(true);
        }
    }

    // Deactivate the tile
    public void DeactivateTile(GameObject tile)
    {
        // Loop through the tiles
        for (int i = 0; i < idTileAlreadySpawned.Length; i++)
        {
            // If the tile is the one to deactivate
            if (tile == tilesPool[i])
            {
                // Organize the tiles
                organizeTiles(i);
            }
        }
    }

}