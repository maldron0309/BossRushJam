using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyerCenterAttack : MonoBehaviour
{
    public Transform centerPost;
    public GameObject projectilePrefab;
    public bool isStarted = false;
    public float fireRate;
    public int fireVollies = 3;
    public float fireAngleInc = 3;
    public int numberOfProjectiles;
    public float initialRadius = 1f;

    private float angleIncrement;
    private int VolliesCounter;
    private float attackCounter;
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

    // Update is called once per frame
    void FixedUpdate()
    {        
        if (!isStarted)
            return;

        movedir = centerPost.position - transform.position;

        if(stage == 0)
        {
            if (movedir.magnitude > 0.5f)
            {
                boss.Move(movedir.normalized);
            }
            else
            {
                stage = 1;
                attackCounter = fireRate;
                VolliesCounter = 0;
                angleIncrement = 0;
                boss.Move(Vector2.zero);
            }
        }
        else if (stage == 1)
        {
            if(VolliesCounter < fireVollies)
            {
                if (attackCounter > 0)
                    attackCounter -= Time.deltaTime;
                else
                {
                    VolliesCounter++;
                    attackCounter = fireRate;
                    Fire();
                    angleIncrement += 10;
                }
            }
            else
            {
                stage = 2;
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
            float angle = i * angleStep;
            Vector2 offset = new Vector2(
                Mathf.Cos((angle + angleIncrement) * Mathf.Deg2Rad) * initialRadius,
                Mathf.Sin((angle + angleIncrement) * Mathf.Deg2Rad) * initialRadius
            );

            Rigidbody2D rb = Instantiate(projectilePrefab, transform.position + (Vector3)offset, Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.velocity = offset * 5;
        }
    }
    public void BeginAttack()
    {
        isStarted = true;
        boss.isPerformingAction = true;
        stage = 0;
    }
}
