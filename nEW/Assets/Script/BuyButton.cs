using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : ButtonParent {
    public string buyWhat;
    private bool canBuy = true;
    private float clickDelay = 0.1f;

    public override void Start()
    {
        base.Start();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    public override void TaskOnClick()
    {
        if (canBuy)
        {
            base.TaskOnClick();
            Budget budget = FindObjectOfType<Budget>();
            budget.BuySkill(buyWhat);
            canBuy = false;
            Invoke("CanBuy", clickDelay);
        }
    }

    private void CanBuy()
    {
        canBuy = true;
    }
}
