using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Valve.VR.InteractionSystem.Sample;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class ActivateActionSetOnTrigger : MonoBehaviour
	{
        public SteamVR_ActionSet actionSet = SteamVR_Input.GetActionSet("default");

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
                
                actionSet.Activate(SteamVR_Input_Sources.LeftHand, initialPriority, disableAllOtherActionSets);
            	Debug.Log("activating : " + actionSet.GetShortName() + " for " + hand.name);

            }
            if(hand.name == "RightHand"){

                actionSet.Activate(SteamVR_Input_Sources.RightHand, initialPriority, disableAllOtherActionSets);
            	Debug.Log("activating : " + actionSet.GetShortName() + " for " + hand.name);

            }
		}


		//-------------------------------------------------
		private void OnHandHoverEnd(Hand hand)
		{
			onHandHoverEnd.Invoke();
            Debug.Log("hovering hand : " + hand.name);

            if(hand.name == "LeftHand"){
                    actionSet.Deactivate(SteamVR_Input_Sources.LeftHand);
            	Debug.Log("dectivating : " + actionSet.GetShortName() + " for " + hand.name);

                
            }
            if(hand.name == "RightHand"){
                actionSet.Deactivate(SteamVR_Input_Sources.RightHand);
            	Debug.Log("dectivating : " + actionSet.GetShortName() + " for " + hand.name);

            }
		}


		//-------------------------------------------------
		
	}
}
