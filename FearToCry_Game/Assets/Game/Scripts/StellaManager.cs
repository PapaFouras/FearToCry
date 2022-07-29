using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class StellaManager : MonoBehaviour
{
    Player player;
    public Transform porte;
    public Transform oreiller;
    public Transform fenetre;

    public FMODUnity.EventReference Stella_Idle;
    public FMOD.Studio.EventInstance stellaIdleInstance;

    public FMODUnity.EventReference Stella_Scream;
    public FMOD.Studio.EventInstance stellaScreamInstance;


    public bool followingPlayer = false;
    float durationToReachPlayer = 6f;

    private float lastMeetingWithPlayer = 0;

    float timeBeforeRespawn = 120;


    public enum Location
    {
        Porte,
        Oreiller,
        Fenetre
    }

    // Start is called before the first frame update
    private void Awake()
    {
        player = Player.instance;
        lastMeetingWithPlayer = Time.time;
        stellaScreamInstance = FMODUnity.RuntimeManager.CreateInstance(Stella_Scream);
        stellaIdleInstance = FMODUnity.RuntimeManager.CreateInstance(Stella_Idle);
        stellaScreamInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        stellaIdleInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

    }

    public void SetFollowingPlayer(bool isFollowing)
    {
        followingPlayer = isFollowing;
    }

    public void GoToPlayerFrom(int location)
    {
        switch (location)
        {
            case 1: GoToPlayerFrom(Location.Porte);
                break;
            case 2:
                GoToPlayerFrom(Location.Oreiller);
                break;
            case 3:
                GoToPlayerFrom(Location.Fenetre);
                break;
            default:
                GoToPlayerFrom(Location.Porte);
                break;
        }
    }

    public void GoToPlayerFrom(Location location)
    {
        switch (location)
        {
            case Location.Porte:
                StartCoroutine(GoToPlayer(porte.transform.position));
                break;
            case Location.Oreiller:
                StartCoroutine(GoToPlayer(oreiller.transform.position));
                break;
            case Location.Fenetre:
                StartCoroutine(GoToPlayer(fenetre.transform.position));
                break;
            
        }
    }

    private void Update()
    {
        if(GameManager.instance.GetCurrentRoomEnum() != GameManager.RoomName.Folie)
        {
            lastMeetingWithPlayer = Time.time;
        }
        if (!followingPlayer && GameManager.instance.GetCurrentRoomEnum() == GameManager.RoomName.Folie)
        {
            if (Time.time - lastMeetingWithPlayer > timeBeforeRespawn)
            {

                float rand = Random.Range(0, 1);
                if(rand < 0.5)
                {
                    GoToPlayerFrom(Location.Porte);
                }
                else
                {
                    GoToPlayerFrom(Location.Fenetre);
                }
            }
        }
    }

  

    IEnumerator GoToPlayer(Vector3 startPos)
    {
        float tValue = 0;
        followingPlayer = true;
        stellaScreamInstance.start();
        stellaIdleInstance.start();
        while(followingPlayer && Vector3.Distance(transform.position, player.headCollider.transform.position) > 0.1f)
        {
            //transform.position = Vector3.Lerp(startPos, player.headCollider.transform.position, tValue) ;
            transform.position = Vector3.Lerp(player.headCollider.transform.forward * 3 + player.headCollider.transform.position, player.headCollider.transform.position, tValue) ;
            transform.LookAt(player.headCollider.transform.position);
            stellaScreamInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            stellaIdleInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            yield return new WaitForEndOfFrame();
            tValue += Time.deltaTime / durationToReachPlayer;
        }
        if (Vector3.Distance(transform.position, player.headCollider.transform.position) <= 0.1f || tValue >= 1)
        {
            stellaIdleInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            GameManager.instance.ChangeRoom(1);
            lastMeetingWithPlayer = Time.time;
        }
        transform.position = porte.transform.position;
        followingPlayer = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GAMEOBJECT TOUCHED BY STELLA: " + other.gameObject.name);
        if(other.CompareTag("Hand"))
        {
            transform.position = porte.transform.position;
            followingPlayer = false;
            stellaIdleInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            lastMeetingWithPlayer = Time.time;
        }
    }
}
