using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayFmodSoundAtDistance : MonoBehaviour
{
    Hand handLeft;
    Hand handRight;

    [SerializeField]
    public FMODUnity.EventReference fmodEvent;
    FMOD.Studio.EventInstance fmodEventInstance;


    [SerializeField]
    public const float distanceStartSound = .8f; // 50 cm
    
    private void Awake() {
       
    }
    
    // Start is called before the first frame update
    void Start()
    {
        handLeft = GameManager.instance._player.hands[0];
        handRight = GameManager.instance._player.hands[1];
        fmodEventInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        fmodEventInstance.start();
        fmodEventInstance.setParameterByName("VarRangeHand",0f);
    }

    // Update is called once per frame
    void Update()
    {
        float handLeftDistance = Vector3.Distance(gameObject.transform.position, handLeft.transform.position);
        float handRightDistance = Vector3.Distance(gameObject.transform.position, handRight.transform.position);
        float currentDistance = Mathf.Min(handLeftDistance,handRightDistance);

        if(currentDistance > distanceStartSound ){
            fmodEventInstance.setParameterByName("VarRangeHand",0f);
            return;
        }
        if(currentDistance <= distanceStartSound){
            float distanceInPercent =100 - ((currentDistance/distanceStartSound) * 100);
            fmodEventInstance.setParameterByName("VarRangeHand",distanceInPercent);
            return;
        }
    }
}
