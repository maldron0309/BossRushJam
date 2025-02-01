using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneShowText : MonoBehaviour
{
    public GameObject textObj;
    public GameObject skipBtn;
    public void Show()
    {
        textObj.SetActive(true);
        skipBtn.SetActive(true);
    }
}
