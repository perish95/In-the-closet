using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    private bool isGathered;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-200, 200), Random.Range(-200, 200)));
    }

    private void OnMouseUpAsButton()
    {
        if (!isGathered)
        {
            isGathered = true;
            FindObjectOfType<Budget>().MoneyIncrease();
            Invoke("DestroyAfterSeconds", 0.5f);
            GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        }
    }

    private void OnMouseDown()
    {
        if (!isGathered)
            GetComponent<AudioSource>().Play();
    }

    private void DestroyAfterSeconds()
    {
        Destroy(gameObject);
    }
}
