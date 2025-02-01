using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyerSideAttack : MonoBehaviour
{
    public Transform topLeft;
    public Transform lowLeft;
    public Transform topRigt;
    public Transform lowRight;

    public Transform topTarget;
    public Transform lowTarget;
    public GameObject projectilePrefab;
    public bool isStarted = false;
    public float fireRate;
    public int numberOfProjectiles;
    public AudioClip soundEffect;

    private float fireCounter;
    private Vector2 projectileDir;
    private Rigidbody2D rb;
    private BossFlyerControll boss;
    private Animator anim;
    private int stage = 0;
    private Vector2 movedir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossFlyerControll>();
        anim = boss.anim;
    }
    void FixedUpdate()
    {
        if (!isStarted)
            return;


        if (stage == 0)
        {
            movedir = topTarget.position - transform.position;
            if (movedir.magnitude > 0.5f)
            {
                boss.Move(movedir.normalized);
            }
            else
            {
                stage = 1;
                boss.Move(Vector2.zero);
                fireCounter = fireRate;
            }
        }
        else if (stage == 1)
        {
            movedir = lowTarget.position - transform.position;
            if (movedir.magnitude > 0.5f)
            {
                boss.Move(movedir.normalized);
            }
            else
            {
                stage = 2;
                boss.Move(Vector2.zero);
            }
        }
        else if (stage == 2)
        {
            movedir = topTarget.position - transform.position;
            if (movedir.magnitude > 0.5f)
            {
                boss.Move(movedir.normalized);
                if (fireCounter > 0)
                    fireCounter -= Time.deltaTime;
                else
                {
                    Fire();
                    fireCounter = fireRate;
                }
            }
            else
            {
                stage = 3;
                boss.Move(Vector2.zero);
                fireCounter = fireRate;
            }
        }
        else
        {
            boss.isPerformingAction = false;
            isStarted = false;
        }
    }
    public void Fire()
    {
        float angleStep = 360f / numberOfProjectiles;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            //float angle = i * angleStep;
            Vector2 offset = projectileDir;

            Rigidbody2D rb = Instantiate(projectilePrefab, transform.position + (Vector3)offset, Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.velocity = projectileDir * 5;
            SoundEffectsManager.Instance.PlaySound(soundEffect);
        }
    }
    public void BeginAttack()
    {
        isStarted = true;
        boss.isPerformingAction = true;
        stage = 0;

        if(Vector2.Distance(transform.position, topLeft.position) > Vector2.Distance(transform.position, topRigt.position))
        {
            topTarget = topRigt;
            lowTarget = lowRight;
            projectileDir = Vector2.left;
        }
        else
        {
            topTarget = topLeft;
            lowTarget = lowLeft;
            projectileDir = Vector2.right;
        }
    }
}
