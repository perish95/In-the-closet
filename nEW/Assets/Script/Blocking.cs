using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : MonoBehaviour {
	void Start () {
        GameManager.instance.blocking = gameObject;
	}
}
