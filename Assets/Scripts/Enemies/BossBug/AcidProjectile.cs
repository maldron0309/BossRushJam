using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidProjectile : MonoBehaviour
{
    [SerializeField] GameObject acidPool;
    public AudioClip soundEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(acidPool, new Vector3(transform.position.x,-3,0), Quaternion.identity);
        SoundEffectsManager.Instance.PlaySound(soundEffect);
        Destroy(gameObject);
    }
}
