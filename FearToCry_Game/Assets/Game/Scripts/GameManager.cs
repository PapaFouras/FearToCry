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
    public Room _room4;

    public InitManager tutoInitManager;
    public Transform startRoom1;
    public Transform porteRoom1;
    public Transform stellaRoomReel;

    private Room _currentRoom;

    public SteamVR_Fade fade;

    [Header("Player")]
    public Player _player;

    public FMODUnity.EventReference Transition_IntoFolie;

    private VoiceLineManager voiceLineManager;

    public enum RoomName{
        Normal,
        Folie,
        Hopital,
        Tuto
    }
public static GameManager instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("Il y a plus d'une instance de GameManager dans la scène");
            return;
        }
        instance = this;
        _player = Player.instance;
        voiceLineManager = GetComponent<VoiceLineManager>();
    
    }
    
    [SerializeField]
    [Header("Starting room")]
    private RoomName _roomName = RoomName.Normal;
    [SerializeField]
    public Vector3 _relativeStartingPosition;
    void Start()
    {
        switch(_roomName){
            case RoomName.Normal : ChangeRoom(_room1);
            break;
            case RoomName.Folie : ChangeRoom(_room2);
            break; 
            case RoomName.Hopital : ChangeRoom(_room3);
            break;
            case RoomName.Tuto: ChangeRoom(_room4);
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
        if (_currentRoom == _room4)
        {
            return RoomName.Tuto;
        }
        return RoomName.Normal;

    }

    bool roomOneVisited = false;
    bool roomTwoVisited = false;


    public void ChangeRoom(Room room){
        if(_currentRoom == _room4)
        {
            _relativeStartingPosition = _room1.transform.position - startRoom1.position;
        }
        if (room == _room4)
        {
            _player.transform.position = tutoInitManager.startPoint.position;
            _player.transform.LookAt(tutoInitManager.StartLookAt);
            _player.transform.localEulerAngles = new Vector3(0, _player.transform.localEulerAngles.y, _player.transform.localEulerAngles.z);
            _currentRoom = _room4;
            return;
        }
        
        if (_currentRoom == null){
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

        List<Hand.AttachedObject>[] attachedGameObjects = new List<Hand.AttachedObject>[_player.hands.Length];
        for(int j = 0; j< _player.hands.Length; j++)
        {
            Debug.Log(attachedGameObjects[j] == null);
            attachedGameObjects[j] = new List<Hand.AttachedObject>();
            for(int l = 0; l< _player.hands[j].AttachedObjects.Count; l++)
            {
                attachedGameObjects[j].Add(_player.hands[j].AttachedObjects[l]);
            }
        }
        List<GameObject> objectsOfCurrentRoom = _currentRoom.GetObjectsInRoom();
        List<GameObject> objectsOfNextRoom = room.GetObjectsInRoom();
        if (_currentRoom != _room4)
        {
            for (int i = 0; i < objectsOfNextRoom.Count; i++)
            {
                objectsOfNextRoom[i].transform.position = room.transform.position + (objectsOfCurrentRoom[i].transform.position - _currentRoom.transform.position);
                objectsOfNextRoom[i].transform.rotation = objectsOfCurrentRoom[i].transform.rotation;

                if (objectsOfNextRoom[i].transform.position.y < -1)
                {
                    objectsOfNextRoom[i].transform.localPosition += new Vector3(0, 1, 0);
                }
                if (objectsOfNextRoom[i].TryGetComponent<Rigidbody>(out Rigidbody rb1))
                {
                    if (objectsOfCurrentRoom[i].TryGetComponent<Rigidbody>(out Rigidbody rb2))
                    {
                        rb1.velocity = rb2.velocity;
                    }
                }
                objectsOfNextRoom[i].SetActive(objectsOfCurrentRoom[i].activeSelf);
                if (objectsOfCurrentRoom[i].TryGetComponent<Flammable>(out Flammable flammable))
                {
                    if (flammable.isBurning)
                    {
                        objectsOfNextRoom[i].SetActive(false);
                    }
                }

            }
        }
       
        
        StartCoroutine(FadeOutFadeIn(.2f,.4F,1.5f,()=>{
            foreach (var hand in _player.hands)
            {
                hand.DetachAllObjects();
            }
            if(_currentRoom != _room4)
            {
                _player.transform.position = room.transform.position + _player.transform.position - _currentRoom.transform.position;
                
            }
            if(_currentRoom == _room4)
            {
                 _relativeStartingPosition = startRoom1.position - _room1.transform.position ;
                _player.transform.position = room.transform.position + _relativeStartingPosition;
                _player.transform.LookAt(porteRoom1);
                _player.transform.eulerAngles = new Vector3(0f, _player.transform.eulerAngles.y, _player.transform.eulerAngles.z);
            }
            if(room == _room1)
            {
                if (!roomOneVisited)
                {
                    IEnumerator PlayRDVMedecin()
                    {
                        yield return new WaitForSeconds(2f);
                        voiceLineManager.PlayRDVMedecin();
                    }
                    StartCoroutine(PlayRDVMedecin());
                    roomOneVisited = true;
                    IEnumerator PlayRemainder()
                    {
                        yield return new WaitForSeconds(60);
                        if(_currentRoom == _room1)
                        {
                            voiceLineManager.PlayFautVrmt();
                        }
                    }
                    StartCoroutine(PlayRemainder());
                }
            }
            if (room == _room2)
            {
                if (!roomTwoVisited)
                {
                    IEnumerator PlayOuSuisje()
                    {
                        yield return new WaitForSeconds(2f);
                        voiceLineManager.PlayOuSuisJe();
                    }
                    StartCoroutine(PlayOuSuisje());
                }
            }
            if (room == _room3)
            {

                _player.transform.LookAt(stellaRoomReel);
                _player.transform.localEulerAngles = new Vector3(0, _player.transform.localEulerAngles.y, _player.transform.localEulerAngles.z);


            }
            bool ok = false;
            if(_currentRoom != _room4)
            {
                ok = true;
            }
            _currentRoom = room;

            if (!ok)
            {
                return;
            }
            for(int j = 0; j< _player.hands.Length; j++)
            {
                foreach(var attachedObject in attachedGameObjects[j])
                {
                    for (int k = 0; k < objectsOfCurrentRoom.Count; k++)
                    {
                        if (attachedObject.attachedObject == objectsOfCurrentRoom[k])
                        {
                            _player.hands[j].AttachObject(objectsOfNextRoom[k], GrabTypes.Pinch);
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
            case 4:
                ChangeRoom(_room4);
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
