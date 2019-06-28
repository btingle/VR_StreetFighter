using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraStabilizer : MonoBehaviour
{
	public Animator animator;

    // Update is called once per frame
    void Update()
    {
        var euler = transform.eulerAngles;
		Debug.Log(euler);
		if (Mathf.Sin(euler.x * Mathf.Deg2Rad) > Mathf.Sin(45 * Mathf.Deg2Rad))
			euler.x = 45;
		else if (Mathf.Sin(euler.x * Mathf.Deg2Rad) < Mathf.Sin(-45 * Mathf.Deg2Rad))
			euler.x = -45;
		transform.rotation = Quaternion.Euler(euler);
    }
	
	void OnAnimatorIK()
	{
		//var trackingHeadRot = InputTracking.GetLocalRotation(XRNode.Head);
		//animator.SetLookAtPosition(transform.position + (trackingHeadRot * Vector3.forward));
	}
}
