using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ObjectToInstantiate;
    public float frequency = 1;
    float timer;

    void Start()
    {
        enabled = false;
    }

    public void Activate()
    {
        enabled = true;
        timer = frequency;
    }

    
    void Update()
    {
        timer -= frequency;
        if(timer < 0)
        {
            Instantiate(ObjectToInstantiate, transform.position, Quaternion.identity);
            timer = frequency;
        }
    }
}
