using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nextScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("OpeningCutscene");
    }
}
