using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject currentRoom; // used to tell what room player is in.
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
}
