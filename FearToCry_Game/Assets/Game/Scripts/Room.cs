using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Objects in room")]
    public GameObject ball;
    public GameObject cube;
    //public GameObject drawer;

    private List<GameObject> objectsInRoom = new List<GameObject>();

    private void Awake() {
        objectsInRoom.Clear();
        objectsInRoom.Add(ball);
        objectsInRoom.Add(cube);
    }

    public List<GameObject> GetObjectsInRoom(){
        
        return objectsInRoom;
    }
}
