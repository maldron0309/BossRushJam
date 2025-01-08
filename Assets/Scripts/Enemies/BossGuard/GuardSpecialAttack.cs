using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpecialAttack : MonoBehaviour
{
    public Transform attackDestenation;
    public Transform startPoint;
    public GameObject attackProjectile;
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
        GuardSpecialBullet bullet = Instantiate(attackProjectile, startPoint.position, Quaternion.identity).GetComponent<GuardSpecialBullet>();
        bullet.destenation = attackDestenation;
    }
}
