using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public SpriteMask lightMask;
    public float flickerRate;
    public float flickerRateRandom;
    public float minFlickerDuration;
    public float maxFlickerDuration;

    private float nextFlickerTimer;
    private float flickerDurationTimer;
    void Start()
    {
        nextFlickerTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (lightMask.enabled)
        {
            if (nextFlickerTimer > 0)
            {
                nextFlickerTimer -= Time.deltaTime;
            }
            else
            {
                lightMask.enabled = false;
                flickerDurationTimer = Random.Range(minFlickerDuration, maxFlickerDuration);
            }
        }
        else
        {
            if(flickerDurationTimer > 0)
            {
                flickerDurationTimer -= Time.deltaTime;
            }
            else
            {
                lightMask.enabled = true;
                nextFlickerTimer = flickerRate + Random.Range(0.0f, flickerRateRandom);
            }
        }
    }
}
