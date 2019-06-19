using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigTestMovement : MonoBehaviour
{
	public Transform la, ra, ll, rl, head;
	private Animator animator;
	
	void Start()
	{
		animator = GetComponent<Animator>();
	}
	
	void OnAnimatorIK()
	{
		// set weights for animator
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
		animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
		animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
		animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
		animator.SetLookAtWeight(1);
		
		// get positions & rotations of VR devices
		/*Quaternion trackingHeadRot, trackingLeftHandRot, trackingRightHandRot;
		Vector3 trackingHeadPos, trackingLeftHandPos, trackingRightHandPos;
		trackingHeadPos = InputTracking.GetLocalPosition(XRNode.Head);
		trackingHeadRot = InputTracking.GetLocalRotation(XRNode.Head);
		trackingLeftHandPos = InputTracking.GetLocalPosition(XRNode.LeftHand);
		trackingLeftHandRot = InputTracking.GetLocalRotation(XRNode.LeftHand);
		trackingRightHandPos = InputTracking.GetLocalPosition(XRNode.RightHand);
		trackingRightHandRot = InputTracking.GetLocalRotation(XRNode.RightHand);*/
		
		// set position & rotation of left and right hands, as well as the lookAt position
		animator.SetIKPosition(AvatarIKGoal.RightHand, ra.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, ra.rotation);
		animator.SetIKPosition(AvatarIKGoal.LeftHand, la.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, la.rotation);
		animator.SetIKPosition(AvatarIKGoal.LeftFoot, ll.position);
        animator.SetIKRotation(AvatarIKGoal.LeftFoot, ll.rotation);
		animator.SetIKPosition(AvatarIKGoal.RightFoot, rl.position);
        animator.SetIKRotation(AvatarIKGoal.RightFoot, rl.rotation);
		animator.SetLookAtPosition(head.position + (head.rotation * Vector3.forward));
		
		transform.position = new Vector3(head.position.x, head.position.y - 1.71f, head.position.z);
		transform.rotation = Quaternion.Euler(0f, head.rotation.eulerAngles.y, 0f);
	}
}
