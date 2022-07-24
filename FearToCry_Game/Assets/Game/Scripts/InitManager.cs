
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;



public class InitManager : MonoBehaviour
{

    public GameObject[] objectsToDestroy;
    void Start()
    {
        
       
    }

    public void StartTuto()
    {

    }

    public void SkipTuto()
    {
        Destroy(Player.instance.gameObject);
        for(int i = 0;i< objectsToDestroy.Length; i++)
        {
            Destroy(objectsToDestroy[i]);
        }
        SceneManager.LoadScene("S_Inté_Orso");
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
