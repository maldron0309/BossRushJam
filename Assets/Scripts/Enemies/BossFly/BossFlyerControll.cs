using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyerControll : BaseBossController
{
    [SerializeField] float timeBetweenActions;
    public BossHealth health;
    public Animator anim;
    [Header("Attacks")]
    public BossFlyerMoveAround flyAround;

    [Header("Start Event")]
    private int stage = 0;
    public SpinningPlatforms wheel;
    public SpinningPlatforms[] platforms;

    [Header("Other")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;
    public bool isPerformingAction = false;

    private bool hadMoved = false;
    private float nextActionCounter;
    private int rage = 0;
    private int normalMoveCounter = 0;
    void Start()
    {
        
    }
    void Update()
    {
        if (!isBattleStarted || isPerformingAction)
            return;

        if (nextActionCounter > 0)
        {
            nextActionCounter -= Time.deltaTime;
        }
        else
        {
            MakeRandomMove();
        }
        ManageStages();
        HandleAnimation();
    }
    public void ManageStages()
    {
        if(stage == 0)
        {
            stage = 1;
            wheel.rotationSpeed = -30;
            foreach (var item in platforms)
            {
                item.rotationSpeed = 60;
            }
        }
    }
    public void HandleAnimation()
    {
        if (isPerformingAction)
            return;

        //if (isGrounded)
        //{
        //    anim.Play("Idle");
        //}
    }
    public void MakeRandomMove()
    {
        //if (groundMonitor.playerIsIn)
        //{
        //    meeleAttack.BeginAttack();
        //}
        //else
        {
            int randomNumber = Random.Range(0, 100);
            flyAround.BeginAttack();
            nextActionCounter = timeBetweenActions;

        }
        hadMoved = !hadMoved;
    }
}
