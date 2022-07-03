using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Room : MonoBehaviour
{
    [Header("Objects in room")]
    public GameObject[] objects;
    //public GameObject drawer;

    private List<GameObject> objectsInRoom = new List<GameObject>();

    private void Awake() {
        objectsInRoom.Clear();
        for(int i = 0 ; i < objects.Length; i++){
            objectsInRoom.Add(objects[i]);
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
