using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class OnTriggerAction : MonoBehaviour
{
    public UnityEvent StartEvent;

    public UnityEvent TriggerEnterEvent;
    public UnityEvent TriggerStayEvent;
    public UnityEvent TriggerExitEvent;

    public UnityEvent OnPlayerGaze;


    private Collider collider;

    void Awake()
    {
        collider = GetComponent<Collider>();
    }

    void Start()
    {
        if(!collider.isTrigger)
            Debug.LogWarningFormat("Collider of {0} not set as Trigger. Events will not work.", gameObject.name);

        if(StartEvent != null) StartEvent.Invoke();
    }

    void Update()
    {
        if(OnPlayerGaze != null)
        {
            Ray playerGaze = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if(collider.Raycast(playerGaze, out hit, Mathf.Infinity))
            {
                OnPlayerGaze.Invoke();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print("triggered by " + other.gameObject.name);
        if(TriggerEnterEvent != null) TriggerEnterEvent.Invoke();
    }

    void OnTriggerStay()
    {
        if(TriggerStayEvent != null) TriggerStayEvent.Invoke();
    }

    void OnTriggerExit()
    {
        if(TriggerExitEvent != null) TriggerExitEvent.Invoke();
    }
}
