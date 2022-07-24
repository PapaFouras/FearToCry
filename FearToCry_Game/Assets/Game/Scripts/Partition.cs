using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partition : MonoBehaviour
{
    public GameObject notes;
    public List<Material> matList = new List<Material>();
    private int currentNote = 0;

    // Start is called before the first frame update
    void Start()
    {
        notes.GetComponent<Renderer>().material = matList[currentNote];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NotifyBurnItem()
    {
        Debug.Log("New Note");
        currentNote = currentNote + 1;
        notes.GetComponent<Renderer>().material = matList[currentNote];
    }

}
