//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Sends UnityEvents for basic hand interactions
//
//=============================================================================

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Valve.VR.InteractionSystem.Sample;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class CanSpawnMedicine : MonoBehaviour
	{
        public SteamVR_ActionSet actionSet = SteamVR_Input.GetActionSet("default");

        private SteamVR_Input_Sources leftHandSource = SteamVR_Input_Sources.LeftHand;
        private SteamVR_Input_Sources rightHandSource = SteamVR_Input_Sources.RightHand;

        public bool disableAllOtherActionSets = false;
        public int initialPriority = 0;


		public UnityEvent onHandHoverBegin;
		public UnityEvent onHandHoverEnd;

		//-------------------------------------------------
		private void OnHandHoverBegin(Hand hand)
		{

			onHandHoverBegin.Invoke();
            Debug.Log("hovering hand : " + hand.name);
            if(hand.name == "LeftHand"){
                
                actionSet.Activate(leftHandSource, initialPriority, disableAllOtherActionSets);

            }
            if(hand.name == "RightHand"){

                actionSet.Activate(rightHandSource, initialPriority, disableAllOtherActionSets);
            }
            

		}


		//-------------------------------------------------
		private void OnHandHoverEnd(Hand hand)
		{
			onHandHoverEnd.Invoke();
            Debug.Log("hovering hand : " + hand.name);
            if(hand.name == "LeftHand"){
                    actionSet.Deactivate(leftHandSource);
                    hand.GetComponent<TakingMedicine>().isGrabbing = false;
            }
            if(hand.name == "RightHand"){
                actionSet.Deactivate(rightHandSource);
                hand.GetComponent<TakingMedicine>().isGrabbing = false;

            }
		}


		//-------------------------------------------------
		
	}
}
