using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHour : MonoBehaviour
{
    [SerializeField]
    RushHourCar[] rushHourCars;
    private Vector3[] rushHourCarsPositions;

    [SerializeField]
    
    GrabbableCar[] grabbableCars;

    private void Start() {
        rushHourCarsPositions = new Vector3[rushHourCars.Length];
        int index = 0;
        foreach(var rushHourCar in rushHourCars ){
            rushHourCarsPositions[index] = new Vector3(rushHourCar.transform.position.x,rushHourCar.transform.position.y,rushHourCar.transform.position.z);
            index++;
        }
    }
   public void ResetRushHour(){
    int index = 0;
    foreach(var rushHourCar in rushHourCars ){
        rushHourCar.transform.position  = rushHourCarsPositions[index];
        index++;
    }
    foreach (var grabbableCar in grabbableCars)
    {
        grabbableCar.OnDetached();
    }
    Debug.Log("Resetting rush hour");
   }
}
