using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XylophoneKey : MonoBehaviour
{
    public int note;
    private ParticleSystem ps;

    [SerializeField]
    private XylophoneManager xylophoneManager;

    [SerializeField]
    public FMODUnity.EventReference XyloNote;
    FMOD.Studio.EventInstance xyloNote;

    private bool canBePLayed = true;
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }


    private void OnCollisionEnter(Collision other) {
        Debug.Log("colliding");
        Debug.Log("Gameobject colliding : " + other.gameObject.name);
        Debug.Log("Gameobject tag colliding : " + other.gameObject.tag);
         if(other.gameObject.CompareTag("XylophoneStick")  && canBePLayed){
            if(Vector2.Distance(other.gameObject.GetComponent<XylophoneStick>().sphereCenter.transform.position,other.GetContact(0).point) < other.gameObject.GetComponent<XylophoneStick>().sphereCollider.radius * 1.001f){
                Debug.Log("should play note: "+note);
               canBePLayed = false;

                StartCoroutine(TimeBeforeCanBeReplayed());
                ps.Play();
                xyloNote.setParameterByName("Notes_Xylo", note);
                xyloNote.start();
                // parameter note
                // joue le son
                xylophoneManager.AddNote(note.ToString());
            }
            
        }
    }

    IEnumerator TimeBeforeCanBeReplayed(){
        yield return new WaitForSeconds(.3f);
        canBePLayed = true;
    }

    

}
