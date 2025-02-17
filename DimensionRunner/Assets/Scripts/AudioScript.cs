using UnityEngine;
using UnityEngine.UI;
public class AudioScript : Singleton<AudioScript>
{
    // Enum for SoundEffect List
    public enum Effects 
    {
        Death,
        Hit,
        Portals,
        PickUpCoin,
        Jump,
        LateralMovement,
        MenuOpen,
        MenuClose,
        GameStart
    }

    // Source Background Music
    [SerializeField]
    private AudioSource musicSource;

    // Source Sound Effects 
    [SerializeField]
    private AudioSource effectsSource;

    // Sound effects clip list
    [SerializeField]
    private AudioClip[] soundEffects;

    // Volume of all the source
    private float volume;

    // Background Music clip
    [SerializeField]
    private AudioClip backgroundMusic;

    // Reference of the slider
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Slider volumeSlider2;

    // Start is called before the first frame update
    void Start()
    {
        volume = volumeSlider.value;

        // Add a listener to the toggle and slider, when the value change the respective methods are called
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
        volumeSlider2.onValueChanged.AddListener(ChangeVolume);
    }

    // Play one of the Sound Effect based on what is passed
    // I used enum to be able to know what effect am calling instead of a simple int
    public void PlayEffect(Effects Effect)
    {
        // Check if the Source and the effect are not null
        if (effectsSource != null && soundEffects[(int)Effect] != null)
        {
            // Play once the chosen sound effect
            effectsSource.PlayOneShot(soundEffects[(int)Effect]);
        }
    }

    // Start the BackGround Music
    public void StartMusic()
    {
        // Check if the Source and the effect are not null
        if (musicSource != null && backgroundMusic != null)
        {
            // Set BackGround Music
            musicSource.clip = backgroundMusic;

            // Song is looped
            musicSource.loop = true;

            // Play the BackGround Music
            musicSource.Play();
        }
    }

    // Stop the BackGroundMusic
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Called when the Player Change the volume
    private void ChangeVolume(float value)
    {
        // Set BackGround Music volume
        musicSource.volume = value;

        // Set Sound Effect volume
        effectsSource.volume = value;

        // Set the value of the second slider
        volumeSlider2.value = value;
    }
}
