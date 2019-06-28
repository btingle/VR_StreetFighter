using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	Animator animator;
	Transform root;
	public float vertical;
	
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
		root = transform.Find("Root");
    }

    // Update is called once per frame
    void Update()
    {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		this.vertical = vertical;
		
		if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) > 0) {
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
	
	void DisableWalkCycles(string exception = "")
	{
		if (exception != "WalkForward") animator.SetBool("WalkForward", false);
		if (exception != "WalkBack") animator.SetBool("WalkBack", false);
		if (exception != "WalkRight") animator.SetBool("WalkRight", false);
		if (exception != "WalkLeft") animator.SetBool("WalkLeft", false);
	}
}
