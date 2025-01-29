using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyerCharge : MonoBehaviour
{

    public float chargeSpeed;
    private float originalMoveSpeed;
    private bool isFInishing = false;
    private Transform targetPost;

    public float chargeDelay;
    private float delayCounter;
    public float chargeTime;
    private float chargeCounter;

    public bool isStarted = false;
    private Rigidbody2D rb;
    private BossFlyerControll boss;
    private Vector2 movedir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossFlyerControll>();
        originalMoveSpeed = boss.moveSpeed;
    }
    void FixedUpdate()
    {
        if (!isStarted)
            return;
        if(delayCounter > 0)
        {
            delayCounter -= Time.deltaTime;
        }
        else
        {
            if (chargeCounter > 0)
            {
                chargeCounter -= Time.deltaTime;
                boss.Move(movedir);
            }
            else
            {
                boss.isPerformingAction = false;
                isStarted = false;
                boss.moveSpeed = originalMoveSpeed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BeginAttack()
    {
        isFInishing = false;
        isStarted = true;
        boss.isPerformingAction = true;
        boss.moveSpeed = chargeSpeed;
        chargeCounter = chargeTime;
        delayCounter = chargeDelay;
        movedir = (PlayerController.instance.transform.position - transform.position).normalized;
        boss.Move(Vector2.zero);
    }
}
