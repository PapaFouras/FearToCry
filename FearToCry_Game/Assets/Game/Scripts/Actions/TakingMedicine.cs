//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class TakingMedicine : MonoBehaviour
    {
        public SteamVR_Action_Boolean takingMedicineAction;
        
        public SteamVR_ActionSet takingMedicineActionSet;

        public GameObject prefab;
        public Hand hand;

        public bool isGrabbing = false;


        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();


            if (takingMedicineAction == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No plant action assigned", this);
                return;
            }

            takingMedicineAction.AddOnChangeListener(OnTakingMedicineActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (takingMedicineAction != null){
                takingMedicineAction.RemoveOnChangeListener(OnTakingMedicineActionChange, hand.handType);
                isGrabbing = false;
            }
                
        }

        private void OnTakingMedicineActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            Debug.Log("value trigger : "+ newValue);
            if (newValue)
            {
                if(!isGrabbing){
                   
                        SpawnAndAttachMedicine();
                        isGrabbing = true;
                    
                } 
            }
            else{
                isGrabbing = false;
            }
        }


        public void SpawnAndAttachMedicine()
        {
            Debug.Log("ShouldSpawnMedicine");

            if(hand.currentAttachedObject == null){
                
                GameObject prefabObject = Instantiate(prefab,hand.transform.position, hand.transform.rotation);
                hand.AttachObject(prefabObject,GrabTypes.Grip,Hand.AttachmentFlags.ParentToHand|Hand.AttachmentFlags.SnapOnAttach| Hand.AttachmentFlags.TurnOffGravity);
                prefabObject.GetComponent<Medicine>().hand = hand;
            }
            else{
                hand.DetachObject(hand.currentAttachedObject);
            }
            
			
        }

        
    }
}