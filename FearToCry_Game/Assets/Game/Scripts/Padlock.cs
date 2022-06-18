using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Padlock : MonoBehaviour
{
    [SerializeField]
    public PadlockRing[] padlockRings;

    private MeshCollider meshCollider;

    public ParticleSystem ps;

    public int[] digitCode;
    private void Awake() {
        meshCollider = GetComponent<MeshCollider>();
        digitCode = new int[padlockRings.Length];
    }

    public void OnPickUp(){
        meshCollider.enabled = false;
        foreach (var ring in padlockRings){
            ring.EnableRingComponents(true);
        }
    }

    public void OnDetachFromHand(){
        meshCollider.enabled = true;
        foreach (var ring in padlockRings){
            ring.EnableRingComponents(false);
        }
    }

    public void OnNewDigit(){
        foreach (var ring in padlockRings){
            if(!ring.isCurrentDigitTheUnlockDigit()){
                return;
            }
        }
        ps.Play();
        Debug.Log("Digit Open !");
    }
    
}
