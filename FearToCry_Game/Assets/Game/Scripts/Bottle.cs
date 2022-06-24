using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Bottle : MonoBehaviour
{
    VelocityEstimator ve;
    private void Awake()
    {
        ve = new VelocityEstimator();
    }
    private void OnCollisionEnter(Collision collision)
    {
        float velocity = ve.GetVelocityEstimate().magnitude;
        Debug.Log("Ca a touché !!! : " + velocity);
        if (velocity >= 1)
        {
            
        }
    }

}
