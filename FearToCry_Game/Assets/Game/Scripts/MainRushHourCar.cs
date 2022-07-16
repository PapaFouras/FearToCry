using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class MainRushHourCar : MonoBehaviour
{
    ParticleSystem ps;

    public Transform endingTransform;
    public GameObject bottleGO;

    private void Awake() {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(Vector3.Distance(endingTransform.position,transform.position) < .1f)
        {
            ps.Play();
            
            bottleGO.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
