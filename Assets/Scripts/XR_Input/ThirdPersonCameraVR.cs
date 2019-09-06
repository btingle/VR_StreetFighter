using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraVR : MonoBehaviour
{
	public Transform tracker;
	public bool XRotation, YRotation, ZRotation;
	
    // Update is called once per frame
    void LateUpdate()
    {
		var euler = tracker.rotation.eulerAngles;
		euler.x = XRotation ? euler.x : 0;
		euler.y = YRotation ? euler.y : 0;
		euler.z = ZRotation ? euler.z : 0;
		transform.localRotation = Quaternion.Euler(euler);
    }
}
