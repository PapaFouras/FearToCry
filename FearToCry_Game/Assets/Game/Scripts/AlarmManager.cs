using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    [SerializeField]
   public AlarmClock[] alarms;

   private List<AlarmClock> alarmsNotChecked;

   private void Awake() {
       ResetAllAlarm();
   }

   private void ResetAllAlarm(){
       alarmsNotChecked = new List<AlarmClock>();
       foreach (var alarm in alarms)
       {
           alarm.isChecked = false;
           alarm.alarmManager = this;
           alarmsNotChecked.Add(alarm);
       }
    }

    public void SetAlarm(AlarmClock alarm){
        for (var i = 0; i < alarms.Length; i++)
        {
            if(alarm == alarms[i]){
                alarm.isChecked = true;
                alarmsNotChecked.Remove(alarm);
            }
        }
    }

    public AlarmClock GetRandomAlarmNotChecked(){
        int alarmNotCheckedSize = alarmsNotChecked.Count;
        int randomAlarmIndex = Random.Range(0,alarmNotCheckedSize);
        return alarmsNotChecked[randomAlarmIndex];
    }

    public bool GetIsComplete(){
        if (alarmsNotChecked.Count == 0){
            return true;
        }
        return false;
    }

    public void NextAlarm(){
        
    }

    public void OnComplete(){

    }
}
