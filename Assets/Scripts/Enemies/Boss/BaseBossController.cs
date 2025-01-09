using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossController : MonoBehaviour
{
    public bool facingRight = true;
    public bool isBattleStarted = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartBossBattle()
    {
        isBattleStarted = true;
    }

}
