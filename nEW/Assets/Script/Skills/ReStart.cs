using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart : Skill
{
    protected override void Start()
    {
        name = "ReStart";
    }

    protected override void Use()
    {
        base.Use();
        if(SceneManager.GetActiveScene().name == "Stage")
        {
            GameManager.instance.DecreaseLevel();
            //GameManager.instance.DecreaseLevel();
            //GameManager.instance.InitGame();
            GameManager.instance.isUsingSkill = true;
            Invoke("ChangeSwitch", 0.5f);
            SceneManager.LoadScene("Stage");
        }
    }

    public override int GetPrice()
    {
        return 100;
    }

    private void ChangeSwitch()
    {
        GameManager.instance.isUsingSkill = false;
    }
}
