using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hiding : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "Stage") gameObject.SetActive(false);
    }
}
