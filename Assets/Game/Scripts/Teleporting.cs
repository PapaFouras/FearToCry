//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class Teleporting : MonoBehaviour
    {
        public SteamVR_Action_Boolean teleportAction;

        public Hand hand;

        [SerializeField]
        private GameObject plane1;
       
        [SerializeField]
        private GameObject plane2;

        [SerializeField]

        private GameObject player;

        


        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (teleportAction == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No plant action assigned", this);
                return;
            }

            teleportAction.AddOnChangeListener(OnTeleportActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (teleportAction != null)
                teleportAction.RemoveOnChangeListener(OnTeleportActionChange, hand.handType);
        }

        private void OnTeleportActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue)
            {
                Teleport();
            }
        }

        public void Teleport()
        {
            var dist1 = Vector3.Distance(player.transform.position,plane1.transform.position);
            var dist2 = Vector3.Distance(player.transform.position,plane2.transform.position);
           if(dist1 < dist2){
               player.transform.position = plane2.transform.position;
           }
           else{
               player.transform.position = plane1.transform.position;
           }
        }

        
    }
}