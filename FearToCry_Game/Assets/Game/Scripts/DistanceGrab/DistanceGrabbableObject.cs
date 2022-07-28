using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceGrabbableObject : MonoBehaviour
{
    // Allows distance grab of the object if true
    public bool isDistGrabbable;

    // Check if the object should be highlighted due to player's hand pointing at it
    bool isHighlighted = false;

    // If the object uses linear mapping
    [HideInInspector] public bool usesLinearMapping;

    // Mesh renderer on the Highlighter child
    private Outline outline;


    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    void Start()
    {
        // Check if the object uses linear mapping
        if(transform.GetComponent<DistanceLinearDrive>() != null)
            usesLinearMapping = true;
    }

    void FixedUpdate()
    {
        if (isHighlighted)
        {
            outline.enabled = true;
            isHighlighted = false;
        }
        else
        {
            outline.enabled = false;
        }
    }

    public void HighlightObject()
    {
        isHighlighted = true;
    }

    public void SetIdDistGrabbable(bool risDistGrabbable)
    {
        isDistGrabbable = risDistGrabbable;
    }
}
