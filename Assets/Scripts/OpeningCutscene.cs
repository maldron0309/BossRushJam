using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningCutscene : MonoBehaviour
{
    [SerializeField] Sprite[] frames;
    bool changing = false;
    int curFrame;
    Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = this.GetComponent<Image>();
        curFrame = -1;
        StartCoroutine(ChangeFrames(4));
    }

    // Update is called once per frame
    void Update()
    {
        if(changing == false && (Input.GetButtonDown("Fire1")|| Input.GetButtonDown("Jump")))
        {
            StartCoroutine(ChangeFrames(6));
        }
        if(curFrame == 18)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator ChangeFrames(int numF)
    {
        changing = true;

        for (int i = 1; i < numF; i++)
        {

            curFrame++;
            img.sprite = frames[curFrame];
            yield return new WaitForSeconds(0.2f);
        }
        curFrame++;
        img.sprite = frames[curFrame];

        changing = false;
    }

    public void Skip()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
