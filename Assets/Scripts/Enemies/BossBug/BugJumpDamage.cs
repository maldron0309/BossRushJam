using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugJumpDamage : MonoBehaviour
{
    GameObject player;
    public float damage;
    public AudioClip soundEffect;

    [SerializeField] GameObject shockwave;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss")) {
            //StartCoroutine(collision.gameObject.GetComponent<BugJumpAttack>().JumpBack());
            if(player != null)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(damage);
            }

            Instantiate(shockwave, transform.position, Quaternion.identity);
            GameObject opp = Instantiate(shockwave, transform.position, Quaternion.identity);
            opp.GetComponent<Shock>().speed *= -1;
            SoundEffectsManager.Instance.PlaySound(soundEffect);
            RoomCamera.instance.Shake(0.1f, .15f);
            Destroy(gameObject); 
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
}
