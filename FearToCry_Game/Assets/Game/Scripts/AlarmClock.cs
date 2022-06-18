using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AlarmClock : MonoBehaviour
{
    private bool isRinging= false;
    public bool isChecked = false;

    public AlarmManager alarmManager;

     public void OnButtonDown(Hand fromHand)
        {
            Debug.Log("Stop Alarm");
            if(isRinging){
                isRinging = false;
                fromHand.TriggerHapticPulse(1000);
                alarmManager.SetAlarm(this);
                isChecked = true;
            }
        }

        public void OnButtonUp(Hand fromHand)
        {
            Debug.Log("Reset Alarm");
        }

        public void StartAlarm(){
            isRinging = true;
        }

}
