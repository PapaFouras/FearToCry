using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flammable : MonoBehaviour
{
    public ParticleSystem ps;
    public GameObject parentGo;

    public UnityEvent onEndBurn;

    private float burnDuration = 5f;
    
    [HideInInspector]
    public bool isBurning = false;
    private void Awake() {
        if(TryGetComponent<ParticleSystem>(out ParticleSystem ps1)){
            ps = ps1;
        }
    }
    public void StartBurning(){
        gameObject.tag = "Burning";
        if(parentGo != null){
            parentGo.tag = "Burning";
        }
        ps.Play();
        isBurning = true;
        StartCoroutine(EndBurn());

    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log(gameObject.name + " is TAGGING:: " + other.gameObject.tag);
        if(other.gameObject.CompareTag("Burning")){
            StartBurning();
        }
    }

    private IEnumerator EndBurn(){
        yield return new WaitForSeconds(burnDuration);
        if(parentGo != null){
            parentGo.SetActive(false);
        }
        else{
            gameObject.SetActive(false);
        }
        onEndBurn?.Invoke();
    }
}
