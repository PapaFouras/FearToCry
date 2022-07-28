using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetToStartPosition : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    public float transitionDuration = 1;


    private void Awake() {
        startPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        startRotation = transform.rotation;
    }

    public void ResetStartPosition()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        startRotation = transform.rotation;
    }

    public void ResetToPosition(){
        StartCoroutine(GoBackToStartPosition());
    }
    IEnumerator GoBackToStartPosition()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
        float tValue = 0;
        while (tValue <= 1)
        {
            transform.position = Vector3.Lerp(lastPosition, startPosition, tValue);
            transform.rotation = Quaternion.Lerp(lastRotation, startRotation, tValue);
            yield return new WaitForEndOfFrame();
            tValue += Time.deltaTime / transitionDuration;
        }

    }
}
