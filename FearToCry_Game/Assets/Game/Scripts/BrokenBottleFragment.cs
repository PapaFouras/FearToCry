using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BrokenBottleFragment : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddExplosionForce(100f, transform.position,.10f);
    }
}
