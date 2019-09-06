using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public Transform head;
	Animator animator;
	
    void Awake()
	{
		animator = GetComponent<Animator>();
	}
	
	Vector3 currentSlope = Vector3.up;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
		
		if (Physics.Raycast(transform.position + 0.3f * Vector3.up, Vector3.down, out hit)) {
			transform.position = hit.point;
			var look = head.forward.normalized;
			//Debug.DrawLine(transform.position, transform.position + look, Color.red);
			look.y = 0f;
			float incline = Mathf.Asin(Vector3.Dot(hit.normal, look)) / Mathf.PI + 0.5f;
			currentSlope = hit.normal;
			animator.SetFloat("Incline", incline);
		}
		
		var moveRotation = Quaternion.FromToRotation(Vector3.up, currentSlope) * onlyYRotation(head.rotation);
		
		float speed = 0.05f;
		
		float horizontal = Input.GetAxis("Horizontal") * speed;
		float vertical = Input.GetAxis("Vertical") * speed;
		
		transform.position += vertical * (moveRotation * Vector3.forward);
		transform.position += horizontal * (moveRotation * Vector3.right);
		
		if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) < 0.0005f) {
			animator.SetBool("walking", false);
		}
		else animator.SetBool("walking", true);
    }
	
	Quaternion onlyYRotation(Quaternion rot)
	{
		var euler = rot.eulerAngles;
		euler.x = euler.z = 0;
		return Quaternion.Euler(euler);
	}
}
