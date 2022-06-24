using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHourCar : MonoBehaviour
{
     public Transform target;
    Rigidbody rb;

    public enum Axis{X,Y,Z};

    
    public Axis axis = Axis.X;

    private Vector3 startPosition;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.localPosition;
    }

    private void FixedUpdate() {
        
        Vector3 direction = Vector3.zero;
        Vector3 currentPosition = transform.localPosition;
        if(axis == Axis.X){
            direction = Vector3.right;
            transform.localPosition = new Vector3(currentPosition.x,target.transform.localPosition.y,target.transform.localPosition.z);
        }
        if(axis == Axis.Y){
            direction = Vector3.up;
            transform.localPosition = new Vector3(target.transform.localPosition.x,currentPosition.y,target.transform.localPosition.z);

        } 

        if(axis == Axis.Z){
            direction = Vector3.forward;
            transform.localPosition = new Vector3(target.transform.localPosition.x,target.transform.localPosition.y,currentPosition.z);
        }
        transform.eulerAngles = target.eulerAngles;


        if(Vector3.Distance(transform.localPosition,target.localPosition) < 0.01){
            rb.velocity = Vector3.zero;
            transform.localPosition = target.localPosition;
            return;
        }
       direction = transform.TransformVector(direction);
        Vector3 localToWorldForce = Vector3.Project(target.position-transform.position,direction);
        //rb.AddRelativeForce(localToWorldForce);
        rb.AddForce(localToWorldForce.normalized, ForceMode.VelocityChange);
        //  if(axis == Axis.X){
        //     direction = Vector3.right;
        //     transform.localPosition = new Vector3(currentPosition.x,target.transform.localPosition.y,target.transform.localPosition.z);
        // }
        // if(axis == Axis.Y){
        //     direction = Vector3.up;
        //     transform.localPosition = new Vector3(target.transform.localPosition.x,currentPosition.y,target.transform.localPosition.z);

        // } 

        // if(axis == Axis.Z){
        //     direction = Vector3.forward;
        //     transform.localPosition = new Vector3(target.transform.localPosition.x,target.transform.localPosition.y,currentPosition.z);
        // }
            // Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            // if(axis == Axis.X){
            //     localVelocity.y = 0;
            //     localVelocity.z = 0;
            //     rb.velocity = transform.TransformDirection(localVelocity);
            // }
            // if(axis == Axis.Y){
            //     localVelocity.x = 0;
            //     localVelocity.z = 0;
            //     rb.velocity = transform.TransformDirection(localVelocity);
            // }
            // if(axis == Axis.Z){
            //     localVelocity.x = 0;
            //     localVelocity.y = 0;
            //     rb.velocity = transform.TransformDirection(localVelocity);
            // } 
        }

        // private void OnCollisionEnter(Collision other) {
        //     Debug.Log(gameObject.name + " colliding with " + other.gameObject.name);
        // }

        // private void OnCollisionStay(Collision other) {
        //     Debug.Log(gameObject.name + " still colliding with " + other.gameObject.name);
            
        // }
    
}
