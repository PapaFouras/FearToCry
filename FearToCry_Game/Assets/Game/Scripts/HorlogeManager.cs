using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorlogeManager : MonoBehaviour
{
    public List<HorlogeStartButton> originList = new List<HorlogeStartButton>();
    private List<HorlogeStartButton> randomList;
    private bool startGame = false;
    private HorlogeStartButton prievious = new HorlogeStartButton();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!startGame)
        {
            startGame = true;
            Reset();
        }
    }
    public void Reset()
    {
        randomList = new List<HorlogeStartButton>(originList);
        prievious = new HorlogeStartButton();
        for (int i = 0; i < randomList.Count; i++)
        {
            HorlogeStartButton temp = randomList[i];
            int randomIndex = Random.Range(i, randomList.Count);
            randomList[i] = randomList[randomIndex];
            randomList[randomIndex] = temp;
            temp.stopTicTac();
        }
        randomList[0].startTicTac();
    }

    public void Noitfy(HorlogeStartButton notifyer)
    {
        if (startGame)
        {
            if (notifyer != prievious && notifyer == randomList[0])
            {
                notifyer.stopTicTac();
                randomList.Remove(notifyer);
                randomList[0].startTicTac();
            }
            else
            {
                randomList[0].stopTicTac();
                Reset();
            }
        }  	
    }
}
