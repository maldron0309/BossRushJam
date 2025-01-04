using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatformStabilizer : MonoBehaviour
{
    public TextMeshPro text;
    private Rigidbody2D rb;
    private Quaternion initialRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialRotation = transform.localRotation;
    }

    private void FixedUpdate()
    {
        transform.rotation = initialRotation;
    }
}
