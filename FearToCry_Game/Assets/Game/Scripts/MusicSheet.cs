using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSheet : MonoBehaviour
{
    public GameObject[] pages;
    public Material materialWhenDone;

    public int nbPagesCompleted;

    public void FillOnePage(){
        if(nbPagesCompleted < pages.Length){
            pages[nbPagesCompleted].GetComponent<MeshRenderer>().sharedMaterial = materialWhenDone;
            nbPagesCompleted++;
        }
    }
}
