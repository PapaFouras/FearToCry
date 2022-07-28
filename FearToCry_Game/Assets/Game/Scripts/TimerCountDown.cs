using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerCountDown : MonoBehaviour
{
    public TMPro.TMP_Text horlogeText;
    public int numberMinutes;
    public int numberSeconds;
    UnityEvent onTimerOver;
    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        UpdateHorlogeDisplay();
        StartCoroutine(DecreaseSecond());
    }
    void UpdateHorlogeDisplay()
    {
        string second = numberSeconds < 10 ? "0" + numberSeconds : numberSeconds.ToString();
        string minute = numberMinutes < 10 ? "0" + numberMinutes : numberMinutes.ToString();
        horlogeText.text = minute + ":" + second;
    }

    public IEnumerator DecreaseSecond()
    {
        yield return new WaitForSeconds(1f);
        numberSeconds--;
        if (numberSeconds < 0)
        {
            DecreaseMinute();
        }
        else
        {
            StartCoroutine(DecreaseSecond());
        }
        UpdateHorlogeDisplay();

        
    }

    void DecreaseMinute()
    {
        numberMinutes--;
        if(numberMinutes <= 0)
        {
            onTimerOver?.Invoke();
            return;
        }
        else
        {
            numberSeconds = 59;
            StartCoroutine(DecreaseSecond());
        }
    }

   
}
