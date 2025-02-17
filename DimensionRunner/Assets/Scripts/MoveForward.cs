using UnityEngine;
using TMPro;
public class MoveForward : MonoBehaviour
{
    // Reference of the Portal Manager
    private GameObject PortalManager;

    // Reference of the left portal
    [SerializeField]
    private GameObject PortalL;

    // Reference of the right portal
    [SerializeField]
    private GameObject PortalR;

    // Reference of the player
    [SerializeField]
    private GameObject Player;

    // Reference of the camera
    [SerializeField]
    private GameObject Camera;

    // Distance between the player and the portal
    private float Distance = 0f;

    // Max scale of the portals
    private Vector3 MaxScale = new Vector3(0.2f, 1.5f, 0.3f);

    // Bool to check if the player is near the portals
    private bool IsPlayerInside = false;

    void Start()
    {
        // Set the reference of the portal manager
        PortalManager = this.gameObject;

        // Set the Scale of the portals
        PortalL.transform.localScale = MaxScale * (Distance * 5);
        PortalR.transform.localScale = MaxScale * (Distance * 5);
    }

    void Update()
    {
        // Refresce the position of the portal manager
        PortalManager.transform.position = new Vector3(0.58f, 0.94f, Player.transform.position.z);

        // Check if the player is inside the portal
        if (!IsPlayerInside)
        {
            // Check if the player is close to the left portal
            if (Vector3.Distance(PortalL.transform.position, Player.transform.position) < 1.65f)
            {
                // Calculate the distance between the player and the portal
                Distance = 1.70f - Mathf.Abs(Vector3.Distance(PortalL.transform.position, Player.transform.position));

                // Check if the distance is greater than 0.3
                if (Distance > 0.30f)
                {
                    // Set the distance to 0.3 to avoid the portal to be too big
                    Distance = 0.30f;
                }
            }

            // Check if the player is close to the rigth portal
            if (Vector3.Distance(PortalR.transform.position, Player.transform.position) < 1.65f)
            {
                // Calculate the distance between the player and the portal
                Distance = 1.70f - Mathf.Abs(Vector3.Distance(PortalR.transform.position, Player.transform.position));

                // Check if the distance is greater than 0.3
                if (Distance > 0.30f)
                {
                    // Set the distance to 0.3 to avoid the portal to be too big
                    Distance = 0.30f;
                }
            }

            // Set the scale of the portals
            PortalL.transform.localScale = MaxScale * (Distance * 5);
            PortalR.transform.localScale = MaxScale * (Distance * 5);

            // Reset the distance
            Distance = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IsPlayerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsPlayerInside = false;
    }
}