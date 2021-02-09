using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonParent : MonoBehaviour
{
    public Button thisButton;
    
    private AudioSource audioSource;

    public virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    public virtual void TaskOnClick()
    {
        audioSource.Play();
    }
}
