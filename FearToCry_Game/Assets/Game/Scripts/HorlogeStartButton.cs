

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HorlogeStartButton : MonoBehaviour
{

    //public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public HorlogeManager manager;
    GameObject presser;
    bool isPressed;
    bool isTictac;
    public Material matTictac;
    public Material matNotTictac;
    public GameObject boitier;

    public FMODUnity.EventReference Horloge_TicTac;
    FMOD.Studio.EventInstance horloge_TicTac;

    public FMODUnity.EventReference Horloge_Bouton;

    void Start()
    {

        GetComponent<Renderer>().material = matNotTictac;

        isPressed = false;
        isTictac = false;
    }

    public void Click(){
        //if (!isPressed)
        {
           // Debug.Log("CLICK : " + transform.localPosition);
            //transform.localPosition = new Vector3(0, 0.03f, 0);
           // Debug.Log("C After : " + transform.localPosition);
           // presser = other.gameObject;
            onPress.Invoke();
            FMODUnity.RuntimeManager.PlayOneShot(Horloge_Bouton, transform.position);
            isPressed = true;
            spawnSphere();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject == presser)
    //     {
    //         Debug.Log("R Bef : " + button.transform.localPosition);
    //         button.transform.localPosition = new Vector3(0, 0.15f, 0);
    //         onRelease.Invoke();
    //         isPressed = false;
    //         Debug.Log("RELEASE : " + button.transform.localPosition);
    //     }
    // }

    public void spawnSphere()
    {
        manager.Notify(this);
    }

    public void startTicTac()
    {
        isTictac = true;
        horloge_TicTac.start();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(horloge_TicTac, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }
    
    public void stopTicTac()
    {
        boitier.GetComponent<Renderer>().materials[1] = matNotTictac;
        GetComponent<Renderer>().material = matNotTictac;
        isTictac = false;
        horloge_TicTac.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Debug.Log("OK");
    }
}
