using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class TeteAllumette : MonoBehaviour
{
    public Collider scraper1;
    public Collider scraper2;
    public Collider scraper3;
    public GameObject boiteAllumette;
    public GameObject allumette;
    private bool isScraper1OK = false;
    private bool isScraper2OK = false;
    private bool isScraper3OK = false;


    private void OnTriggerEnter(Collider other) {
       if(other == scraper1){
           isScraper1OK = true;
           StartCoroutine(StartTimerAllumette());
           Debug.Log("allumette 1 OK");

       }
       else if(other == scraper2){
           isScraper2OK = true;
           StartCoroutine(StartTimerAllumette());
           Debug.Log("L'allumette 2 OK");

       }
       else if(other == scraper3){
           isScraper3OK = true;
           StartCoroutine(StartTimerAllumette());
           Debug.Log("L'allumette 3 OK");

       }
       if (GetShouldTurnOnAllumette()){
           Debug.Log("L'allumette doit s'allumer !");
       }
   }

   private IEnumerator StartTimerAllumette(){
       yield return new WaitForSeconds(.4f);
       isScraper1OK = false;
       isScraper2OK = false;
       isScraper3OK = false;
   }

   private bool GetShouldTurnOnAllumette(){
       return isScraper1OK && isScraper2OK && isScraper3OK;
   }
}
