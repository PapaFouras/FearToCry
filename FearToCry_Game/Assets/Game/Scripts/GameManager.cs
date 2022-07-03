using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;
using Valve.VR;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Rooms")]
    public Room _room1;
    public Room _room2;
    public Room _room3;

    private Room _currentRoom;

    public SteamVR_Fade fade;

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
             StartCoroutine(FadeOutFadeIn(.2f,1f,1.5f,()=>{
                _player.transform.position = room.transform.position + _relativeStartingPosition;
                _currentRoom = room;    
             }));
           
            return;
        }

        if(room == _currentRoom){
            return;
        }

        
        
        foreach (var hand in _player.GetComponent<Player>().hands)
        {
            if(hand.currentAttachedObject != null){
                hand.DetachObject(hand.currentAttachedObject);  
            }
        };
        List<GameObject> objectsOfCurrentRoom = _currentRoom.GetObjectsInRoom();
        List<GameObject> objectsOfNextRoom = room.GetObjectsInRoom();
        for(int i = 0; i<objectsOfNextRoom.Count; i++){
            objectsOfNextRoom[i].transform.localPosition = objectsOfCurrentRoom[i].transform.localPosition;
            objectsOfNextRoom[i].transform.rotation = objectsOfCurrentRoom[i].transform.rotation;
            if(objectsOfNextRoom[i].TryGetComponent<Rigidbody>(out Rigidbody rb1)){
                 if(objectsOfCurrentRoom[i].TryGetComponent<Rigidbody>(out Rigidbody rb2)){
                    rb1.velocity = rb2.velocity;
                 } 
            }
            objectsOfNextRoom[i].SetActive(objectsOfCurrentRoom[i].activeSelf);
            if(objectsOfCurrentRoom[i].TryGetComponent<Flammable>(out Flammable flammable)){
                if(flammable.isBurning){
                    objectsOfNextRoom[i].SetActive(false);
                }
            }

        }
        StartCoroutine(FadeOutFadeIn(.2f,.4F,1.5f,()=>{
             _player.transform.position = room.transform.position + _player.transform.position - _currentRoom.transform.position;
            _currentRoom = room;
        }));
        
    }

    IEnumerator FadeOutFadeIn(float fadeInDuration, float timeBetweenFadeInAndFadeOut,float fadeOutDuration, Action whenInBetween){
        SteamVR_Fade.View(Color.black,fadeInDuration);
        yield return new WaitForSeconds(fadeInDuration);
        whenInBetween?.Invoke();
        yield return new WaitForSeconds(timeBetweenFadeInAndFadeOut);
        SteamVR_Fade.View(Color.clear,fadeOutDuration);
    }


    public void ChangeRoom(int roomNumber){
        switch (roomNumber){
             case 1: 
                ChangeRoom(_room1);
                break;
            case 2: 
                ChangeRoom(_room2);
                break;
            case 3: 
                ChangeRoom(_room3);
                break;
             default : Debug.Log("room number not valid");
                break;
        }
        
    }
}
