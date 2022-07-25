using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class Flammable : MonoBehaviour
{
    public ParticleSystem ps;
    public GameObject parentGo;

    public UnityEvent onStartBurn;
    public UnityEvent onEndBurn;

    private float burnDuration = 3f;
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
        onStartBurn?.Invoke();
        StartCoroutine(EndBurn());

    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log(gameObject.name + " is TAGGING:: " + other.gameObject.tag);
        if(other.gameObject.CompareTag("Burning")){
            if (!isBurning)
            {
                StartBurning();
            }
        }
    }

    private IEnumerator EndBurn(){
        yield return new WaitForSeconds(burnDuration);
        if (parentGo != null)
        {
            parentGo.SetActive(false);
        }
        else 
        { 
            bool isAttached = false;
            int handIndex = 0;
            for(int i = 0;i< Player.instance.hands.Length; i++)
            {
                for(int j = 0; j< Player.instance.hands[i].AttachedObjects.Count; j++)
                {
                    if(Player.instance.hands[i].AttachedObjects[j].attachedObject == this.gameObject)
                    {
                        isAttached = true;
                    }
                }
            }
            if (isAttached)
            {
                Player.instance.hands[handIndex].DetachObject(gameObject);
            }
            gameObject.SetActive(false);
        }
        onEndBurn?.Invoke();
    }


}
