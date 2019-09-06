using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootIK : MonoBehaviour
{
	public Transform
	leftFoot,
	leftHint,
	rightHint,
	rightFoot,
	head;
	
	Animator animator;
	Quaternion leftFootRot, rightFootRot;
	
	public float offset = 0.045236f;
	Vector3 ogl, ogr;
	
	void Awake()
	{
		animator = GetComponent<Animator>();
		leftFootRot = leftFoot.rotation;
		rightFootRot = rightFoot.rotation;
	}
	
    void OnAnimatorIK()
	{
		Vector3 left_pos, right_pos;
		left_pos = right_pos = Vector3.zero;
		
		Quaternion left_rot, right_rot;
		left_rot = right_rot = Quaternion.identity;
		
		FindGround(leftFoot, leftFootRot, ref left_pos, ref left_rot);
		FindGround(rightFoot, rightFootRot, ref right_pos, ref right_rot);
		
		float left_weight = animator.GetFloat("leftFoot");
		float right_weight = animator.GetFloat("rightFoot");
		
		ApplyIK(AvatarIKGoal.LeftFoot, left_weight, left_pos, left_rot);
		ApplyIK(AvatarIKGoal.RightFoot, right_weight, right_pos, right_rot);
		
		animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, 1.0f);
		animator.SetIKHintPosition(AvatarIKHint.LeftKnee, leftHint.position);
		animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, 1.0f);
		animator.SetIKHintPosition(AvatarIKHint.RightKnee, rightHint.position);
	}
	
	void ApplyIK(AvatarIKGoal goal, float weight, Vector3 position, Quaternion rotation)
	{
		animator.SetIKPositionWeight(goal, weight);
		animator.SetIKRotationWeight(goal, weight);
		animator.SetIKPosition(goal, position);
		animator.SetIKRotation(goal, rotation);
	}
	
	void FindGround(Transform target, Quaternion restingRotation, ref Vector3 targetPosition, ref Quaternion targetRotation)
	{
		RaycastHit hit;
		var origin = target.position + Vector3.up;
		//Debug.DrawLine(origin, origin + Vector3.down, Color.green);
		
		int ignoreLayers = ~(1 << 9); // Hit everything except layer 9
		
		if (Physics.Raycast(origin, -Vector3.up, out hit, 100, ignoreLayers)) {
			targetPosition = hit.point + offset * hit.normal;
			//Debug.DrawLine(hit.point, hit.point + hit.normal, Color.blue);
			targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * onlyYRotation(head.rotation);
		}
	}
	
	Quaternion onlyYRotation(Quaternion rot)
	{
		var euler = rot.eulerAngles;
		euler.x = euler.z = 0;
		return Quaternion.Euler(euler);
	}
}
