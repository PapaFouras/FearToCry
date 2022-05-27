using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float delay = 3;

    void Start()
    {
        Destroy(gameObject, delay);
    }
}
