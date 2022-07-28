using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partition : MonoBehaviour
{
    public GameObject notes;
    public Renderer dissolvePartition;
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
        StartCoroutine(DissolveAndDisplay());
       
    }

    IEnumerator DissolveAndDisplay()
    {
        float timeFromStart = 0;
        
        while(timeFromStart < .5f)
        {
            timeFromStart += Time.deltaTime;
            dissolvePartition.material.SetFloat("_Dissolve", timeFromStart*2);
            yield return new WaitForEndOfFrame();
        }
        timeFromStart = 0;
        currentNote++;
        if (currentNote < matList.Count)
        {
            notes.GetComponent<Renderer>().material = matList[currentNote];
        }
        while (timeFromStart < .5f)
        {
            timeFromStart += Time.deltaTime;
            dissolvePartition.material.SetFloat("_Dissolve",1- (timeFromStart * 2));
            yield return new WaitForEndOfFrame();
        }
    }

}
