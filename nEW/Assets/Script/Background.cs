using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Item")
        {
            GameManager.instance.gameCheck = true;
            //GameManager.instance.GameOver();
            //배경 상단부에 collider에 trigger를 넣어서 시간이 다 됬을때 닿아있으면 gameover
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Item")
        {
            GameManager.instance.gameCheck = false;
        }
    }
}
