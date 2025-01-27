using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance;
    public AudioClip boss1Trak;
    public AudioClip boss2Trak;
    public AudioClip boss3Trak;
    public AudioClip boss4Trak;
    public AudioClip boss5Trak;
    public AudioClip boss6Trak;
    public AudioClip menuTrack;
    public AudioClip normalTrack;
    public AudioClip endingTrack1;
    private AudioSource audioSource;
    private int currentIdx = -1;
    public GameSettings gameSettings;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.volume = 0.5f;
            audioSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
            //Instance.StopBGM();
        }
    }

    void Start()
    {
        audioSource.volume = 0.25f;
    }

    void Update()
    {

    }
    public void UpdateMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }
    public float GetVolume()
    {
        return audioSource.volume;
    }
    public void PlayMenuTrack()
    {
        audioSource.clip = menuTrack;
        audioSource.Play();
    }
    public void PlayEndingTrack1()
    {
        audioSource.clip = endingTrack1;
        audioSource.Play();
    }
    public void PlayBossMusic(int bossIdx)
    {
        if(bossIdx == 0)
            audioSource.clip = normalTrack;
        else if(bossIdx == 1)
            audioSource.clip = boss1Trak;
        else if (bossIdx == 2)
            audioSource.clip = boss2Trak;
        else if (bossIdx == 3)
            audioSource.clip = boss3Trak;
        else if (bossIdx == 4)
            audioSource.clip = boss4Trak;
        else if (bossIdx == 5)
            audioSource.clip = boss5Trak;
        else if (bossIdx == 6)
            audioSource.clip = boss6Trak;

        if(currentIdx != bossIdx)
            audioSource.Play();
        currentIdx = bossIdx;
    }
    public void StopBGM()
    {
        audioSource.Stop();
    }
}