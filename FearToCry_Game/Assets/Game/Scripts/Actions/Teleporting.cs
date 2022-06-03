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
        private GameObject room1;
       
        [SerializeField]
        private GameObject room2;

        [SerializeField]

        private GameObject player;

        private int roomNb = 1;


        


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
            if(roomNb%3 == 0){
                GameManager.instance.ChangeRoom(GameManager.instance._room1);
            }
            else if(roomNb%3 == 1){
                GameManager.instance.ChangeRoom(GameManager.instance._room2);
            }
            else if (roomNb%3 == 2){
                GameManager.instance.ChangeRoom(GameManager.instance._room3);
            }
            roomNb ++;
        }

        
    }
}