using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicTrigger : MonoBehaviour
{
    public int musicIdx;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BackgroundMusicManager.Instance.PlayBossMusic(musicIdx);
    }
}
