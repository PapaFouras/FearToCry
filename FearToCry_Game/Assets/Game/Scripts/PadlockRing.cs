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
    private int unlockDigit = 2;
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
        float newRotationY = 0f;
        float yRot = transform.localEulerAngles.y;

        int nbChoice = 6;

        float smallestStep = ((1f/(float)nbChoice)*360);

        int yRotInt = Mathf.RoundToInt(yRot);
        int smallestStepInt = Mathf.RoundToInt(smallestStep);

        float floorStep,ceilStep;

        int newDigit = (yRotInt / smallestStepInt);
        floorStep = newDigit * smallestStep;
        ceilStep = (newDigit + 1) * smallestStep;

        float distanceFloorStep,distanceCeilStep;
        distanceFloorStep = Mathf.Abs(floorStep - yRot);
        distanceCeilStep = Mathf.Abs(ceilStep - yRot);

        if(distanceFloorStep < distanceCeilStep){
            newRotationY = floorStep;
        }
        else{
            newRotationY = ceilStep;
            newDigit++;
        }
        Debug.Log(yRot + " divided by " + smallestStep +" will become " +newRotationY);
        currentDigit = newDigit;
        transform.localEulerAngles = new Vector3(0f,newRotationY,0f);
    }
}
