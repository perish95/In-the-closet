using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInDrawer : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Item")
        {
            GameManager.instance.setCount++;
            Debug.Log("setCount " + GameManager.instance.setCount);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Item")
        {
            GameManager.instance.setCount--;
            Debug.Log("setCount " + GameManager.instance.setCount);
        }
    }
}
