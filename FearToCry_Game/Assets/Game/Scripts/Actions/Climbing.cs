//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class Climbing : MonoBehaviour
    {
        public SteamVR_Action_Boolean climbingAction;
        
        public SteamVR_ActionSet climbingActionSet;

        public Hand hand;

        public GameObject player;
        private PlayerController playerController;


        bool _isClimbing = false;


        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();


            playerController = player.GetComponent<PlayerController>();


            if (climbingAction == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No plant action assigned", this);
                return;
            }

            climbingAction.AddOnChangeListener(OnClimbActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (climbingAction != null)
                climbingAction.RemoveOnChangeListener(OnClimbActionChange, hand.handType);
        }

        private void OnClimbActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            Debug.Log("value trigger : "+ newValue);
            if (newValue)
            {
                StartClimb();
                if(inputSource == SteamVR_Input_Sources.LeftHand){
                    climbingActionSet.Deactivate(SteamVR_Input_Sources.RightHand);
                }
                if(inputSource == SteamVR_Input_Sources.RightHand){
                    climbingActionSet.Deactivate(SteamVR_Input_Sources.LeftHand);
                }
            }
            if(!newValue){
                StopClimb();
            }
        }

        public void StartClimb()
        {
            Debug.Log("ShouldClimb");
            playerController.climbingHand = hand;
            
            _isClimbing = true;
        }

        public void StopClimb(){
            if(playerController.climbingHand == hand){
                _isClimbing = false;
                playerController.climbingHand = null;
                climbingActionSet.Deactivate(SteamVR_Input_Sources.LeftHand);
                climbingActionSet.Deactivate(SteamVR_Input_Sources.RightHand);

            }
        }

        
    }
}