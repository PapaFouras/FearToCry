

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HorlogeStartButton : MonoBehaviour
{
    public GameObject button;
    public AudioSource soundClick;
    public AudioSource soundTicTac;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public HorlogeManager manager;
    GameObject presser;
    bool isPressed;
    bool isTictac;
    public Material matTictac;
    public Material matNotTictac;

    void Start()
    {
        button.GetComponent<Renderer>().material = matNotTictac;
        isPressed = false;
        isTictac = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            Debug.Log("CLICK : " + button.transform.localPosition);
            button.transform.localPosition = new Vector3(0, 0.03f, 0);
            Debug.Log("C After : " + button.transform.localPosition);
            presser = other.gameObject;
            onPress.Invoke();
            soundClick.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            Debug.Log("R Bef : " + button.transform.localPosition);
            button.transform.localPosition = new Vector3(0, 0.15f, 0);
            onRelease.Invoke();
            isPressed = false;
            Debug.Log("RELEASE : " + button.transform.localPosition);
        }
    }

    public void spawnSphere()
    {
        manager.Noitfy(this);
    }

    public void startTicTac()
    {
        button.GetComponent<Renderer>().material = matTictac;
        isTictac = true;
        soundTicTac.Play();
    }
    
    public void stopTicTac()
    {
        button.GetComponent<Renderer>().material = matNotTictac;
        isTictac = false;
        soundTicTac.Stop();
        Debug.Log("OK");
    }
}
