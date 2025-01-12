using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBossSpinningShield : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 30.0f;
    public GameObject[] orbs;
    public List<GameObject> ShieldsLevel1;
    public List<GameObject> ShieldsLevel2;
    public List<GameObject> ShieldsLevel3;
    public int activeStage = 0;
    void Start()
    {

    }
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
        activeStage = count;
    }
    public void EnterStage(int idx)
    {
        activeStage = idx;
        if (idx == 1)
        {
            foreach (var orb in orbs)
            {
                orb.gameObject.SetActive(ShieldsLevel1.Contains(orb));
            }
        }
        else if (idx == 2)
        {
            foreach (var orb in orbs)
            {
                orb.gameObject.SetActive(ShieldsLevel2.Contains(orb));
            }
        }
        else if(idx == 3)
        {
            foreach (var orb in orbs)
            {
                orb.gameObject.SetActive(ShieldsLevel3.Contains(orb));
            }
        }
    }
}
