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

    public float countDownDuration = 2;

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
    public void Reset(bool restart = true)
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
        if(restart){
            randomList[0].startTicTac();
            ResetCountDown();
        }
    }

    public void Notify(HorlogeStartButton notifyer)
    {
        if(!win)
        {
            if (startGame)
            {
                if(randomList.Count >0){

                    if (notifyer != prievious && notifyer == randomList[0])
                    {
                        notifyer.stopTicTac();
                        randomList.Remove(notifyer);
                        randomList[0].startTicTac();
                        nbHorlogeOk++;
                        ResetCountDown();
                    }
                    else
                    {
                        randomList[0].stopTicTac();
                        Reset();
                    }
                }
                
            }
            if (nbHorlogeOk == originList.Count)
            {
                winSound.Play();
                win = true;
            }
        }
       
    }

    IEnumerator StartCountDown(){
        yield return new WaitForSeconds(countDownDuration);
        Reset(false);
        startGame = false;
        Debug.Log("Fin timer");
    }
    public void ResetCountDown(){
        StopAllCoroutines();
        StartCoroutine(StartCountDown());
    }
}
