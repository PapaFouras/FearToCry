using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorlogeManager : MonoBehaviour
{
    public List<HorlogeStartButton> originList = new List<HorlogeStartButton>();
    private List<HorlogeStartButton> randomList;
    private bool startGame = false;
    private bool win = false;
    private HorlogeStartButton prievious = new HorlogeStartButton();
    private int nbHorlogeOk = 0;
    public AudioSource winSound;
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
        nbHorlogeOk = 0;
        randomList[0].startTicTac();
    }

    public void Noitfy(HorlogeStartButton notifyer)
    {
        if(!win)
        {
            if (startGame)
            {
                if (notifyer != prievious && notifyer == randomList[0])
                {
                    notifyer.stopTicTac();
                    randomList.Remove(notifyer);
                    randomList[0].startTicTac();
                    nbHorlogeOk++;
                }
                else
                {
                    randomList[0].stopTicTac();
                    Reset();
                }
            }
            if (nbHorlogeOk == originList.Count)
            {
                winSound.Play();
                win = true;
            }
        }
       
    }
}
