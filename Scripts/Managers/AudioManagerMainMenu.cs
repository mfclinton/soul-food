using UnityEngine;

public class AudioManagerMainMenu : MonoBehaviour
{
    public static AudioManagerMainMenu Instance { get; private set; }

    private AudioSource audioSource;

    void Awake()
    {
        // Ensure there is only one instance of AudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Add AudioSource component if not already attached
        if (!TryGetComponent(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Function to play a sound
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("No AudioClip specified.");
        }
    }
}
