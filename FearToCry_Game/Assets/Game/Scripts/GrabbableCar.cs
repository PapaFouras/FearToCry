using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class GrabbableCar : MonoBehaviour
{
    public Transform car;
 public void OnDetached(){
     StartCoroutine(SetTransformPosition());
   
 }

 IEnumerator SetTransformPosition(){
     yield return new WaitForSeconds(.1f);
     transform.position = car.position;
     LinearDrive ld = GetComponent<LinearDrive>();
     float totalDistance = Vector3.Distance(ld.startPosition.position,ld.endPosition.position);
     float currentDistance = Vector3.Distance(ld.startPosition.position, car.transform.position);
     float value = currentDistance/totalDistance;
     GetComponent<LinearMapping>().value = value;

 }
}
