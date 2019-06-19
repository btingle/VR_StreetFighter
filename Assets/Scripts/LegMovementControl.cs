using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LegMovementControl : MonoBehaviour
{
	public Transform headTracker;
	public Transform robot;
	public Vector3 offset;
	public float legRaise;
	
	float elapsedTime = 0f;
	
	bool moving = false;
	
    // Update is called once per frame
    void Update()
    {
		var headGroundPos = new Vector3(headTracker.position.x, 0f, headTracker.position.z);
		if (Vector3.Distance(transform.position, headGroundPos) > 0.3f) {
			if (!moving) {
				moving = true;
				var directionalOffset = 0.25f * (headTracker.rotation * Vector3.forward);
				StartCoroutine(MoveToTarget(headGroundPos, headTracker.rotation));
			}
		}
    }
	
	IEnumerator MoveToTarget(Vector3 targetPosition, Quaternion targetRotation)
	{
		float time = 0f;
		var startPosition = transform.position;
		var startRotation = transform.rotation;
		
		while (time < 1f) {
			transform.position = Vector3.Lerp(startPosition, targetPosition, time);
			transform.rotation = Quaternion.Lerp(startRotation, targetRotation, time);
			time += 0.02f;
			yield return null;
		}
		
		moving = false;
		yield return null;
	}
}
