using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    
    public float stunTime = 5f;
    public float lifetime = 20f;
    public float speed;

    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(deathDelay(lifetime));
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position += Vector3.right * speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            StartCoroutine(stun(stunTime)); 
        }
    }

    IEnumerator deathDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    IEnumerator stun(float time)
    {
        player.enabled = false;
        yield return new WaitForSeconds(time);
        player.enabled = true; ;

    }
}
