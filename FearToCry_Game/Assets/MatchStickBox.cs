using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class MatchStickBox : MonoBehaviour
{
 private void OnAttachedToHand( Hand hand )
		{
			hand.GetComponent<HandGOReferences>().matchStickBox = gameObject;
			hand.otherHand.GetComponent<HandGOReferences>().matchStickBox = gameObject;
		}


		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			hand.GetComponent<HandGOReferences>().matchStickBox = null;
		}
}
