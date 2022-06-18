using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class MainRushHourCar : MonoBehaviour
{
    ParticleSystem ps;

    private void Awake() {
        ps = GetComponent<ParticleSystem>();
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "endingCube"){
            Debug.Log("Main Car has arrived ! ");
            ps.Play();
        }
    }
}
