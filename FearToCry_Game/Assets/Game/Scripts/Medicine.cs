using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "HeadCollider"){
            GameManager.instance.ChangeRoom(1);
        }
    }
}
