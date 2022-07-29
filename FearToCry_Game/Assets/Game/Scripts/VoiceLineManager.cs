using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class VoiceLineManager : MonoBehaviour
{
    public FMODUnity.EventReference R_Medecin;
    public FMODUnity.EventReference R_FautVrmt;
    public FMODUnity.EventReference R_TuMeManques;
    public FMODUnity.EventReference R_LivrePrefStella;
    public FMODUnity.EventReference R_PlayTresorStella;
    public FMODUnity.EventReference R_PlayOuSuisJe;

    private void Awake()
    {
        FMODUnity.RuntimeManager.CreateInstance(R_Medecin);
        FMODUnity.RuntimeManager.CreateInstance(R_FautVrmt);
        FMODUnity.RuntimeManager.CreateInstance(R_TuMeManques);
        FMODUnity.RuntimeManager.CreateInstance(R_LivrePrefStella);
        FMODUnity.RuntimeManager.CreateInstance(R_PlayTresorStella);
        FMODUnity.RuntimeManager.CreateInstance(R_PlayOuSuisJe);
    }
    public void PlayRDVMedecin()
    {
        FMODUnity.RuntimeManager.PlayOneShot(R_Medecin, Player.instance.headCollider.transform.position);
    }
    public void PlayFautVrmt()
    {
        FMODUnity.RuntimeManager.PlayOneShot(R_FautVrmt, Player.instance.headCollider.transform.position);
    }
    bool firstTime = true;
    public void PlayStellaTuMeManques()
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }
        FMODUnity.RuntimeManager.PlayOneShot(R_TuMeManques, Player.instance.headCollider.transform.position);
    }
    public void PlayLivrePrefStella()
    {
        FMODUnity.RuntimeManager.PlayOneShot(R_LivrePrefStella, Player.instance.headCollider.transform.position);
    }
    public void PlayTresorStella()
    {
            FMODUnity.RuntimeManager.PlayOneShot(R_PlayTresorStella, Player.instance.headCollider.transform.position);
    }
    public void PlayOuSuisJe()
    {
        FMODUnity.RuntimeManager.PlayOneShot(R_PlayOuSuisJe, Player.instance.headCollider.transform.position);
    }
}
