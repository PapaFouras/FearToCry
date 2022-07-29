using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PadlockRing : MonoBehaviour
{
    private Interactable interactable;
    private Collider boxCollider;
    private CircularDrive circularDrive;
    private LinearMapping linearMapping;

    private int currentDigit = 0;

    [SerializeField]
    private int unlockDigit = 0;

    [SerializeField]
    private int nbDigit = 26;
    private void Awake() {
        interactable = GetComponent<Interactable>();
        boxCollider = GetComponent<Collider>();  
        circularDrive = GetComponent<CircularDrive>();
        linearMapping = GetComponent<LinearMapping>();
    }
    public void EnableRingComponents(bool enable = true)
    {
        interactable.enabled = enable;
        circularDrive.enabled = enable;
        boxCollider.enabled = enable;
    }

    public bool isCurrentDigitTheUnlockDigit(){
        return currentDigit == unlockDigit;
    }

    public void OnDetachFromHand(){
        Debug.Log("Hello rotation x = " + transform.localEulerAngles.x);
        float newRotationX = 0f;
        
        float xRot = GetCircularLinearValue();


        float smallestStep = (1f/(float)nbDigit);


        float floorStep,ceilStep;

        int newDigit = Mathf.RoundToInt(xRot / smallestStep);
        floorStep = newDigit * (smallestStep*360);
        ceilStep = (newDigit + 1) * (smallestStep * 360);

        float distanceFloorStep,distanceCeilStep;
        distanceFloorStep = Mathf.Abs(floorStep - xRot * 360);
        distanceCeilStep = Mathf.Abs(ceilStep - xRot * 360);

        if(distanceFloorStep < distanceCeilStep){
            newRotationX = floorStep;
        }
        else{
            newRotationX = ceilStep;
            newDigit++;
        }
        //newRotationX = (newRotationX < 0) ? newRotationX + 180 : newRotationX;
        Debug.Log(xRot + " divided by " + smallestStep +" will become " + newRotationX);
        currentDigit = newDigit%nbDigit;
        Debug.Log("New digit = " + currentDigit);

        transform.localEulerAngles = new Vector3(newRotationX, 0f,0f);
        GetComponent<LinearMapping>().value = newRotationX / 360f;

    }
    public float GetCircularLinearValue()
    {

        return GetComponent<LinearMapping>().value;
    }
}
