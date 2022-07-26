using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PadlockRing : MonoBehaviour
{
    private Interactable interactable;
    private MeshCollider meshCollider;
    private CircularDrive circularDrive;
    private LinearMapping linearMapping;

    private int currentDigit = 0;

    [SerializeField]
    private int unlockDigit = 0;

    [SerializeField]
    private int nbDigit = 2;
    private void Awake() {
        interactable = GetComponent<Interactable>();
        meshCollider = GetComponent<MeshCollider>();  
        circularDrive = GetComponent<CircularDrive>();
        linearMapping = GetComponent<LinearMapping>();
    }
    public void EnableRingComponents(bool enable = true){
        interactable.enabled = enable;
        circularDrive.enabled = enable;
        meshCollider.enabled = enable;
    }

    public bool isCurrentDigitTheUnlockDigit(){
        return currentDigit == unlockDigit;
    }

    public void OnDetachFromHand(){
        Debug.Log("Hello");
        float newRotationX = 0f;
        float xRot = transform.localEulerAngles.x;

        float smallestStep = ((1f/(float)nbDigit)*360);

        int xRotInt = Mathf.RoundToInt(xRot);
        int smallestStepInt = Mathf.RoundToInt(smallestStep);

        float floorStep,ceilStep;

        int newDigit = (xRotInt / smallestStepInt);
        floorStep = newDigit * smallestStep;
        ceilStep = (newDigit + 1) * smallestStep;

        float distanceFloorStep,distanceCeilStep;
        distanceFloorStep = Mathf.Abs(floorStep - xRot);
        distanceCeilStep = Mathf.Abs(ceilStep - xRot);

        if(distanceFloorStep < distanceCeilStep){
            newRotationX = floorStep;
        }
        else{
            newRotationX = ceilStep;
            newDigit++;
        }
        Debug.Log(xRot + " divided by " + smallestStep +" will become " +newRotationX);
        currentDigit = newDigit%nbDigit;
        Debug.Log("New digit = " + currentDigit);

        transform.localEulerAngles = new Vector3(newRotationX, 0f,0f);
    }
}
