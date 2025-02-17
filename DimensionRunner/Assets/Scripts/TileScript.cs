using System;
using UnityEngine;

// Script to manage the tiles
public class TileScript : MonoBehaviour
{
    // Event to organize the tiles
    public static event Action<GameObject> OrganizeTiles;
    private void OnTriggerExit(Collider other)
    {
        // If the tile is out of the camera view
        if (other.tag == "MainCamera")
        {
            // Deactivate the tile
            this.gameObject.SetActive(false);

            // Invoke the event to organize the tiles
            OrganizeTiles?.Invoke(this.gameObject);
        }
    }
}
