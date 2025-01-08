using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBossSpinningShield : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 30.0f;
    public GameObject[] orbs;
    public int activeOrbs;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
    public void EnableOrbs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            orbs[i].SetActive(true);
        }
        activeOrbs = count;
    }
}
