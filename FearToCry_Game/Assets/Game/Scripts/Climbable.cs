//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Sends UnityEvents for basic hand interactions
//
//=============================================================================

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class Climbable : MonoBehaviour
	{
        public SteamVR_ActionSet actionSet = SteamVR_Input.GetActionSet("default");
        public GameObject player;

        private PlayerController playerController;

        private SteamVR_Input_Sources leftHandSource = SteamVR_Input_Sources.LeftHand;
        private SteamVR_Input_Sources rightHandSource = SteamVR_Input_Sources.RightHand;

        public bool disableAllOtherActionSets = false;
        public int initialPriority = 0;


		public UnityEvent onHandHoverBegin;
		public UnityEvent onHandHoverEnd;
		public UnityEvent onAttachedToHand;
		public UnityEvent onDetachedFromHand;

        private void Start() {
            playerController = player.GetComponent<PlayerController>();
        }

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
                if(playerController.climbingHand != hand){
                    //playerController.climbingHand = null;
                    actionSet.Deactivate(leftHandSource);
                }
                

            }
            if(hand.name == "RightHand"){
                if(playerController.climbingHand != hand){
                   // playerController.climbingHand = null;
                    actionSet.Deactivate(rightHandSource);

                }

            }

		}


		//-------------------------------------------------
		private void OnAttachedToHand( Hand hand )
		{
			onAttachedToHand.Invoke();
		}

        /*void LateUpdate()
        {
            if(playerController.climbingHand != null)
            {
                playerController.climbingHand.transform.position = this.transform.position;
            }
        }*/


		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			onDetachedFromHand.Invoke();
		}
	}
}
