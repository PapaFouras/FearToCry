using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThuderScript : MonoBehaviour
{
    public GameObject light;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startThunder()
    {
        StartCoroutine(ThunderRoutine());
    }

    IEnumerator ThunderRoutine()
    {
        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(false);
    }

}
