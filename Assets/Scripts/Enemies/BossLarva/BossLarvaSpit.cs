using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLarvaSpit : MonoBehaviour
{
    public bool isStarted = false;
    public GameObject bulletProjectile;
    public float speed = 10;
    public int damage = 5;
    public float fireRate;
    public float spreadAngle = 45f;
    private float fireCooldown;

    private Rigidbody2D rb;
    private BossLarvaController boss;
    private PlayerController player;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<PlayerController>();
        boss = GetComponent<BossLarvaController>();
        fireCooldown = fireRate;
    }

    void Update()
    {
        if (!isStarted)
            return;

        if(fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
        else
        {
            FireAtPlayer();
            fireCooldown = fireRate;

            isStarted = false;
            boss.isPerformingAction = false;
        }
    }
    public void BeginAttack()
    {
        isStarted = true;
        boss.isPerformingAction = true;
    }
    public void FireAtPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        float angle =Mathf.Min(135,Mathf.Max(45, Vector2.Angle(Vector2.right, direction)));
        Debug.Log(angle);

        Rigidbody2D bullet;
        bullet = Instantiate(bulletProjectile, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody2D>();
        direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector2.right;
        bullet.velocity = direction * (speed + Random.Range(0.0f, 5.0f));
        bullet.GetComponent<BossBullet>().damage = damage;

        bullet = Instantiate(bulletProjectile, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody2D>();
        direction = Quaternion.AngleAxis(angle - spreadAngle, Vector3.forward) * Vector2.right;
        bullet.velocity = direction * (speed + Random.Range(0.0f, 5.0f));
        bullet.GetComponent<BossBullet>().damage = damage;

        bullet = Instantiate(bulletProjectile, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody2D>();
        direction = Quaternion.AngleAxis(angle + spreadAngle, Vector3.forward) * Vector2.right;
        bullet.velocity = direction * (speed + Random.Range(0.0f, 5.0f));
        bullet.GetComponent<BossBullet>().damage = damage;
    }
}
