using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Rooms")]
    public Room _room1;
    public Room _room2;
    public Room _room3;

    private Room _currentRoom;

    [Header("Player")]
    public GameObject _player;

   
    public enum RoomName{
        Room1,
        Room2,
        Room3
    }
public static GameManager instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("Il y a plus d'une instance de GameManager dans la scène");
            return;
        }
        instance = this;
    
    }
    
    [SerializeField]
    [Header("Starting room")]
    private RoomName _roomName = RoomName.Room1;
    [SerializeField]
    private Vector3 _relativeStartingPosition;
    void Start()
    {
        switch(_roomName){
            case RoomName.Room1 : ChangeRoom(_room1);
            break;
            case RoomName.Room2 : ChangeRoom(_room2);
            break; 
            case RoomName.Room3 : ChangeRoom(_room3);
            break;
        }        
    }
    public void ChangeRoom(Room room){

        
         if(_currentRoom == null){
             //Set position
            _player.transform.position = room.transform.position + _relativeStartingPosition;
            _currentRoom = room;
            return;
        }

        List<GameObject> objectsOfCurrentRoom = _currentRoom.GetObjectsInRoom();
        List<GameObject> objectsOfNextRoom = room.GetObjectsInRoom();
        for(int i = 0; i<objectsOfNextRoom.Count; i++){
            objectsOfNextRoom[i].transform.localPosition = objectsOfCurrentRoom[i].transform.localPosition;
            objectsOfNextRoom[i].transform.rotation = objectsOfCurrentRoom[i].transform.rotation;
            objectsOfNextRoom[i].GetComponent<Rigidbody>().velocity = objectsOfCurrentRoom[i].GetComponent<Rigidbody>().velocity;

        }

         _player.transform.position = room.transform.position + _player.transform.position - _currentRoom.transform.position;
        _currentRoom = room;
    }
}
