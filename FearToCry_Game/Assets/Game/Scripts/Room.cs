using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Room : MonoBehaviour
{
    [Header("Objects in room")]
    public GameObject[] objects;

    [HideInInspector]
    public Vector3[] startPosition;

    [HideInInspector]
    public Vector3[] startRotation;
    //public GameObject drawer;

    private List<GameObject> objectsInRoom = new List<GameObject>();

    private void Awake() {

        objectsInRoom.Clear();
        startPosition = new Vector3[objects.Length];
        startRotation = new Vector3[objects.Length];
        for(int i = 0 ; i < objects.Length; i++){
            objectsInRoom.Add(objects[i]);
            startPosition[i] = objects[i].transform.localPosition;
            startRotation[i] = objects[i].transform.localEulerAngles;
        } 
    }

    public List<GameObject> GetObjectsInRoom(){
        
        return objectsInRoom;
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            SteamVR_Fade.View(Color.black,0f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            SteamVR_Fade.View(Color.clear,0f);
        }
    }
}
