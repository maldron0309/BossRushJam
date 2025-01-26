using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    public static BlackScreen instance;
    public static BlackScreen CombineInstance;
    public Animator anim;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Show();
    }
    void Update()
    {
        
    }
    public void Show()
    {
        anim.Play("FadeIn");
    }
    public void Hide()
    {
        anim.Play("FadeOut");
    }
}
