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

        public Hand hand;


        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

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
                Climb();
            }
        }

        public void Climb()
        {
            Debug.Log("ShouldClimb");
        }

        
    }
}