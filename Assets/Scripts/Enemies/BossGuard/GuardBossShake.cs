using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBossShake : MonoBehaviour
{
    public void StepShake()
    {
        RoomCamera.instance.Shake(.35f, .3f);
    }
}
