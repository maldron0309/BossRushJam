using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableElevator : MonoBehaviour, IInteractable
{
    public Transform endPoint1;
    public Transform endPoint2;
    private Transform destenation;
    public float maxSpeed = 5;
    public float acceleration = 1;
    private float speed;
    private Rigidbody2D rb;
    private bool isInUse; 
    private bool isActive;
    public Transform markLocation;
    public Transform MarkLocation => markLocation;
    public AudioClip soundEffect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        destenation = endPoint1;
        transform.position = endPoint1.position;
    }
    void Start()
    {

    }
    public void Interact(PlayerController player)
    {
        if (destenation == endPoint1)
            destenation = endPoint2;
        else
            destenation = endPoint1;
        
        isInUse = true;
        SoundEffectsManager.Instance.PlaySound(soundEffect);
    }
    void FixedUpdate()
    {
        if (isInUse)
        {
            Vector3 dir = destenation.position - transform.position;
            float dist = dir.sqrMagnitude;
            if (dist > speed * Time.deltaTime)
            {
                rb.velocity = dir.normalized * speed;// * Time.deltaTime;
                if (speed < maxSpeed)
                    speed = Mathf.Min(maxSpeed, speed + acceleration * Time.deltaTime);
            }
            else
            {
                rb.velocity = Vector3.zero;
                transform.position = destenation.position;
                speed = 0;
                isInUse = false;
            }
        }
    }
}
