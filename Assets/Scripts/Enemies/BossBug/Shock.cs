using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : MonoBehaviour
{
    
    public float stunTime = 5f;
    public float lifetime = 20f;
    public float speed;
    public GameObject stunEffect;
    public AudioClip stunSound;

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
            if (player.IsDashing() == false) { StartCoroutine(stun(stunTime)); }
        }
    }

    IEnumerator deathDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    IEnumerator stun(float time)
    {
        SoundEffectsManager.Instance.PlaySound(stunSound);
        GameObject effectObject = Instantiate(stunEffect, player.transform.position, Quaternion.identity);
        player.enabled = false;
        player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<BaseAttack>().canShoot = false;
        yield return new WaitForSeconds(time);
        player.enabled = true; ;
        player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<BaseAttack>().canShoot = true;
        Destroy(effectObject);

    }
}
