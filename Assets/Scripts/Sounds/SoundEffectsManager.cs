using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager Instance;

    public AudioClip buttonPressSound;
    public AudioClip tickSound;
    public AudioClip rainSound;
    public AudioClip itemPickupSound;
    public GameSettings gameSettings;

    private AudioSource audioSource;
    private List<AudioClip> soundsBuffer = new List<AudioClip>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void FixedUpdate()
    {
        soundsBuffer.Clear();
    }
    private void Start()
    {
        UpdateSoundEffectsVolume(0.65f);
    }
    public void PlaySound(AudioClip clip)
    {
        // play any sound. just pass audio clip
        if (!soundsBuffer.Contains(clip))
        {
            soundsBuffer.Add(clip);
            audioSource.PlayOneShot(clip);
        }
    }
    public void PlayButtonPressSound()
    {
        audioSource.PlayOneShot(buttonPressSound);
    }
    public void PlayTickPressSound()
    {
        audioSource.PlayOneShot(buttonPressSound);
    }
    public void PlayPickItemSound()
    {
        audioSource.PlayOneShot(itemPickupSound);
    }
    public void UpdateSoundEffectsVolume(float volume)
    {
        //audioSource.volume = volume;
    }
    public void StopSound()
    {
        audioSource.Stop();
    }
}