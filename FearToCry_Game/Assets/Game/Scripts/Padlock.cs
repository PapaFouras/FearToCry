using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Padlock : MonoBehaviour
{
    [SerializeField]
    public PadlockRing[] padlockRings;

    public UnityEvent onChestOpen;

    public int[] digitCode;
    private void Awake() {
        digitCode = new int[padlockRings.Length];
    }
    private void Start()
    {
        OnPickUp();
    }

    public void OnPickUp()
    {
        foreach (var ring in padlockRings)
        {
            ring.EnableRingComponents(true);
        }
    }

    //public void OnDetachFromHand(){
    //    foreach (var ring in padlockRings){
    //        ring.EnableRingComponents(false);
    //    }
    //}

    public void OnNewDigit(){
        Debug.Log("OnNewDIgit");
        foreach (var ring in padlockRings){
            if(!ring.isCurrentDigitTheUnlockDigit()){
                return;
            }
        }
        onChestOpen?.Invoke();
    }
    
}
