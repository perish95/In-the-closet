using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public Animator over;
    public Animator clear;
    //public Animator clear;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        PrintText();
	}

    void PrintText() {
        if (GameManager.instance.endTag == 1)
            clear.SetBool("isClear", true);
        else if (GameManager.instance.endTag == 2)
            over.SetBool("isOver", true);
    }
}
