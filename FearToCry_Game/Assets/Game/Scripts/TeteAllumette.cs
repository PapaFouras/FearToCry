using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class TeteAllumette : MonoBehaviour
{
    public GameObject boiteAllumette;
    public GameObject allumette;
    public GameObject allumetteMesh;

    private int currentScraperColliderHit = 0;
    public ParticleSystem fire;

    private bool _isTurnedOn = false;

    public FMODUnity.EventReference Braise_Allumette;
    FMOD.Studio.EventInstance braise_Allumette;

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

       if (currentScraperColliderHit >1 || other.gameObject.CompareTag("Burning")){
           Debug.Log("L'allumette doit s'allumer !");
           _isTurnedOn = true;
           gameObject.tag = "Burning";
           allumette.tag = "Burning";
           allumetteMesh.tag = "Burning";
           // braise_Allumette.start(); TODO : trouver l'endroit pour stop le son 
           fire.Play();
       }
   }

   private IEnumerator StartTimerAllumette(){
       yield return new WaitForSeconds(.6f);
       currentScraperColliderHit = 0;
   }
}
