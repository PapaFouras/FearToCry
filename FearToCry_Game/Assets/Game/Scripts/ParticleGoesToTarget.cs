using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleGoesToTarget : MonoBehaviour
{
    public Transform targetTransform;
    public GameObject startGO;
    private Vector3 startPoint;
    private Vector3 midPoint;
    [SerializeField] [Range(0,1)] float tValue = 0;
    public float duration = 2;

    public bool canGo = false;

    public UnityEvent onParticleArrived;

    public void SetCanGo(bool rcanGo)
    {
        canGo = rcanGo;
    }
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<ParticleSystem>().Play();
        startPoint = new Vector3(startGO.transform.position.x, startGO.transform.position.y, startGO.transform.position.z);
        midPoint = (targetTransform.position + startPoint) / 2f;
        float yMidPoint = Mathf.Min(.8f, Vector3.Distance(startPoint, targetTransform.position) / 2f);
        midPoint = new Vector3(midPoint.x, midPoint.y + yMidPoint, midPoint.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canGo)
        {
            transform.position = startGO.transform.position;
            startPoint = new Vector3(startGO.transform.position.x, startGO.transform.position.y, startGO.transform.position.z);
            tValue = 0;
            return;
        }
        tValue += Time.deltaTime / duration;
        Vector3 lerp1 = Vector3.Lerp(startPoint, midPoint, tValue);
        Vector3 lerp2 = Vector3.Lerp(midPoint, targetTransform.position, tValue);
        Vector3 lerp3 = Vector3.Lerp(lerp1, lerp2, tValue);
        transform.position = lerp3;
        if (tValue > 1f)
        {
            onParticleArrived?.Invoke();
            Destroy(gameObject);
        }
    }



    //private void OnValidate()
    //{
    //    startPoint = startGO.transform.position;
    //    midPoint = (targetTransform.position + startPoint) / 2f;
    //    float yMidPoint = Mathf.Min(.8f, Vector3.Distance(startPoint, targetTransform.position) / 2f);
    //    midPoint = new Vector3(midPoint.x, midPoint.y + yMidPoint, midPoint.z);
    //    Vector3 lerp1 = Vector3.Lerp(startPoint, midPoint, tValue);
    //    Vector3 lerp2 = Vector3.Lerp(midPoint, targetTransform.position, tValue);
    //    Vector3 lerp3 = Vector3.Lerp(lerp1, lerp2, tValue);
    //    transform.position = lerp3;
    //}
}
