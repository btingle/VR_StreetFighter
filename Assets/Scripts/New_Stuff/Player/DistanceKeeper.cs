using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceKeeper : MonoBehaviour
{
	public Transform fighter;
	public float minDistance;
	
	public void CheckDistance()
	{
		if (fighter == null) return;
		
		float distance = Vector3.Distance(transform.position, fighter.position);
		
		if (distance < minDistance) {
			var offsetVector = (transform.position - fighter.position).normalized;
			offsetVector.y = 0;
			transform.position = fighter.position + (minDistance * offsetVector.normalized);
		}
	}
}
