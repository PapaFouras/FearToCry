using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableHandler : MonoBehaviour
{
    Transform parent;

    private void Awake() {
        parent = transform.parent;
    }
 public void OnDetached(){
     transform.parent = parent;
     transform.localEulerAngles = Vector3.zero;
     transform.localPosition = Vector3.zero;
     transform.localScale = Vector3.zero;
 }
}
