using UnityEngine;

// Class to manage the obstacles
public class Obstacol : MonoBehaviour
{
    // Variables to determine the movement of the player
    [SerializeField]
    private bool moveRight;
    [SerializeField]
    private bool moveLeft;
    [SerializeField]
    private bool jump;

    private void OnTriggerEnter(Collider other)
    {
        // Call the GetDamage method from the Player scipt to invoke the DamageTaken event
        other.gameObject.GetComponent<RunForward>().GetDamage();

        // Check if the player should move right, left or jump
        if (moveRight)
        {
            // Call the MoveRight method from the Player script
            other.gameObject.GetComponent<RunForward>().MoveRight();
            return;
        }
        else if (moveLeft)
        {
            // Call the MoveLeft method from the Player script
            other.gameObject.GetComponent<RunForward>().MoveLeft();
            return;
        }
        else if (jump)
        {
            // Check all the BoxColliders of the obstacle
            foreach (BoxCollider collider in this.gameObject.GetComponents<BoxCollider>())
            {
                // Check if the collider is not a trigger
                if (!collider.isTrigger) 
                {
                    // Disable the collider
                    collider.enabled = false;
                }
            }

            // Call the Jump method from the Player script
            other.gameObject.GetComponent<RunForward>().Jump();
            return;
        }
        else
        {
            // Randomly call the MoveRight or MoveLeft
            if (Random.value < 0.5f)
            {
                other.gameObject.GetComponent<RunForward>().MoveLeft();
            }
            else
            {
                other.gameObject.GetComponent<RunForward>().MoveRight();
            }
        }
    }
}
