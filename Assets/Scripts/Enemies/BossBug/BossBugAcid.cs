using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBugAcid : MonoBehaviour
{
    GameObject player;
    public float damage;
    public float damageDelay;
    public float lifetime;
    float damageTimer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(deathDelay(lifetime));
    }

    // Update is called once per frame
    void Update()
    {
        if (damageTimer > 0) { damageTimer -= Time.deltaTime; }
        else
        {
            if (player != null)
            {
                damageTimer = damageDelay;
                player.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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

    IEnumerator deathDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
