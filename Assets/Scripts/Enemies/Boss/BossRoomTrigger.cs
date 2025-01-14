using UnityEngine;

class BossRoomTrigger : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossUI;
    [SerializeField] GameObject doorObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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

            Destroy(gameObject);
        }
    }
}