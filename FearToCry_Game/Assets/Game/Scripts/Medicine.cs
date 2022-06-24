using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Medicine : MonoBehaviour
{
    public Hand hand;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "HeadCollider"){
            OnDetached();
            GameManager.instance.ChangeRoom(1);
            
        }
    }

    private void Update() {
        if(hand == null){
            return;
        }
        if(hand.gameObject.name == "LeftHand"){
            if(hand.grabPinchAction.GetStateUp(Valve.VR.SteamVR_Input_Sources.LeftHand)){
                    hand.DetachObject(hand.currentAttachedObject);
            }   
        }
        if(hand.gameObject.name == "RightHand"){
            if(hand.grabPinchAction.GetStateUp(Valve.VR.SteamVR_Input_Sources.RightHand)){
                    hand.DetachObject(hand.currentAttachedObject);
            }  
        }

    }

    public void OnDetached(){

        Vector3 angularVelocity = Vector3.zero;
        Vector3 velocity = Vector3.zero;
        hand.GetEstimatedPeakVelocities(out velocity, out angularVelocity);
        GetComponent<Rigidbody>().velocity = velocity;
        GetComponent<Rigidbody>().angularVelocity = angularVelocity;
        
        StartCoroutine(DestroyAfterSeconds(1f));
    }

    IEnumerator DestroyAfterSeconds(float duration){
        GetComponent<Interactable>().enabled = false;
        
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
        
    }


}
