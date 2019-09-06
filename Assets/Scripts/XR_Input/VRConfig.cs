using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRConfig : MonoBehaviour
{
	public Camera cam;
	
    void Awake()
	{
		InputTracking.disablePositionalTracking = true;
		XRDevice.DisableAutoXRCameraTracking(cam, true);
	}
}
