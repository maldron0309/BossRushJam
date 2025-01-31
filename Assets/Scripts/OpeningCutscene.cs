using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OpeningCutscene : MonoBehaviour
{
    [SerializeField] Sprite[] frames;
    bool changing = false;
    int curFrame;
    Image img;
    public TextMeshProUGUI sceneText;
    [TextArea(4,10)]
    public string[] text;
    private int textIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundMusicManager.Instance.PlayOpenningTrack();
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
        sceneText.text = "";

        for (int i = 1; i < numF; i++)
        {

            curFrame++;
            img.sprite = frames[curFrame];
            yield return new WaitForSeconds(0.2f);
        }
        curFrame++;
        img.sprite = frames[curFrame];
        sceneText.text = text[textIdx];
        textIdx++;

        changing = false;
    }

    public void Skip()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
