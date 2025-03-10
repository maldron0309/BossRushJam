using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugJumpAttack : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float Ypos;
    private Vector3 basePos;

    public GameObject Player;
    public GameObject DangerArea;
    private GameObject AttackSpot;
    
    private Rigidbody2D rb;
    public AudioClip soundEffect;

    private BossBugController boss;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = this.GetComponent<BossBugController>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        basePos = transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator BeginAttack()
    {
        SoundEffectsManager.Instance.PlaySound(soundEffect);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        boss.state = "launch";
        //float jumpPos = Random.Range(minX, maxX);
        float jumpPos = Player.transform.position.x;
        Debug.Log("Jumping to " + jumpPos.ToString());
        yield return new WaitForSeconds(1);
        boss.state = "smash";
        transform.position = new Vector2(jumpPos, transform.position.y);
        AttackSpot = Instantiate(DangerArea);
        AttackSpot.transform.position = new Vector2(jumpPos, Ypos);


        //Debug.Log("JumpBackStart");
        //yield return new WaitForSeconds(2f);
        //StartCoroutine(JumpBack());
    }

    public IEnumerator JumpBack()
    {
        
        Debug.Log(basePos.x);
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        

        yield return new WaitForSeconds(1);
        RoomCamera.instance.Shake(.4f, .3f);

        transform.position = new Vector2(basePos.x, transform.position.y);
    }
}
