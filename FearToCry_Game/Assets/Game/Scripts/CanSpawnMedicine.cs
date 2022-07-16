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
	public class CanSpawnMedicine : MonoBehaviour
	{
        public SteamVR_ActionSet actionSet = SteamVR_Input.GetActionSet("default");

        public bool disableAllOtherActionSets = false;
        public int initialPriority = 0;


		public UnityEvent onHandHoverBegin;
		public UnityEvent onHandHoverEnd;



        private void Update() {
            Hand[] hands = GameManager.instance?._player.hands;
            if(hands == null){
                return;
            }

            Vector3 positionXZ = new Vector3(transform.position.x,0f,transform.position.z);
            Vector3 positionXZhand = new Vector3();
            foreach(Hand hand in hands){
                positionXZhand = new Vector3(hand.transform.position.x,0f,hand.transform.position.z);
                Vector2 forwardIn2D = new Vector2(transform.forward.x,transform.forward.z);
                Vector2 handToHips = new Vector2(transform.position.x - positionXZhand.x, transform.position.z - positionXZhand.z);
                Debug.DrawLine(transform.position,transform.position + new Vector3(forwardIn2D.x,0f,forwardIn2D.y), Color.green);
                Debug.DrawLine(transform.position,transform.position + new Vector3(handToHips.x,0f,handToHips.y),Color.magenta);
                
                if( Vector2.Dot(handToHips,forwardIn2D) > 0 ){
                     if(hand.name == "LeftHand" ){ 
                        actionSet.Activate(SteamVR_Input_Sources.LeftHand, initialPriority, disableAllOtherActionSets);
                    }
                    if(hand.name == "RightHand"){
                        actionSet.Activate(SteamVR_Input_Sources.RightHand, initialPriority, disableAllOtherActionSets);
                    }
                }
                else{
                    if(hand.name == "LeftHand"){
                        actionSet.Deactivate(SteamVR_Input_Sources.LeftHand);
                       // hand.GetComponent<TakingMedicine>().isGrabbing = false;
                        }
                    if(hand.name == "RightHand"){
                        actionSet.Deactivate(SteamVR_Input_Sources.RightHand);
                     //   hand.GetComponent<TakingMedicine>().isGrabbing = false;
                    }
                }
            }
        }
		// //-------------------------------------------------
		// private void OnHandHoverBegin(Hand hand)
		// {

		// 	onHandHoverBegin.Invoke();
        //     Debug.Log("hovering hand : " + hand.name);

        //     if(hand.name == "LeftHand" && source ==  SteamVR_Input_Sources.LeftHand){
                
        //         actionSet.Activate(source, initialPriority, disableAllOtherActionSets);
        //     }
        //     if(hand.name == "RightHand" && source == SteamVR_Input_Sources.RightHand){

        //         actionSet.Activate(source, initialPriority, disableAllOtherActionSets);
        //     }
		// }


		// //-------------------------------------------------
		// private void OnHandHoverEnd(Hand hand)
		// {
		// 	onHandHoverEnd.Invoke();
        //     Debug.Log("hovering hand : " + hand.name);
        //     if(hand.name == "LeftHand"){
        //             actionSet.Deactivate(SteamVR_Input_Sources.LeftHand);
        //             hand.GetComponent<TakingMedicine>().isGrabbing = false;
        //     }
        //     if(hand.name == "RightHand"){
        //         actionSet.Deactivate(SteamVR_Input_Sources.RightHand);
        //         hand.GetComponent<TakingMedicine>().isGrabbing = false;

        //     }
		// }


		//-------------------------------------------------
		
	}
}
