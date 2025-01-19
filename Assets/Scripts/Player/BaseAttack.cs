using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    public int maxCharges = 1;
    public int currentCharges;
    public bool canShoot = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    virtual public void OnRelease()
    {

    }
    virtual public void OnPressed()
    {

    }
    virtual public void Fire()
    {

    }
}
