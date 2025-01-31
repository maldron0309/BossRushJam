using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBossMultiShot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float fireRate = 0.1f;
    [SerializeField] int bulletCount = 4;
    [SerializeField] bool aimAtPlayer = false;
    [SerializeField] private Animator bossAnim;
    public BossGuardController boss;
    private float reloadCounter;
    private bool isExecuting;
    private int bulletsLeft;
    public AudioClip soundEffect;
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
                Shoot();
            }
        }
    }
    public void Shoot()
    {
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector2 lineDir = new Vector2(boss.facingRight ? 1 : -1, 0);
        if(aimAtPlayer)
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        else    
            projectile.GetComponent<Rigidbody2D>().velocity = lineDir * projectileSpeed;
        SoundEffectsManager.Instance.PlaySound(soundEffect);

        reloadCounter = fireRate;
        bulletsLeft--;
        isExecuting = bulletsLeft > 0;
    }
    public void BeginSequence()
    {
        bossAnim.SetTrigger("RedShoot");
        StartCoroutine(delayedShooting());
    }

    IEnumerator delayedShooting()
    {
        yield return new WaitForSeconds(0.05f);
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
    }
}
