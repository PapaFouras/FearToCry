using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class MatchStickBox : MonoBehaviour
{

	public FMODUnity.EventReference Prendre_Box;

	private void OnAttachedToHand( Hand hand )
		{
		//FMODUnity.RuntimeManager.PlayOneShot(Prendre_Box, transform.position);
		hand.GetComponent<HandGOReferences>().matchStickBox = gameObject;
			hand.otherHand.GetComponent<HandGOReferences>().matchStickBox = gameObject;
		}


		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			hand.GetComponent<HandGOReferences>().matchStickBox = null;
		}
}
