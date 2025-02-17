using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// MenuScript class
public class MenuScript : Singleton<MenuScript>
{


    // Reference of the red heart
    [SerializeField]
    private Image redHeart;

    // Reference of the shield
    [SerializeField]
    private Image shield;

    // Reference of the blue heart
    [SerializeField]
    private Image blueHeart;

    // Multiplier of the shield recharge
    private float multiplier = 0.05f;

    // Score multiplier
    private float scoreMultiplier = 5;

    // Value of the slider
    private float volume;

    // Reference of the score text
    [SerializeField]
    private TMP_Text scoreTMP;

    // Reference of the shield text
    [SerializeField]
    private TMP_Text shieldTMP;

    // Score of the player
    private float score = 0;

    //Read-only property of the volume float
    public float Volume { get { return volume; } }

    new private void Awake()
    {
        // Call the Awake method of the base class
        base.Awake();

        // Set the time scale to 0
        PauseGame();
    }

    // Quit the Game
    public void ExitGame()
    {
        Application.Quit();
    }

    // Used to stop the timer in the setting menu is open 
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    // Used to resume the timer when the setting menu is closed
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    IEnumerator RechardeShield()
    {
        // Check if the shield isn't full
        while (shield.fillAmount < 1f)
        {
            // Increase the value of the shield fill amount
            shield.fillAmount += Time.deltaTime * multiplier;

            // Update the text of the shield
            shieldTMP.text = (shield.fillAmount * 100).ToString("F0");

            // Wait next frame
            yield return null;
        }

        // Restore the shield
        RestoreShield();
    }

    private void DestroyShield()
    {
        // Check if the shield isn't full
        if (shield.fillAmount < 1)
        {
            // Play the death sound effect
            AudioScript.Instance.PlayEffect(AudioScript.Effects.Death);

            // Game Over
            PauseGame();

            return;
        }

        // Play the hit sound effect
        AudioScript.Instance.PlayEffect(AudioScript.Effects.Hit);

        // Set to 0 the shield fill amount
        shield.fillAmount = 0f;

        // Hide the blue heart
        blueHeart.enabled = false;

        // Start the coroutine
        StartCoroutine(RechardeShield());
    }

    private void Update()
    {
        // Increase the score by the time passed multiplied by the score multiplier
        score += Time.deltaTime * scoreMultiplier;

        // Update the text of the score, wihout decimal
        scoreTMP.text = ":SCORE:\n" + score.ToString("F0");
    }

    private void RestoreShield()
    {
        // Show the blue heart
        blueHeart.enabled = true;
    }

    private void OnEnable()
    {
        // Subscribe to the event
        RunForward.DamageTaken += DestroyShield;
        RunForward.CollectableTaken += CollectableTaken;
        RunForward.CollectableTaken += ChargeShield;
        RunForward.PortalPassed += PortalPassed;
    }

    private void OnDisable()
    {
        // Unsubscribe to the event
        RunForward.DamageTaken -= DestroyShield;
        RunForward.CollectableTaken -= CollectableTaken;
        RunForward.CollectableTaken -= ChargeShield;
        RunForward.PortalPassed -= PortalPassed;
    }

    private void CollectableTaken()
    {
        // Increase the score by 15
        score += 15;
    }

    private void PortalPassed()
    {
        // Increase the score by 100
        score += 100;

        // Refull the shield
        shield.fillAmount = 1f;
    }

    private void ChargeShield()
    {
        // Increase the value of the shield fill amount
        shield.fillAmount += 0.05f;
    }
}
