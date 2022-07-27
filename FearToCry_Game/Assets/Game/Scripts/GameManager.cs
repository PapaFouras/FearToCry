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
    public Player _player;

    public FMODUnity.EventReference Transition_IntoFolie;

    public enum RoomName{
        Normal,
        Folie,
        Hopital
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
    private RoomName _roomName = RoomName.Normal;
    [SerializeField]
    private Vector3 _relativeStartingPosition;
    void Start()
    {
        switch(_roomName){
            case RoomName.Normal : ChangeRoom(_room1);
            break;
            case RoomName.Folie : ChangeRoom(_room2);
            break; 
            case RoomName.Hopital : ChangeRoom(_room3);
            break;
        }        
    }

    public RoomName GetCurrentRoomEnum()
    {
        if(_currentRoom == _room1)
        {
            return RoomName.Normal;
        }
        if (_currentRoom == _room2)
        {
            return RoomName.Folie;
        }
        if (_currentRoom == _room3)
        {
            return RoomName.Hopital;
        }
        return RoomName.Normal;

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

        List<Hand.AttachedObject>[] attachedGameObjects = new List<Hand.AttachedObject>[_player.GetComponent<Player>().hands.Length];
        for(int j = 0; j< _player.GetComponent<Player>().hands.Length; j++)
        {
            Debug.Log(attachedGameObjects[j] == null);
            attachedGameObjects[j] = new List<Hand.AttachedObject>();
            for(int l = 0; l< _player.GetComponent<Player>().hands[j].AttachedObjects.Count; l++)
            {
                attachedGameObjects[j].Add(_player.GetComponent<Player>().hands[j].AttachedObjects[l]);
            }
        }

        
        List<GameObject> objectsOfCurrentRoom = _currentRoom.GetObjectsInRoom();
        List<GameObject> objectsOfNextRoom = room.GetObjectsInRoom();
        for(int i = 0; i<objectsOfNextRoom.Count; i++){
            objectsOfNextRoom[i].transform.position = room.transform.position +  (objectsOfCurrentRoom[i].transform.position - _currentRoom.transform.position);
            objectsOfNextRoom[i].transform.rotation = objectsOfCurrentRoom[i].transform.rotation;

            if(objectsOfNextRoom[i].transform.position.y < -1 ){
                objectsOfNextRoom[i].transform.localPosition += new Vector3(0,1,0);
            }
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
            foreach (var hand in _player.GetComponent<Player>().hands)
            {
                hand.DetachAllObjects();
            }
            _player.transform.position = room.transform.position + _player.transform.position - _currentRoom.transform.position;
            _currentRoom = room;
            for(int j = 0; j< _player.GetComponent<Player>().hands.Length; j++)
            {
                foreach(var attachedObject in attachedGameObjects[j])
                {
                    for (int k = 0; k < objectsOfCurrentRoom.Count; k++)
                    {
                        if (attachedObject.attachedObject == objectsOfCurrentRoom[k])
                        {
                            _player.GetComponent<Player>().hands[j].AttachObject(objectsOfNextRoom[k], GrabTypes.Pinch);
                        }
                    }
                    
                }
                
            }
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
                //FMODUnity.RuntimeManager.PlayOneShot(Transition_IntoFolie, transform.position);
                ChangeRoom(_room2);
                break;
            case 3: 
                ChangeRoom(_room3);
                break;
             default : Debug.Log("room number not valid");
                break;
        }
        
    }

    public void ChangeRoomAfterSmallDelay(int roomNumber)
    {
        StartCoroutine(ChangeRoomAfterDelay(roomNumber));
    }

    private IEnumerator ChangeRoomAfterDelay(int room)
    {
        yield return new WaitForSeconds(.2f);
        ChangeRoom(room);
    }
}
