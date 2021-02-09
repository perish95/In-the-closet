using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour {
    Rigidbody2D rb2d;
    [HideInInspector] public bool isEnter;
    public List<Item> randomizeItemList;
    public List<Item> originalItemList;
    
    private float convertTime;
    private bool isInCloset;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        isEnter = false;
        
        convertTime = GameManager.instance.timeCount / 2;
    }

    private void Update()
    {
        if(GameManager.instance.timeCount <= convertTime && !GameManager.instance.convertedSomething)
        {
            ConvertToRandom();
        }
    }

    void OnMouseDrag()
    {
        Vector2 mouseDragPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldObjectPosition = Camera.main.ScreenToWorldPoint(mouseDragPosition);
        transform.position = worldObjectPosition;
        GameManager.instance.isDragging = true;
    }

    private void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Check")
        {
            if (CheckMouse())
            {
                //GameManager.instance.setCount++;
                //Debug.Log(GameManager.instance.setCount);
                isInCloset = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Check")
        {
            if (CheckMouse())
            {
                rb2d.WakeUp();
                GetGravity();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Check") {
            isEnter = false;
            //GameManager.instance.setCount--;
            //Debug.Log(GameManager.instance.setCount);
            rb2d.Sleep(); //재운다
            rb2d.gravityScale = 0;
        }
    }

    bool CheckMouse() {
        if (Input.GetMouseButton(0))
        {
            Physics2D.gravity = new Vector3(0, -3, 0);
            isEnter = true;
            //GameManager.instance.holdMouse = true;
        }
        /*else if (Input.GetMouseButtonUp(0)) {
            GameManager.instance.holdMouse = false;
        }*/
        return isEnter;
    }

    void GetGravity()
    {
        if (isEnter) {
            rb2d.gravityScale = 3;
        }
    }

    private void ConvertToRandom()
    {
        Debug.Log("convert random");
        GameManager.instance.convertedOriginaItem = gameObject.GetComponent<Item>();
        GameManager.instance.convertedSomething = true;
        //if (isInCloset) GameManager.instance.setCount++;
        Instantiate(randomizeItemList[Random.Range(0, randomizeItemList.Count)], transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    public void ConvertToOriginal()
    {
        if (GameManager.instance.convertedOriginaItem != null && !originalItemList.Contains(this))
        {
            Debug.Log("reconvert");
            GameManager.instance.convertedOriginaItem.transform.position = gameObject.transform.position;
            Destroy(gameObject);
            GameManager.instance.convertedOriginaItem.gameObject.SetActive(true);
            //if(GameManager.instance.convertedOriginaItem.isInCloset) GameManager.instance.setCount++;
        }
    }

    public void SetList()
    {
        originalItemList.Clear();
        Item[] items = FindObjectsOfType<Item>();
        for(int i = 0; i<items.Length; i++)
        {
            originalItemList.Add(items[i]);
        }
    }

    private void OnLevelWasLoaded()
    {
        if (SceneManager.GetActiveScene().name == "Stage") SetList();
    }
}
