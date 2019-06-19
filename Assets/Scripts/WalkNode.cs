using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkNode : MonoBehaviour
{
	public Vector3 offsetVector, target;
	public float offsetMagnitude, baseWalkHeight;
	public bool walking;
	
	Queue<IEnumerator> walkQueue;
	
	const float speed = 1f;
	const float timeStep = 0.02f;
	
	bool targetReached;
	
    // Start is called before the first frame update
    void Start()
    {
        walkQueue = new Queue<IEnumerator>();
		targetReached = false;
    }

	IEnumerator Walk()
	{
		walking = true;
		float initialDistance = Vector3.Distance(transform.position, target);
		float maxWalkHeight = initialDistance / 2f;
		var currentPosition = transform.position;
		float currentSpeed = speed * timeStep;
		
		do {
			currentPosition = Vector3.MoveTowards(currentPosition, target, currentSpeed);
			currentSpeed *= 1.1f;
			
			float currentDistance = Vector3.Distance(currentPosition, target);
			float footHeight = Mathf.Sin((currentDistance / initialDistance) * Mathf.PI) * (maxWalkHeight + baseWalkHeight);
			transform.position = new Vector3(currentPosition.x, footHeight, currentPosition.z);
			
			if (currentPosition.Equals(target)) {
				targetReached = true;
			}
			
			yield return null;
		}
		while (!targetReached);
		
		walking = false;
		targetReached = false;
		yield return null;
	}
	
	public void StartWalk()
	{
		if (!walking) StartCoroutine(Walk());
	}
	
	public Vector3 getOffset() { return offsetMagnitude * offsetVector; }
}
