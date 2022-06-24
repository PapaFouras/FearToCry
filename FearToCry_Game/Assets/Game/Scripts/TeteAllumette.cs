using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class TeteAllumette : MonoBehaviour
{
    public GameObject boiteAllumette;
    public GameObject allumette;

    private int currentScraperColliderHit = 0;
    public ParticleSystem fire;

    private bool _isTurnedOn = false;

    public bool GetIsTurnedOn(){
        return _isTurnedOn;
    }

    public void SetIsTurnedOn(bool isTurnedOn){
        _isTurnedOn = isTurnedOn;
    }

    private void OnTriggerEnter(Collider other) {
       if(other.tag == "scraper"){
           currentScraperColliderHit++;
           StartCoroutine(StartTimerAllumette());
           Debug.Log("allumette 1 OK");

       }
       if (currentScraperColliderHit >1){
           Debug.Log("L'allumette doit s'allumer !");
           _isTurnedOn = true;
           gameObject.tag = "Burning";
           allumette.tag = "Burning";
           fire.Play();
       }
   }

   private IEnumerator StartTimerAllumette(){
       yield return new WaitForSeconds(.6f);
       currentScraperColliderHit = 0;
   }
}
