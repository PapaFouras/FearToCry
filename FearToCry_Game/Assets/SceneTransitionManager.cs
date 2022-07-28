using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeOutTp fadeScreen;
    public GameManager gm;

    public void goToRoom(int iRoom)
    {
        StartCoroutine(GoToSceneRoutine(iRoom));
    }

    IEnumerator GoToSceneRoutine(int iRoom)
    {
        yield return new WaitForSeconds(fadeScreen.fadeDuration*2);
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);
        // TP PLAYER
        gm.ChangeRoom(iRoom);

        fadeScreen.FadeIn();

    }

}
