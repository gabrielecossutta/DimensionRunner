using UnityEngine;

// Class to manage the collectables
public class Collectables : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Play the sound effect
        AudioScript.Instance.PlayEffect(AudioScript.Effects.PickUpCoin);

        // Disable the collectable
        this.gameObject.SetActive(false);

        // Call the GetCollecatable method from the Player scipt to invoke the CollecatableTaken event
        other.gameObject.GetComponent<RunForward>().GetCollecatable();
    }
}
