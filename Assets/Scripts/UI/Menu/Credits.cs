using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject root;

    public void CloseCredits()
    {
        root.SetActive(false);
    }
    public void OpenCredits() 
    {
        root.SetActive(true);
    }
}
