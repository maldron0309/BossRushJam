using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLarvaRunning : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public int damage;
    public Transform[] posts;
    public float attackRate = 1;
    private float attackCooldown;

    public float moveSpeed;
    private bool isFInishing = false;
    private Transform targetPost;

    public bool isStarted = false;
    private Rigidbody2D rb;
    private BossLarvaController boss;
    private int movedir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossLarvaController>();
        attackCooldown = attackRate;
    }
    void FixedUpdate()
    {
        if (!isStarted)
            return;

        float dist = targetPost.position.x - transform.position.x;
        if (dist > 0)
            movedir = 1;
        else
            movedir = -1;

        if (Mathf.Abs(dist) > 0.5f)
        {
            Move();

            if (attackCooldown > 0)
                attackCooldown -= Time.deltaTime;
            else
            {
                Fire();
                attackCooldown = attackRate;
            }
        }
        else
        {
            boss.isPerformingAction = false;
            isStarted = false;
            boss.FacePlayer();
        }
    }
    public void BeginAttack()
    {
        targetPost = posts[Random.Range(0, posts.Length)];

        boss.FacePlayer();
        isFInishing = false;
        isStarted = true;
        boss.isPerformingAction = true;
    }
    public void Fire()
    {
        Rigidbody2D bullet;
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Rigidbody2D>();
        bullet.velocity = Vector2.up * (bulletSpeed + Random.Range(0.0f, 5.0f));
        bullet.GetComponent<BossBullet>().damage = damage;
    }
    public void Move()
    {
        rb.velocity = new Vector2(movedir * moveSpeed, rb.velocity.y);
    }
}
