using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AlarmClock : MonoBehaviour
{
     public void OnButtonDown(Hand fromHand)
        {
            Debug.Log("Stop Alarm");
            fromHand.TriggerHapticPulse(1000);
        }

        public void OnButtonUp(Hand fromHand)
        {
            Debug.Log("Reset Alarm");
        }

}
