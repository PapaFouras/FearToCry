//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class TakingMatchStick : MonoBehaviour
    {
        public SteamVR_Action_Boolean takingMatchStickAction;
        
        public SteamVR_ActionSet takingMatchStickActionSet;

        public GameObject prefab;
        public Hand hand;

        public bool isGrabbing = false;


        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();


            if (takingMatchStickAction == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No plant action assigned", this);
                return;
            }

            takingMatchStickAction.AddOnChangeListener(OnTakingMatchStickActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (takingMatchStickAction != null){
                takingMatchStickAction.RemoveOnChangeListener(OnTakingMatchStickActionChange, hand.handType);
                isGrabbing = false;
            }
                
        }

        private void OnTakingMatchStickActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            Debug.Log("value trigger : "+ newValue);
            if (newValue)
            {
                if(!isGrabbing){
                   
                        SpawnAndAttachMatchStick();
                        isGrabbing = true;
                    
                } 
            }
            else{
                isGrabbing = false;
            }
        }


        public void SpawnAndAttachMatchStick()
        {
            Debug.Log("ShouldSpawnMatchStick");

            if(hand.currentAttachedObject == null){
                
                GameObject prefabObject = Instantiate(prefab,hand.transform.position, hand.transform.rotation);
                hand.AttachObject(prefabObject,GrabTypes.Grip,Hand.AttachmentFlags.ParentToHand|Hand.AttachmentFlags.SnapOnAttach | Hand.AttachmentFlags.TurnOffGravity);
                prefabObject.GetComponent<MatchStick>().hand = hand;
            }
            else{
                hand.DetachObject(hand.currentAttachedObject);
            }
            
			
        }

        
    }
}