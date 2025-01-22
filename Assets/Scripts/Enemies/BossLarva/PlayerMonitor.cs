using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonitor : MonoBehaviour
{
    public bool playerIsIn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsIn = false;
        }
    }
}
