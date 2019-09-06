using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	public Transform camera;
	public float vertical;
	public float _speed = 0.04f;
	public float multiplier = 1f;
	public bool controllable = true;
	
	float speed { get { return _speed * multiplier; } }
	DistanceKeeper distancer;
	Animator animator;
	Transform root;
	
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
		root = transform.Find("Root");
		distancer = GetComponent<DistanceKeeper>();
    }
	
	Vector3 currentSlope = Vector3.up;

    // Update is called once per frame
    void Update()
    {
		RaycastHit hit;
		int ignoreLayers = ~(1 << 9);
		
		if (Physics.Raycast(transform.position + 0.3f * Vector3.up, Vector3.down, out hit, 100, ignoreLayers)) {
			transform.position = hit.point;
			currentSlope = hit.normal;
		}
		
		var moveRotation = Quaternion.FromToRotation(Vector3.up, currentSlope) * onlyYRotation(camera.rotation);
		
		//var moveRotation = Quaternion.FromToRotation(Vector3.up, currentSlope) * Quaternion.Euler(0, VRControllerInput.GetRightTrackpadRotation(), 0);
		
		if (controllable) {
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");
			this.vertical = vertical;
			
			var forward = moveRotation * Vector3.forward;
			var right = moveRotation * Vector3.right;
			
			transform.position += (vertical * speed * forward);
			transform.position += (horizontal * speed * right);
			
			if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) > 0) {
				
				distancer.CheckDistance();
				
				if (vertical < 0f) {
					animator.SetBool("WalkBack", true);
					DisableWalkCycles("WalkBack");
				}
				else if (vertical > 0f) {
					animator.SetBool("WalkForward", true);
					DisableWalkCycles("WalkForward");
				}
				else if (horizontal < 0f) {
					animator.SetBool("WalkLeft", true);
					DisableWalkCycles("WalkLeft");
				}
				else if (horizontal > 0f) {
					animator.SetBool("WalkRight", true);
					DisableWalkCycles("WalkRight");
				}
			}
			else {
				DisableWalkCycles();
			}
		}
    }
	
	void DisableWalkCycles(string exception = "")
	{
		if (exception != "WalkForward") animator.SetBool("WalkForward", false);
		if (exception != "WalkBack") animator.SetBool("WalkBack", false);
		if (exception != "WalkRight") animator.SetBool("WalkRight", false);
		if (exception != "WalkLeft") animator.SetBool("WalkLeft", false);
	}
	
	Quaternion onlyYRotation(Quaternion rot)
	{
		var euler = rot.eulerAngles;
		euler.x = euler.z = 0;
		return Quaternion.Euler(euler);
	}
}
