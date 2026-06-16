using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    // A simple struct so you can easily assign names and clips inside Unity's Inspector
    [System.Serializable]
    public struct SoundSetup
    {
        public string soundName; // e.g. "Footstep", "PlatformStart", "Spawn"
        public AudioClip clip;
    }

    [Header("Sound Config Array")]
    [SerializeField] private SoundSetup[] soundRegistry;

    private CustomSFXHashMap sfxMap;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        sfxMap = new CustomSFXHashMap(20); // Create our custom map with a capacity of 20 buckets

        // Initialize the custom hashmap with the files assigned in the Inspector
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
            // PlayOneShot allows multiple clips to overlay naturally without cutting each other off
            audioSource.PlayOneShot(clipToPlay);
        }
        else
        {
            Debug.LogWarning($"Sound key '{soundName}' could not be found in the custom HashMap!");
        }
    }
}