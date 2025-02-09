using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIntroUI : MonoBehaviour
{
    public static BossIntroUI instance;
    public GameObject root;
    public Animator anim;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hide()
    {
        root.SetActive(false);
    }
    public void Show()
    {
        root.SetActive(true);
        anim.Play("Show");
        StartCoroutine(HideAfterTime());
    }
    public IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(2);
        anim.Play("Hide");
        yield return new WaitForSeconds(1);
        Hide();
    }
}
