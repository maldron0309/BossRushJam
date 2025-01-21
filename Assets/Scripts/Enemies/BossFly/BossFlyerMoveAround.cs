using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyerMoveAround : MonoBehaviour
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
    private BossFlyerControll boss;
    private Vector2 movedir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossFlyerControll>();
        attackCooldown = attackRate;
    }
    void FixedUpdate()
    {
        if (!isStarted)
            return;

        movedir = (targetPost.position - transform.position);

        if (movedir.magnitude > 0.5f)
        {
            boss.Move(movedir.normalized);

            if (attackCooldown > 0)
                attackCooldown -= Time.deltaTime;
            else
            {
                attackCooldown = attackRate;
            }
        }
        else
        {
            boss.isPerformingAction = false;
            isStarted = false;
            //boss.FacePlayer();
        }
    }
    public void BeginAttack()
    {
        targetPost = posts[Random.Range(0, posts.Length)];

        //boss.FacePlayer();
        isFInishing = false;
        isStarted = true;
        boss.isPerformingAction = true;
    }
}
