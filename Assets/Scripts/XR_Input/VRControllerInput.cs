using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRControllerInput : MonoBehaviour
{
	static float leftTrackPadRotation, rightTrackPadRotation;
	
	void Update()
	{
		float leftHorizontal = Input.GetAxis("Horizontal_VR_Left");
		float leftVertical = -Input.GetAxis("Vertical_VR_Left");
		float rightHorizontal = Input.GetAxis("Horizontal_VR_Right");
		float rightVertical = -Input.GetAxis("Vertical_VR_Right");
		
		if (Mathf.Abs(leftHorizontal) + Mathf.Abs(leftVertical) > 0) {
			leftTrackPadRotation = Mathf.Atan2(leftHorizontal, leftVertical);
		}
		
		if (Mathf.Abs(rightHorizontal) + Mathf.Abs(rightVertical) > 0) {
			rightTrackPadRotation = Mathf.Atan2(rightHorizontal, rightVertical);
		}
		
		Debug.Log(leftTrackPadRotation * Mathf.Rad2Deg + ", " + rightTrackPadRotation * Mathf.Rad2Deg);
	}
	
    public static float GetLeftTrackpadRotation()
	{
		return leftTrackPadRotation * Mathf.Rad2Deg + 37;
	}
	
	public static float GetRightTrackpadRotation()
	{
		return rightTrackPadRotation * Mathf.Rad2Deg + 37;
	}
}
