using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheThingShake : MonoBehaviour
{
    public void Shake()
    {
        RoomCamera.instance.Shake(.5f, .35f);
    }
}
