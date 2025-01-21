using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyerSideAttack : MonoBehaviour
{
    public float moveSpeed;
    public bool isStarted = false;
    private Rigidbody2D rb;
    private BossFlyerControll boss;
    private Animator anim;
    private int stage = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossFlyerControll>();
        anim = boss.anim;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BeginAttack()
    {
        isStarted = true;
        boss.isPerformingAction = true;
        stage = 0;
    }
}
