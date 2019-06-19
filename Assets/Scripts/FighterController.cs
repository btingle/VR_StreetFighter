using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
	Animator animator;
	Transform root;
	
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
		root = transform.Find("Root");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e")) {
			animator.SetTrigger("PunchLeft");
		}
		else if (Input.GetKeyDown("r")) {
			animator.SetTrigger("PunchRight");
		}
		else if (Input.GetKeyDown("q")) {
			animator.SetBool("Block", true);
		}
		else if (Input.GetKeyUp("q")) {
			animator.SetBool("Block", false);
		}
		
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		
		if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) > 0) {
			animator.SetBool("Walking", true);
			Vector3 facingDirection = 0.5f * (vertical * Vector3.forward + horizontal * Vector3.right);
			transform.rotation = Quaternion.LookRotation(facingDirection, Vector3.up);
			transform.position += 0.1f * facingDirection;
		}
		else {
			animator.SetBool("Walking", false);
		}
    }
}
