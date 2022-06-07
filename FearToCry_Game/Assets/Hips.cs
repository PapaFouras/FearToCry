using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hips : MonoBehaviour
{
    public GameObject head;
    void FixedUpdate()
    {
        transform.position = new Vector3(head.transform.position.x,head.transform.position.y/2, head.transform.position.z);
        transform.localPosition += new Vector3(0,0,-.18f);
        transform.eulerAngles = new Vector3(0,head.transform.eulerAngles.y,0);
    }
}
