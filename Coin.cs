using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(coin);
            GameObject coinsObj = GameObject.Find("Coins");
            Coins coins = coinsObj.GetComponent<Coins>();
            coins.coinCounter += 1;
            Debug.Log(coins.coinCounter);
        }
    }
}
