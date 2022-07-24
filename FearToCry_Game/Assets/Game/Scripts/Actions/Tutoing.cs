//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class Tutoing : MonoBehaviour
    {
        public SteamVR_Action_Boolean tutoingAction;

        public Hand hand;

        bool isComplete = false;
        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (tutoingAction == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No plant action assigned", this);
                return;
            }

            tutoingAction.AddOnChangeListener(OnTutoingActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (tutoingAction != null)
                tutoingAction.RemoveOnChangeListener(OnTutoingActionChange, hand.handType);
        }

        private void OnTutoingActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue)
            {
                TutoComplete();
            }
        }

        public void TutoComplete()
        {
            Debug.Log("Tutoing");

            ControllerButtonHints.HideTextHint(hand, SteamVR_Input.actionsIn[2]);
            
        }

        
    }
}