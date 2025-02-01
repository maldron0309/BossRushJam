using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfTheGame : MonoBehaviour
{
    public bool allBossesBeaten = false;
    public float shakeDuration;
    public float shakeIntencity;
    public float shakeRate;
    private float shakeCounter = 0;
    public AudioClip soundEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (allBossesBeaten)
        {
            if (shakeCounter > 0)
                shakeCounter -= Time.deltaTime;
            else
            {
                shakeCounter = shakeRate + Random.Range(0, .5f);
                SoundEffectsManager.Instance.PlaySound(soundEffect);
                RoomCamera.instance.Shake(shakeDuration, shakeRate);
                foreach (var item in FindObjectsByType<SetMusicTrigger>(FindObjectsSortMode.None))
                {
                    Destroy(item, 0.5f);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (allBossesBeaten)
        {
            GameProgressManager gpm = FindAnyObjectByType<GameProgressManager>();
            for (int i = 0; i < gpm.bossDefeated.Length; i++)
            {
                gpm.bossDefeated[i] = false;

            }
            SceneManager.LoadScene("EndingCutscene");
        }
    }
}
