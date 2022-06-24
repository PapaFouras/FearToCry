using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Padlock : MonoBehaviour
{
    [SerializeField]
    public PadlockRing[] padlockRings;

    private MeshCollider meshCollider;

    private ParticleSystem ps;

    public int[] digitCode;
    private void Awake() {
        meshCollider = GetComponent<MeshCollider>();
        digitCode = new int[padlockRings.Length];
        ps = GetComponent<ParticleSystem>();
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
        Debug.Log("OnNewDIgit");
        foreach (var ring in padlockRings){
            if(!ring.isCurrentDigitTheUnlockDigit()){
                return;
            }
        }
        ps.Play();
        Debug.Log("Digit Open !");
    }
    
}
