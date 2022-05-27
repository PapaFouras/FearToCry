using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;
using Valve.VR.InteractionSystem;


public class PlayerController : MonoBehaviour
{

    public CharacterController characterController;

    public Hand climbingHand = null;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(climbingHand != null){  
            Vector3  velocity = Vector3.zero;
            Vector3 angularVelocity = Vector3.zero;
           // velocity = climbingHand.GetComponent<VelocityEstimator>().GetVelocityEstimate();
            climbingHand.GetEstimatedPeakVelocities(out velocity, out angularVelocity);
            
            transform.position += ( -velocity * Time.fixedDeltaTime);   
        }
        else{
            if(transform.position.y >=0f)
                transform.position += new Vector3(0,-.05f,0);
        }
       
    }
}
