using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ParticleSystem))]
public class Flammable : MonoBehaviour
{
    private ParticleSystem ps;
    private void Awake() {
        ps = GetComponent<ParticleSystem>();
    }
    public void StartBurning(){
        gameObject.tag = "Burning";
        ps.Play();

    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("TAG:: " + other.gameObject.tag);
        if(other.gameObject.CompareTag("Burning")){
            StartBurning();
        }
    }
}
