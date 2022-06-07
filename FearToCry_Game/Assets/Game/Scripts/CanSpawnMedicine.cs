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

        public SteamVR_Input_Sources source;

        public bool disableAllOtherActionSets = false;
        public int initialPriority = 0;


		public UnityEvent onHandHoverBegin;
		public UnityEvent onHandHoverEnd;

		//-------------------------------------------------
		private void OnHandHoverBegin(Hand hand)
		{

			onHandHoverBegin.Invoke();
            Debug.Log("hovering hand : " + hand.name);

            if(hand.name == "LeftHand" && source ==  SteamVR_Input_Sources.LeftHand){
                
                actionSet.Activate(source, initialPriority, disableAllOtherActionSets);
            }
            if(hand.name == "RightHand" && source == SteamVR_Input_Sources.RightHand){

                actionSet.Activate(source, initialPriority, disableAllOtherActionSets);
            }
		}


		//-------------------------------------------------
		private void OnHandHoverEnd(Hand hand)
		{
			onHandHoverEnd.Invoke();
            Debug.Log("hovering hand : " + hand.name);
            if(hand.name == "LeftHand"){
                    actionSet.Deactivate(SteamVR_Input_Sources.LeftHand);
                    hand.GetComponent<TakingMedicine>().isGrabbing = false;
            }
            if(hand.name == "RightHand"){
                actionSet.Deactivate(SteamVR_Input_Sources.RightHand);
                hand.GetComponent<TakingMedicine>().isGrabbing = false;

            }
		}


		//-------------------------------------------------
		
	}
}
