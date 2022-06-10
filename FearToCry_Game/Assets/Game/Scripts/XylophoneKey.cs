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

    private void OnCollisionEnter(Collision other) {
        Debug.Log("colliding");
        Debug.Log("Gameobject colliding : " + other.gameObject.name);
        Debug.Log("Gameobject tag colliding : " + other.gameObject.tag);
         if(other.gameObject.CompareTag("XylophoneStick")){
            if(Vector2.Distance(other.gameObject.GetComponent<XylophoneStick>().sphereCenter.transform.position,other.GetContact(0).point) < other.gameObject.GetComponent<XylophoneStick>().sphereCollider.radius * 1.05f){
                Debug.Log("should play note: "+note);
                ps.Play();
            }
            
        }
    }

    

}
