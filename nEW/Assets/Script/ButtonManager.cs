using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
    public void GoShopButton() {
        SceneManager.LoadScene("Shop");
    }

    public void GoStageButton() {
        SceneManager.LoadScene("Stage");
        //GameManager gameManager;
    }
}
