using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRIKController : MonoBehaviour
{
	public Transform head, leftHand, rightHand;
	Animator animator;
	
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnAnimatorIK()
    {
		// set weights for animator
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
		animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
		animator.SetLookAtWeight(1);
		
		// get positions & rotations of VR devices
		Quaternion trackingHeadRot, trackingLeftHandRot, trackingRightHandRot;
		Vector3 trackingHeadPos, trackingLeftHandPos, trackingRightHandPos;
		trackingHeadPos = InputTracking.GetLocalPosition(XRNode.Head);
		trackingHeadRot = InputTracking.GetLocalRotation(XRNode.Head);
		trackingLeftHandPos = InputTracking.GetLocalPosition(XRNode.LeftHand);
		trackingLeftHandRot = InputTracking.GetLocalRotation(XRNode.LeftHand);
		trackingRightHandPos = InputTracking.GetLocalPosition(XRNode.RightHand);
		trackingRightHandRot = InputTracking.GetLocalRotation(XRNode.RightHand);
		
		// set position & rotation of left and right hands, as well as the lookAt position
		animator.SetIKPosition(AvatarIKGoal.RightHand, trackingRightHandPos);
        animator.SetIKRotation(AvatarIKGoal.RightHand, trackingRightHandRot);
		animator.SetIKPosition(AvatarIKGoal.LeftHand, trackingLeftHandPos);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, trackingLeftHandRot);
		animator.SetLookAtPosition(head.position + (trackingHeadRot * Vector3.forward));
		
		transform.position = new Vector3(trackingHeadPos.x, 0, trackingHeadPos.z);
    }
}
