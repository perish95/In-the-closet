using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Budget : MonoBehaviour
{
    public static Budget instance = null;
    public List<Skill> skillList;
    public List<int> skillCount;
    public GameObject hiding;

    private int money = 50000;
    private bool initList;
    [HideInInspector]public Text budgetText;
    [HideInInspector] public bool GameMgrInstance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            if (!initList) InitSkillCountList();
            budgetText.text = "Budget : " + money;
        }
    }

    public void InitSkillCountList()
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            skillCount.Add(0);
        }

        initList = true;
    }

    public void BuySkill(string clickedButtonName)
    {
        int index = 0;
        initList = true;

        foreach (Skill skill in skillList)
        {
            if(skill.name == clickedButtonName)
            {
                if (money >= skill.GetPrice())
                {
                    money -= skill.GetPrice();
                    skillCount[index]++;
                }
            }

            index++;
        }
    }

    public void MoneyIncrease()
    {
        money += 5;
    }

    private void OnLevelWasLoaded(int level)
    {

        if (SceneManager.GetActiveScene().name == "Shop")
            budgetText = GameObject.FindGameObjectWithTag("BudgetText").GetComponent<Text>();
    }
}
