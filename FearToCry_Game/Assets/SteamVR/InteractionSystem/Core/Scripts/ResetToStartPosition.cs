using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetToStartPosition : MonoBehaviour
{
    private Vector3 startPosition;
    private void Awake() {
        startPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z);   
    }

    public void ResetToPosition(){
        transform.localPosition = startPosition;
    }
}
