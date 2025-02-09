using System.Collections;
using UnityEngine;

class BossRoomTrigger : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossUI;
    [SerializeField] GameObject doorObject;
    [SerializeField] int bossNumber;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShowBossIntro());

        }
    }
    public IEnumerator ShowBossIntro()
    {
        yield return new WaitForSeconds(1);
        if (BossIntroUI.instance)
        {
            BossIntroUI.instance.Show();
            yield return new WaitForSeconds(3);
        }
        boss.GetComponent<BaseBossController>().StartBossBattle();
        bossUI.SetActive(true);
        BossHealthUI bossHealthUI = bossUI.GetComponent<BossHealthUI>();
        BossHealth bossHealth = boss.GetComponent<BossHealth>();
        if (bossHealthUI != null && bossHealth != null)
        {
            bossHealth.Initialize(bossHealthUI);
        }
        if (doorObject)
            doorObject.SetActive(true);

        BackgroundMusicManager.Instance.PlayBossMusic(bossNumber);
        Destroy(gameObject);
    }
}