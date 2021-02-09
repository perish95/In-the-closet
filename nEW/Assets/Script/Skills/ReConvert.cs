using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReConvert : Skill
{
    protected override void Start()
    {
        name = "ReConvert";
    }

    protected override void Use()
    {
        base.Use();
        if (GameManager.instance.convertedSomething)
        {
            Item[] items = FindObjectsOfType<Item>();

            foreach(Item item in items)
            {
                item.ConvertToOriginal();
            }
        }
        else
        {
            FindObjectOfType<Budget>().skillCount[index]++;
        }
    }

    public override int GetPrice()
    {
        return 100;
    }
}
