using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XylophoneManager : MonoBehaviour
{
    public string code = "1234";
    public string currentCode ="";
    public UnityEvent onMusicPlayed;

   public void AddNote(string note){
        currentCode += note;
        Debug.Log("currentcode length : " + currentCode.Length);
        Debug.Log("code length : " + code.Length);
        RemoveFirstChar();
        CheckCode();

   }
    public void RemoveFirstChar()
    {
        if (currentCode.Length > code.Length)
        {
            Debug.Log("Remove first char");
            currentCode = currentCode.Remove(0, 1);
            RemoveFirstChar();
        }
    }

   public void CheckCode(){
        if(currentCode == code ){
            onMusicPlayed?.Invoke();
        }
   }
}
