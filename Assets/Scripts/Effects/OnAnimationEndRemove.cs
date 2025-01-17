using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnimationEndRemove : MonoBehaviour
{
    public void RemoveObject()
    {
        Destroy(gameObject); // Destroy the object after the animation ends
    }
}
