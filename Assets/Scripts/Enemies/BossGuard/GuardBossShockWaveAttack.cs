using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBossShockWaveAttack : MonoBehaviour
{
    [SerializeField] private GameObject shockwavePrefab; 
    [SerializeField] private Transform shockwaveSpawnPoint; 
    [SerializeField] private float shockwaveSpeed = 5f; 
    [SerializeField] private float shockwaveLifetime = 2f;
    [SerializeField] private Animator bossAnim;

    void Start()
    {
    }

    private void Update()
    {
        
    }
    public void TriggerShockwave()
    {
        
        GameObject shockwave = Instantiate(shockwavePrefab, shockwaveSpawnPoint.position, Quaternion.identity);

        Shockwave shockwaveScript = shockwave.GetComponent<Shockwave>();
        if (shockwaveScript != null)
        {
            shockwaveScript.Initialize(Vector2.left, shockwaveSpeed, shockwaveLifetime);
        }
    }

    public void Step()
    {
        bossAnim.SetTrigger("Step");
    }
    

}
