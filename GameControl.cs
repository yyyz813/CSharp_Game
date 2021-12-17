using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject objectToEnable;
    public bool enable = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enable)
        {
            objectToEnable.SetActive(true);
        }
        else
        {
            objectToEnable.SetActive(false);
        }
    }
}
