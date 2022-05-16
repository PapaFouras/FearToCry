using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class TeteAllumette : MonoBehaviour
{
    public Collider boiteAllumetteCollider;
    public GameObject boiteAllumette;
    public GameObject allumette;
   private void OnTriggerEnter(Collider other) {
       if(other == boiteAllumetteCollider){
           
           Debug.Log("boite " + boiteAllumette.GetComponent<VelocityEstimator>().GetVelocityEstimate().magnitude);
           Debug.Log("allumette "+ allumette.GetComponent<VelocityEstimator>().GetVelocityEstimate().magnitude);
           if((boiteAllumette.GetComponent<VelocityEstimator>().GetVelocityEstimate().magnitude < .3f)
           && allumette.GetComponent<VelocityEstimator>().GetVelocityEstimate().magnitude > .5f){
               Debug.Log("L'allumette s'allume");
           }
       }
   }
}
