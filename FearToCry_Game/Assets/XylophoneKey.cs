using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XylophoneKey : MonoBehaviour
{
    public string note;
    public ParticleSystem ps;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "XylophoneStick"){
            Debug.Log("should play note: "+note);
            ps.Play();
        }
    }

}
