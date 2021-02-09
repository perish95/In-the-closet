using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource BGM;

	void Start () {
        DontDestroyOnLoad(gameObject);
	}
}
