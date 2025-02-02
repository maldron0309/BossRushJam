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
    public AudioClip openningTrack;
    public AudioClip endingTrack1;
    public AudioSource adSrc;
    private int currentIdx = -1;
    public GameSettings gameSettings;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //audioSource = gameObject.AddComponent<AudioSource>();
            //audioSource.loop = true;
            //audioSource.volume = 0.5f;
            //audioSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
            //Instance.StopBGM();
        }
    }

    void Start()
    {
        adSrc.volume = 0.25f;
    }

    void Update()
    {

    }
    public void UpdateMusicVolume(float volume)
    {
        //audioSource.volume = volume;
    }
    public float GetVolume()
    {
        return adSrc.volume;
    }
    public void PlayMenuTrack()
    {
        adSrc.clip = menuTrack;
        adSrc.Play();
    }
    public void PlayEndingTrack1()
    {
        adSrc.clip = endingTrack1;
        adSrc.Play();
    }
    public void PlayOpenningTrack()
    {
        adSrc.clip = openningTrack;
        adSrc.Play();
    }
    public void PlayBossMusic(int bossIdx)
    {
        if(bossIdx == 0)
            adSrc.clip = normalTrack;
        else if(bossIdx == 1)
            adSrc.clip = boss1Trak;
        else if (bossIdx == 2)
            adSrc.clip = boss2Trak;
        else if (bossIdx == 3)
            adSrc.clip = boss3Trak;
        else if (bossIdx == 4)
            adSrc.clip = boss4Trak;
        else if (bossIdx == 5)
            adSrc.clip = boss5Trak;
        else if (bossIdx == 6)
            adSrc.clip = boss6Trak;

        if(currentIdx != bossIdx)
            adSrc.Play();
        currentIdx = bossIdx;
    }
    public void StopBGM()
    {
        adSrc.Stop();
    }
}