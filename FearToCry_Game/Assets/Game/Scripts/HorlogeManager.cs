using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorlogeManager : MonoBehaviour
{
	public List<HorlogeTimer> myList = new List<HorlogeTimer>();
	public Material matTicTac;
	public Material matOk;
	public Material matWin;
	int lastnumberOk = -1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
    	lastnumberOk = -1;
    	int cpt = 0;
        foreach (HorlogeTimer element in myList)
		{
			element.number = cpt;
		   element.GetComponent<MeshRenderer> ().material = matWin;
		myList[0].isTicTac = false;
		   cpt++;
		}
		myList[0].isTicTac = true;
		myList[0].GetComponent<MeshRenderer> ().material = matTicTac;
    }

    public void Noitfy(int number)
    {
    	if(number == lastnumberOk+1)
    	{

    		

    		lastnumberOk = number;
    		myList[number].GetComponent<MeshRenderer> ().material = matOk;
    		myList[number].isTicTac = false;
	    	
	    	if(lastnumberOk < myList.Count-1)
	    	{
	    		myList[number+1].GetComponent<MeshRenderer> ().material = matTicTac;
	    		myList[number+1].isTicTac = true;
			} else
			{
				foreach (HorlogeTimer element in myList)
				{
				   element.GetComponent<MeshRenderer> ().material = matWin;
				}
			}
    	} else
    	{
    		Reset();
    	}
    	
    	
    }
}
