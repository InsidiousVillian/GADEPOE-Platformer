using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    // A simple struct so I can easily assign names and clips inside Unity's Inspector
    [System.Serializable]
    public struct SoundSetup
    {
        public string soundName; 
        public AudioClip clip;
    }

    [Header("Sound Config Array")]
    [SerializeField] private SoundSetup[] soundRegistry;

    [Header("Dedicated Music Channel")]
    [Tooltip("Drag your second, dedicated music AudioSource component here!")]
    [SerializeField] private AudioSource musicSource; 

    private CustomSFXHashMap sfxMap;
    private AudioSource sfxSource;

    private void Awake()
    {
        sfxSource = GetComponent<AudioSource>();
        sfxMap = new CustomSFXHashMap(20); // Create our custom map with a capacity of 20 buckets

        // Initialise the custom hashmap with the files assigned in the Inspector
        InitializeMap();
    }

    private void InitializeMap()
    {
        if (soundRegistry == null) return;

        foreach (SoundSetup sound in soundRegistry)
        {
            if (!string.IsNullOrEmpty(sound.soundName) && sound.clip != null)
            {
                sfxMap.Put(sound.soundName, sound.clip);
                Debug.Log($"Custom HashMap registered sound: {sound.soundName}");
            }
        }
    }

    // The core method requested by the deliverable
    public void PlaySFX(string soundName)
    {
        // Query our custom lookup method
        AudioClip clipToPlay = sfxMap.Get(soundName);

        if (clipToPlay != null)
        {
            
            sfxSource.PlayOneShot(clipToPlay);
        }
        else
        {
            Debug.LogWarning($"Sound key '{soundName}' could not be found in the custom HashMap!");
        }
    }

    
    public void PlayBackgroundMusic(string musicKey)
    {
        // Query your custom lookup map class wrapper object directly
        AudioClip musicClip = sfxMap.Get(musicKey);

        if (musicClip != null && musicSource != null)
        {

            if (musicSource.clip == musicClip && musicSource.isPlaying) return;

            musicSource.clip = musicClip;
            musicSource.loop = true;       
            musicSource.playOnAwake = false;
            
            musicSource.Play();
            Debug.Log($"SFXManager: Custom HashMap streaming background music track: {musicKey}");
        }
        else
        {
            if (musicSource == null)
            {
                Debug.LogError("SFXManager: The musicSource AudioSource field is unassigned in the inspector!");
            }
            else
            {
                Debug.LogWarning($"Music key '{musicKey}' could not be found in the custom HashMap!");
            }
        }
    }
}