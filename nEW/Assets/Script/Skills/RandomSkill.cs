using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkill : Skill {
    protected override void Start()
    {
        name = "RandomSkill";
    }

    protected override void Use()
    {
        base.Use();
        int randNum = Random.Range(1, 7);
        Debug.Log("randNum : " + randNum);

        switch (randNum)
        {
            case 1 :
                GameManager.instance.timeCount += 5;
                break;
            case 2:
                GameManager.instance.timeCount += 10;
                break;
            case 3:
                GameManager.instance.timeCount -= 5;
                break;
            case 4:
                GameManager.instance.timeCount -= 10;
                break;
            case 5:
                FindObjectOfType<Budget>().hiding.SetActive(true);
                Invoke("OffHiding", 5);
                break;
            case 6:
                GameObject[] itemList = GameObject.FindGameObjectsWithTag("Item");
                randNum = Random.Range(0, itemList.Length);
                Destroy(itemList[randNum]);
                break;
        }
    }

    public override int GetPrice()
    {
        return 100;
    }

    private void OffHiding()
    {
        FindObjectOfType<Budget>().hiding.SetActive(false);
    }
}
