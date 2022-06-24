using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AlarmClock : MonoBehaviour
{
    AudioSource soundClick;
    bool isPressed;
    void Start()
    {
        soundClick = GetComponent<AudioSource>();
        isPressed = false;
    }
    public void OnButtonDown(Hand fromHand)
        {
            if (!isPressed)
            {
                soundClick.Play();
                isPressed = true;
                Debug.Log("Stop Alarm");
                fromHand.TriggerHapticPulse(1000);
            }
        }

        public void OnButtonUp(Hand fromHand)
        {
            if (isPressed)
            {
                isPressed = false;
                Debug.Log("Reset Alarm");
            }
        }

}
