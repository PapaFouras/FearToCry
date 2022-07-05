using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XylophoneManager : MonoBehaviour
{
    public string code = "ihgf";
    public string currentCode ="";

   public ParticleSystem ps;
   public void AddNote(string note){
        currentCode += note;
        Debug.Log("currentcode length : " + currentCode.Length);
        Debug.Log("code length : " + code.Length);
        if(currentCode.Length > code.Length){
            Debug.Log("Remove first char");
            currentCode = currentCode.Remove(0,1);
        }
        CheckCode();

   }

   public void CheckCode(){
        if(currentCode == code ){
            ps.Play();
        }
   }
}
