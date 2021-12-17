using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int coinCounter = 0;
    public GameObject block1;
    public GameObject block2;
    public GameObject block3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(coinCounter == 4)
        {
            Debug.Log("true");
            Destroy(block1);
        }

        if (coinCounter == 8)
        {
            Debug.Log("true");
            Destroy(block2);
        }

        if (coinCounter == 12)
        {
            Debug.Log("true");
            Destroy(block3);
        }
    }
    
}
