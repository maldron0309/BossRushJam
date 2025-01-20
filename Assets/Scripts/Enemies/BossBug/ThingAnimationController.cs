using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingAnimationController : MonoBehaviour
{
    public bool onGround;
    public float groundCheckRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private BossBugController boss;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        boss = this.GetComponent<BossBugController>();
        anim = boss.anim;
    }

    // Update is called once per frame
    void Update()
    {
        if (!boss.state.Equals(""))
        {
            if (boss.state.Equals("smash")) { anim.Play("Smash"); StartCoroutine(resetState(1f)); }
            else if (boss.state.Equals("launch")) { anim.Play("LaunchUp");}
            else if (boss.state.Equals("spit")) { anim.Play("Spitting"); StartCoroutine(resetState(0.3f)); }



        }
        else if (CheckGround()) anim.Play("Idle");
        else anim.Play("Spin");
    }

    public bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    IEnumerator resetState(float time)
    {
        yield return new WaitForSeconds(time);
        boss.state = "";
    }
}
