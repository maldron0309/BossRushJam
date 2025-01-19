using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBossYellowGun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePointLow;
    [SerializeField] Transform firePointHigh;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float fireRate = 0.1f;
    [SerializeField] int bulletCount = 4;
    [SerializeField] bool aimAtPlayer = false;
    [SerializeField] Animator bossAnim;
    public BossGuardController boss;
    private float reloadCounter;
    private bool isExecuting;
    private int bulletsLeft;
    private Vector3 direction;
    

    void Start()
    {
        isExecuting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isExecuting)
        {
            if (reloadCounter > 0)
            {
                reloadCounter -= Time.deltaTime;
            }
            else
            {
                reloadCounter = fireRate;
                int height = 0;
                if ( (bulletCount - bulletsLeft) % 2 == 1)
                    height = 1;
                
                //Shoot(height);
            }
        }
    }
    public void Shoot(int height)
    {
        GameObject projectile = null;
        if (height == 0)
            projectile = Instantiate(bulletPrefab, firePointLow.position, Quaternion.identity);
        else if (height == 1)
            projectile = Instantiate(bulletPrefab, firePointHigh.position, Quaternion.identity);

        if (projectile)
        {
            Vector2 lineDir = new Vector2(boss.facingRight ? 1 : -1, 0);
            if (aimAtPlayer)
                projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            else
                projectile.GetComponent<Rigidbody2D>().velocity = lineDir * projectileSpeed;
        }
        reloadCounter = fireRate;
        bulletsLeft--;
        isExecuting = bulletsLeft > 0;
    }
    public void BeginSequence()
    {
        isExecuting = true;
        bulletsLeft = bulletCount;
        reloadCounter = 0;
        if (aimAtPlayer)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;

            if (player != null)
            {
                direction = (player.position - transform.position).normalized;
            }
        }
        if (bossAnim != null && isExecuting)
        {
            bossAnim.SetTrigger("YellowShoot");
        }
    }
}
