using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OursBurning : MonoBehaviour
{
    public MeshRenderer[] renderers;
    public void StartBurn()
    {
        StartCoroutine(Dissolve());
    }

    IEnumerator Dissolve()
    {
        float value = 0f;
        while (true)
        {
            value += Time.deltaTime/3;
            for(int i = 0; i < renderers.Length; i++)
            {
                for(int j = 0; j < renderers[i].materials.Length;j++)
                    renderers[i].materials[j].SetFloat("_Dissolve", value);
                
            }
           yield return new WaitForEndOfFrame();
        }

    }
}
