using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;
using UnityEngine.ProBuilder;

[RequireComponent(typeof (Interactable))]
public class Bottle : MonoBehaviour
{
    VelocityEstimator ve;

    public GameObject brokenBottlePrefab;

    private Hand _hand = null;
    private void Awake()
    {
        ve = GetComponent<VelocityEstimator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        float velocity = ve.GetVelocityEstimate().magnitude;
        Debug.Log("Ca a touché !!! : " + velocity);
        if (velocity >= 1f)
        {
            BreakBottle();
        }
    }

    private void OnAttachedToHand( Hand hand )
    {
        _hand = hand;
    }


	//-------------------------------------------------
    private void OnDetachedFromHand( Hand hand )
    {
       hand = null;
    }

    private void BreakBottle(){
        Debug.Log("Ca va casser !");
        Hand currentHand = _hand;

        //_hand.DetachObject(gameObject);
        GameObject go = Instantiate(brokenBottlePrefab,currentHand.transform.position,currentHand.transform.rotation);
        gameObject.SetActive(false);
        //currentHand.AttachObject(go,GrabTypes.Scripted);



    }

}
