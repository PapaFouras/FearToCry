using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    public FMOD.Studio.EventInstance braise_Allumette;

    public FMODUnity.EventReference Allumette_Scratch;
    public FMOD.Studio.EventInstance Allumette_Scratch_instance;

    public UnityEvent onStartBurn;

    private void Awake()
    {
        braise_Allumette = FMODUnity.RuntimeManager.CreateInstance(Braise_Allumette);
        FMODUnity.RuntimeManager.CreateInstance(Allumette_Scratch.Path);
    }

    public bool GetIsTurnedOn(){
        return _isTurnedOn;
    }

    public void SetIsTurnedOn(bool isTurnedOn){
        _isTurnedOn = isTurnedOn;
    }

    private void OnTriggerEnter(Collider other) {
       if(other.tag == "scraper"){
           currentScraperColliderHit++;
            FMODUnity.RuntimeManager.PlayOneShot(Allumette_Scratch, transform.position);
            StartCoroutine(StartTimerAllumette());
           Debug.Log("allumette 1 OK");

       }

       if (currentScraperColliderHit >1 || other.gameObject.CompareTag("Burning")){
           Debug.Log("L'allumette doit s'allumer !");
           _isTurnedOn = true;
            onStartBurn?.Invoke();
           gameObject.tag = "Burning";
           allumette.tag = "Burning";
           allumetteMesh.tag = "Burning";
           braise_Allumette.start(); //TODO : trouver l'endroit pour stop le son 
           fire.Play();
       }
   }

   private IEnumerator StartTimerAllumette(){
       yield return new WaitForSeconds(.6f);
       currentScraperColliderHit = 0;
   }

    private void OnDestroy()
    {
        braise_Allumette.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
