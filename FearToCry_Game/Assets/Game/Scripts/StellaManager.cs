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

    public bool followingPlayer = false;
    public float durationToReachPlayer = 6f;

    private float lastMeetingWithPlayer = 0;

    public float timeBeforeRespawn = 60;


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
        while(followingPlayer && Vector3.Distance(transform.position, player.headCollider.transform.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(startPos, player.headCollider.transform.position, tValue) ;
            transform.LookAt(player.headCollider.transform.position);
            yield return new WaitForEndOfFrame();
            tValue += Time.deltaTime / durationToReachPlayer;
        }
        if (Vector3.Distance(transform.position, player.headCollider.transform.position) <= 0.1f || tValue >= 1)
        {
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
            lastMeetingWithPlayer = Time.time;
        }
    }
}
