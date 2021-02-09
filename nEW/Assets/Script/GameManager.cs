using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class ItemArray
{
    [System.Serializable]
    public class RandNum
    {
        public int min, max;
    }

    public RandNum[] randoms;
    public GameObject[] stageItems;
}

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public Text timeLabel;
    public float timeCount;
    //public GameObject[] ItemList;
    public List<Skill> canSkillList;
    public GameObject coin;
    public AudioClip clearClip;
    public AudioClip failClip;
    public List<GameObject> setPositions;
    public ItemArray[] stageArray;
    [HideInInspector] public bool gameCheck = false; //gameover check, true <= GameOver
    [HideInInspector] public int setCount;//들어간 아이템 갯수
    [HideInInspector] public bool holdMouse = false;
    [HideInInspector] public bool convertedSomething; //바뀐 템이 있는가
    [HideInInspector] public int endTag = 0; //1==클리어, 2==게임오버
    [HideInInspector] public float usedSkillTime = 0;
    [HideInInspector] public Item convertedOriginaItem;
    [HideInInspector] public Item convertedItem;
    [HideInInspector] public GameObject blocking;
    [HideInInspector] public bool isDragging;
    [HideInInspector] public GameObject hidingObject; // 옷장 가리는 오브젝트
    [HideInInspector] public bool isUsingSkill;

    private int level = 0;
    private int column;
    private int row;
    private int itemCount;//배치된 아이템 갯수
    private List<Vector3> gridPositions = new List<Vector3>();
    private List<bool> isInstantiate = new List<bool>();
    private int instantiateCount = 0;
    private Budget budget;
    private bool keyDown;
    private bool timeOver = false;
    private bool playOnce;

    void Awake () {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        budget = FindObjectOfType<Budget>();
	}


    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && !keyDown && timeCount > 0)
        {
            int index = 0;
            if(usedSkillTime < Time.time - 1.0)
            {
                usedSkillTime = Time.time;
                foreach (Skill skill in canSkillList)
                {
                    skill.UseSkill(e.keyCode, index);
                    index++;
                }
            }
        }
    }

    void Update () {
        if(SceneManager.GetActiveScene().name == "Stage")
            TimeManager();
        GameOver();
        CheckHoldingMouse();
        CheckShop();
        //if(blocking != null) CheckBlocking();
	}

    public void InitGame() {
        enabled = true;
        Text[] temp = FindObjectsOfType<Text>();
        foreach(Text tmp in temp) {
            if (tmp.name == "Timer")
                timeLabel = tmp;
        }

        if (SceneManager.GetActiveScene().name == "Stage")
        {
            level++;
            FindObjectOfType<CoinPush>().gameObject.SetActive(true);
            SettingPos();
            itemCount = 0;
            TestFunc();
            Debug.Log("itemCount " + itemCount);
        }

        endTag = 0;
        setCount = 0;
        //itemCount = 0;
        //timeCount = 15;
        convertedSomething = false;
    }

    public void TestFunc() {
        int numOfItem = 0;
        int makeCount;
        //itemCount = 0;
        Debug.Log("testfunc in level " + level);
        foreach (GameObject tmp in stageArray[level-1].stageItems) {
            makeCount = GetRanNum(numOfItem);

            for (int i = 0; i < makeCount; i++) {
                int randNum = Random.Range(0, gridPositions.Count);
                if (isInstantiate[randNum])
                {
                    i--;
                    continue;
                }
                Instantiate(tmp, gridPositions[randNum], Quaternion.identity);
                isInstantiate[randNum] = true;
                itemCount++;
            }
            numOfItem++;
        }
    }

    int GetRanNum(int num)
    {
        return Random.Range(stageArray[level-1].randoms[num].min, stageArray[level-1].randoms[num].max);
    }

    /*void InitPosition() {
        column = -26;
        row = -16;
        gridPositions.Clear();
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 5; j++) {
                gridPositions.Add(new Vector3(column, row, 0));
                isInstantiate.Add(false);
                column += 3;
            }row += 2;
        }
    }*/

    void SettingPos() {
        gridPositions.Clear();
        isInstantiate.Clear();
        setPositions.Clear();
        
        foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("SetPosition"))
        {
            setPositions.Add(gameObject);
        }


        foreach (GameObject go in setPositions){
            gridPositions.Add(go.transform.position);
            isInstantiate.Add(false);
        }
    }

    void CheckHoldingMouse() {
        if (Input.GetMouseButton(0))
            holdMouse = true;
        else if (Input.GetMouseButtonUp(0))
            holdMouse = false;
    }

    void TimeManager() {
        if (timeCount < 0)
        {
            timeOver = true;
            gameCheck = true;
            timeCount = 0;
            if (SceneManager.GetActiveScene().name == "Stage") timeLabel.text = timeCount.ToString("0.00");
        }
        if (!timeOver)
        {
            timeCount -= Time.deltaTime;
            if (SceneManager.GetActiveScene().name == "Stage") timeLabel.text = timeCount.ToString("0.00");
        }
    }

    //void GameSetUp(int level)
    //{
    //    //for (int i = 0; i < itemCount; i++)
    //    //{
    //    //    GameObject temp = ItemList[Random.Range(0, ItemList.Length)];
    //    //    Instantiate(temp, gridPositions[i], Quaternion.identity);
    //    //}

    //    while (true)
    //    {
    //        if (instantiateCount >= itemCount) break;

    //        int randNum = Random.Range(0, gridPositions.Count);
    //        if (isInstantiate[randNum]) continue;

    //        GameObject temp = ItemList[Random.Range(0, ItemList.Length)];
    //        Instantiate(temp, gridPositions[randNum], Quaternion.identity);
    //        isInstantiate[randNum] = true;
    //        instantiateCount++;
    //    }
    //}

    public void GameOver() {
        if (!holdMouse)
        {
            if (timeOver)//fail
            {
                if (gameCheck)
                    if (itemCount != setCount)
                    {
                        //animator.SetTrigger("isOver");
                        if (!playOnce)
                        {
                            GetComponent<AudioSource>().clip = failClip;
                            GetComponent<AudioSource>().Play();

                            playOnce = true;
                        }

                        endTag = 2;
                    }

            }

            else if (!timeOver)//pass
            {
                if (!gameCheck)
                    if (itemCount == setCount && !isUsingSkill && itemCount != 0)
                    {
                        GetComponent<AudioSource>().clip = clearClip;
                        GetComponent<AudioSource>().Play();

                        endTag = 1;

                        if (level == 11) Invoke("GoClear", 2.0f);
                        else
                        {
                            Invoke("GoShop", 5.0f);
                            //StartCoroutine("GoShop");
                            enabled = false; //GameOver 함수를 업데이트에 둬서 텀을 두면 자꾸 호출하는게
                                             //문제였음 그래서 여기서 Update를 잠시 멈춤
                            CreateCoin(); // 클리어시 코인 생성
                        }
                    }
            }
        }
    }

    void GoShop() {
        SceneManager.LoadScene("Shop");
    }

    private void GoClear()
    {
        SceneManager.LoadScene("Clear");
    }

    private void OnLevelWasLoaded()
    {
        if(instance == this)
        {
            InitGame();
            Debug.Log("Check On level " + level);
        }

    }

    public int GetLevel()
    {
        return level;
    }

    public void DecreaseLevel()
    {
        level--;
    }

    private void CheckShop()
    {
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            GameObject.FindGameObjectWithTag("StageText").gameObject.GetComponent<Text>().text = "Stage " + (level + 1).ToString();
        }
    }

    private void CheckBlocking()
    {
        if (!isDragging)
        {
            blocking.SetActive(false);
        }
        else blocking.SetActive(true);
    }

    private void CreateCoin()
    {
        FindObjectOfType<CoinPush>().gameObject.SetActive(true);
        int count = Random.Range(10, 16);

        float minw = Screen.width - Screen.width * 0.83f, maxw = Screen.width * 0.83f;
        float minh = Screen.height - Screen.height * 0.74f, maxh = Screen.height * 0.74f;

        for (int i = 0; i< count; i++)
        {
            Vector3 coinPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(minw, maxw), Random.Range(minh, maxh), 10));
            Instantiate(coin, coinPos, Quaternion.identity);
        }
    }
}
