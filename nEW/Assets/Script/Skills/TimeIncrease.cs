using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeIncrease : Skill
{
    protected override void Start()
    {
        name = "TimeIncrease";
    }

    protected override void Use()
    {
        base.Use();
        if (GameManager.instance.GetLevel() <= 5) GameManager.instance.timeCount += 5;
        else if (GameManager.instance.GetLevel() > 5) GameManager.instance.timeCount += 10;
    }

    public override int GetPrice()
    {
        return 100;
    }
}
