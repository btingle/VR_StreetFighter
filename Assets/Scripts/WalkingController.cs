using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingController : MonoBehaviour
{
	public Transform hips;
	public WalkNode left_IK, right_IK;
	public float maxDistance, velocity;
	
	Vector3 lastPosition, velocityDirection, totalMovement;
	float elapsedTime;
	
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = new Vector3(transform.position.x, transform.position.z);
		elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var thisPosition = new Vector3(transform.position.x, 0f, transform.position.z);
		velocity = Vector3.Distance(lastPosition, thisPosition) / Time.deltaTime;
		velocityDirection = (thisPosition - lastPosition).normalized;
		
		var yRotation = Quaternion.Euler(0, hips.rotation.eulerAngles.y, 0); 
		var leftTarget  = thisPosition + yRotation * left_IK.getOffset();
		var rightTarget = thisPosition + yRotation * right_IK.getOffset();
		left_IK.target  = leftTarget;
		right_IK.target = rightTarget;
		
		var leftPosition = left_IK.transform.position;
		var rightPosition = right_IK.transform.position;
	
		float distanceLeft = Vector3.Distance(leftTarget, leftPosition);
		float distanceRight = Vector3.Distance(rightTarget, rightPosition);
		
		if (!left_IK.walking && !right_IK.walking)
			if (distanceLeft > distanceRight && distanceLeft > maxDistance){
				left_IK.StartWalk();
			}
			else if (distanceRight > maxDistance) {
				right_IK.StartWalk();
			}
		
		totalMovement += velocity * velocityDirection;
		if (elapsedTime > 0.75f) {
			StartCoroutine(resetLegs());
			elapsedTime = 0;
		}
		else elapsedTime += Time.deltaTime;
		
		lastPosition = thisPosition;
    }
	
	IEnumerator resetLegs()
	{
		left_IK.StartWalk();
		while (left_IK.walking) yield return null;
		right_IK.StartWalk();
		yield return null;
	}
}
