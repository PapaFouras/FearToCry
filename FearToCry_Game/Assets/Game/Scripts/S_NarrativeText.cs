using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_NarrativeText : MonoBehaviour
{
    public Material matDefault;
    public List<Material> matList = new List<Material>();
    public float timeDisp;
    public GameObject quad;

    // Start is called before the first frame update
    void Start()
    {
        quad.GetComponent<Renderer>().material = matDefault;
    }

    public void endScreen()
    {
        new WaitForSeconds(timeDisp*2f);
        StartCoroutine(afficheMat(0));
        new WaitForSeconds(timeDisp);
        StartCoroutine(afficheMat(1));
    }


    IEnumerator afficheMat(int iMat)
    {
        quad.GetComponent<Renderer>().material = matList[iMat];
        yield return new WaitForSeconds(timeDisp);
        quad.GetComponent<Renderer>().material = matDefault;
    }
}
