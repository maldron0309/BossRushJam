using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossController : MonoBehaviour
{
    public bool facingRight = true;
    public bool isBattleStarted = false;
    public GameObject model;
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
    public bool FacinRight()
    {
        return facingRight;
    }
    public void Flip()
    {
        facingRight = !facingRight;

        // Flip the sprite by inverting the local scale's X value
        Vector3 scale = model.transform.localScale;
        scale.x *= -1;
        model.transform.localScale = scale;
    }
    public void FacePlayer()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if ((transform.position.x > player.transform.position.x && facingRight) || (transform.position.x < player.transform.position.x && !facingRight))
        {
            Flip();
        }

    }
    public virtual void OnDefeat()
    {

    }
}
