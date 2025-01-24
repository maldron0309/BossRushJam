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

    bool resetting;

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
            else if (boss.state.Equals("onwall")) { anim.Play("WallLanding"); transform.rotation = Quaternion.Euler(0f, 0f, 90f); }
            else if (boss.state.Equals("wallspit")) { Debug.Log("EEE"); anim.Play("WallSpit"); StartCoroutine(resetState(3f)); }


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
        if(resetting == false)
        {
            resetting = true;
            Debug.Log(time); 
            yield return new WaitForSeconds(time);
            boss.state = "";
            transform.rotation = Quaternion.identity;
            resetting = false;
        }
        
        
    }
}
