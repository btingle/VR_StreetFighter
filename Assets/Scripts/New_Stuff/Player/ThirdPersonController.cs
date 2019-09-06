using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ThirdPersonController : MonoBehaviour
{
	public Transform camera;
	public float sensitivity = 1f;
	bool mouseLocked = true;
	
	void Awake()
	{
		InputTracking.disablePositionalTracking = true;
	}
	
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
		mouseLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) {
			Cursor.lockState = mouseLocked ? CursorLockMode.None : CursorLockMode.Locked;
			mouseLocked = !mouseLocked;
		}
		
		float mouseVelX = Input.GetAxis("Mouse X");
		float mouseVelY = -Input.GetAxis("Mouse Y");
		
		transform.rotation = Quaternion.Euler(0, VRControllerInput.GetRightTrackpadRotation(), 0);
		//transform.Rotate(0, mouseVelX, 0);
		//camera.Rotate(mouseVelY, 0, 0);
    }
}
