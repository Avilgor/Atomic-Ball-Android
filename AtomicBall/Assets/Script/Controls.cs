using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    float timer;
    void Start()
    {
        timer = 0;
        GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 3.0f) timer += Time.deltaTime;
        else
        {
            GetComponent<Animator>().enabled = true;
        }      
    }

    public void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
