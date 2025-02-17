using UnityEngine;

//This script handles the inputs of the player
public class Inputs : MonoBehaviour
{
    // Reference to the input actions
    private PlayerInputActions inputActions;
    
    // Reference to the player script
    [SerializeField]
    private RunForward player;

    // Minimum distance to detect a swipe
    [SerializeField]
    private float minDistance = 0.2f;

    // Threshold to detect the direction of the swipe
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = 0.9f;

    // Maximum time to detect a swipe
    [SerializeField]
    private float maxTime = 1f;

    // Start position of the swipe
    private Vector2 StartPosition;

    // Start time touch
    private float StartTime;

    // End position of the swipe
    private Vector2 EndPosition;

    // End time touch
    private float EndTime;

    void Awake()
    {
        // Initialize the input actions
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        // Activate the input actions
        inputActions.Enable();

        // Subscribe to the primary contact events
        inputActions.Touch.PrimaryContact.performed += ctx => StartTouchPrimary();

        // unsubscribe to the primary contact events
        inputActions.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary();
    }

    void OnDisable()
    {
        // Deactivate the input actions
        inputActions.Disable();
    }

    private void StartTouchPrimary()
    {
        // Save the start position
        StartPosition = inputActions.Touch.PrimaryPosition.ReadValue<Vector2>();

        // Save the start time
        StartTime = Time.time;
    }

    private void EndTouchPrimary()
    {
        // Save the end position
        EndPosition = inputActions.Touch.PrimaryPosition.ReadValue<Vector2>();

        // Save the end time
        EndTime = Time.time;

        // Detect the direction of the swipe
        DetectDirection();
    }

    private void DetectDirection()
    {
        // Check if the swipe is valid
        if (Vector3.Distance(StartPosition, EndPosition) >= minDistance && (EndTime - StartTime) <= maxTime)
        {
            // Calculate the direction of the swipe
            Vector2 Direction = (EndPosition - StartPosition).normalized;

            // Check the direction of the swipe
            if (Vector2.Dot(Vector2.up, Direction) > directionThreshold)
            {
                OnSwipeUp();
            }
            else if (Vector2.Dot(Vector2.down, Direction) > directionThreshold)
            {
                OnSwipeDown();
            }
            else if (Vector2.Dot(Vector2.left, Direction) > directionThreshold)
            {
                // Play the lateral movement sound effect
                AudioScript.Instance.PlayEffect(AudioScript.Effects.LateralMovement);
                OnSwipeLeft();
            }
            else if (Vector2.Dot(Vector2.right, Direction) > directionThreshold)
            {
                // Play the lateral movement sound effect
                AudioScript.Instance.PlayEffect(AudioScript.Effects.LateralMovement);
                OnSwipeRight();
            }
        }
    }

    private void OnSwipeLeft()
    {
        // Move the player to the left
        player.MoveLeft();
    }

    private void OnSwipeRight()
    {
        // Move the player to the right
        player.MoveRight();
    }

    private void OnSwipeUp()
    {
        // Make the player jump
        player.Jump();
    }

    private void OnSwipeDown()
    {
        // Method to slide ?
    }
}
