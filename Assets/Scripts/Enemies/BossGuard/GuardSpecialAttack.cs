using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpecialAttack : MonoBehaviour
{
    public Transform attackDestenation;
    public Transform startPoint;
    public GameObject attackProjectile;
    [SerializeField] Animator bossAnim;
    void Start()
    {
        attackDestenation.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BeginAttack()
    {
        bossAnim.SetTrigger("GreenShoot");
    }
    public void SpecialAttack()
    {
        GuardSpecialBullet bullet = Instantiate(attackProjectile, startPoint.position, Quaternion.identity).GetComponent<GuardSpecialBullet>();
        bullet.destenation = attackDestenation;
    }
}
