
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;



public class InitManager : MonoBehaviour
{
    public Transform startDoor;
    public Transform startPoint;
    public Transform StartLookAt;

    public PlayableDirector playableDirector2;

    public GameObject[] objectsToDestroy;
    void Start()
    {
        
       
    }

    public void StartTuto()
    {

    }
    bool hasPlayed = false;
    public void PlaySequence2()
    {
        if (!hasPlayed)
        {
            playableDirector2.Play();
            hasPlayed = true;
        }
    }

    public void SkipTuto()
    {
        GameManager.instance.ChangeRoom(1);
    }
    public void ShowDebugHint()
    {
        foreach(var hand in Player.instance.hands)
        {
            //ControllerButtonHints.ShowButtonHint(hand, SteamVR_Input.actionsIn[2]);
            ControllerButtonHints.ShowTextHint(hand, SteamVR_Input.actionsIn[2], "Grab object with this button");
        }
    }

    public void HideDebugHint()
    {
        foreach (var hand in Player.instance.hands)
        {
            //ControllerButtonHints.ShowButtonHint(hand, SteamVR_Input.actionsIn[2]);
            ControllerButtonHints.HideTextHint(hand, SteamVR_Input.actionsIn[2]);
        }
    }


}
