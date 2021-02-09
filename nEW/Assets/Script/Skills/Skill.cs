using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public KeyCode usingKey;

    protected GameManager gameManager;
    protected int price = 100;
    protected int index;

    private bool skillUsed;

    protected abstract void Start();

    public void UseSkill(KeyCode pressedKey, int index)
    {
        this.index = index;

        if (usingKey == pressedKey && !skillUsed)
        {
            if (FindObjectOfType<Budget>().skillCount[index] > 0)
            {
                FindObjectOfType<Budget>().skillCount[index]--;
                Use();
            }

            skillUsed = true;
            Invoke("ChangeUsed", 0.5f);
        }
    }

    protected virtual void Use()
    {
        Debug.Log(name);
    }

    public abstract int GetPrice();

    private void ChangeUsed()
    {
        skillUsed = false;
    }
}

