using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleScript : MonoBehaviour
{
    public UnityEvent OnPuzzleCompleted;

    public KeyCode PuzzleCompletionKey = KeyCode.Space;

    bool completed = false;

    void Update()
    {
        if(!completed)
        {
            if(Input.GetKeyDown(PuzzleCompletionKey))
            {
                if(OnPuzzleCompleted != null) OnPuzzleCompleted.Invoke();
                completed = true;
            }
        }
    }
}
