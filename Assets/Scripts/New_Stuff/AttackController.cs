using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
	public HitboxNode rightHand, leftHand;
	public bool controllable;
	protected Animator animator;
	protected int IFrames = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
	
	public void getHit(string orientation)
	{
		if (IFrames == 0) {
			animator.SetTrigger(orientation + "Hit");
			leftHand.attack = false;
			rightHand.attack = false;
			IFrames = 50;
		}
	}
	
	// Update is called once per frame
    void Update()
    {
		if (controllable)
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
		IFrames = IFrames > 0 ? IFrames-1 : 0;
    }
	
	public bool HasIFrames()
	{
		return IFrames > 0;
	}
	
	public void StopAttack()
	{
		leftHand.attack = false;
		rightHand.attack = false;
		animator.SetTrigger("ExitAttack");
	}
	
	void DamageStart(string bodyPart)
	{
		Debug.Log("Punching!" + ", " + bodyPart + ", " + Time.time);
		switch (bodyPart) {
			case "LeftHand":
				leftHand.attack = true; break;
			case "RightHand":
				rightHand.attack = true; break;
		}
	}
	
	void DamageEnd(string bodyPart)
	{
		//Debug.Log("Un-Punching!" + ", " + bodyPart + ", " + Time.time);
		switch (bodyPart) {
			case "LeftHand":
				leftHand.attack = false; break;
			case "RightHand":
				rightHand.attack = false; break;
		}
	}
}
