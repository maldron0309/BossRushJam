using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogScreen : MonoBehaviour
{
    public static DialogScreen instance;
    public TextMeshProUGUI message;
    public GameObject root;
    private string[] messageArray;
    private int currentmsgIdx = 0;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        root.SetActive(false);
    }
    public void Progess()
    {
        currentmsgIdx++;
        if (currentmsgIdx < messageArray.Length)
        {
            DisplayMessage(messageArray[currentmsgIdx]);
        }
        else
        {
            Close();
        }
    }
    public void Open(string[] messages)
    {
        root.SetActive(true);
        PlayerController.instance.Stop();
        PlayerController.instance.anim.Play("Idle");
        messageArray = messages;
        currentmsgIdx = 0;
        DisplayMessage(messageArray[currentmsgIdx]);
    }
    public void Close()
    {
        PlayerController.instance.Resume();
        root.SetActive(false);
    }
    public void DisplayMessage(string text)
    {
        message.text = text; 
    }
}
