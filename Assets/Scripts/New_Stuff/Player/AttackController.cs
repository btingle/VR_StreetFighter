using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
	public bool controllable;
	public float blockSpeedMultiplier;
	
	Animator animator;
	int IFrames = 0;
	HitboxNodeManager hitboxManager;
	MovementController movement;
	PlayerStats stats;
	
    void Start()
    {
        animator = GetComponent<Animator>();
		hitboxManager = GetComponent<HitboxNodeManager>();
		movement = GetComponent<MovementController>();
		stats = GetComponent<PlayerStats>();
    }
	
	// reduces health on hit, called by hitbox scripts
	public void getHit(string orientation)
	{
		if (IFrames == 0) {
			stats.takeHit();
			animator.SetTrigger(orientation + "Hit");
			IFrames = 50;
		}
	}
	
	public void blockHit()
	{
		if (IFrames == 0) {
			stats.blockHit();
			if (stats.blockBroken()) {
				animator.SetBool("Block", false);
				getHit("Center");
			}
		}
	}
	
	// triggers or toggles animations based on input
	// animations have events that trigger callbacks in this script, which I will mark
    void Update()
    {
		if (controllable) {
			if (Input.GetKey(KeyCode.LeftShift)) {
				movement.multiplier = 2f;
				animator.SetFloat("movementMultiplier", 2);
			}
			else {
				movement.multiplier = 1f;
				animator.SetFloat("movementMultiplier", 1);
			}
			
			if (Input.GetKeyDown("e") || ActionInput.GetAction("LeftJab")) {
				animator.SetTrigger("PunchLeft");
			}
			else if (Input.GetKeyDown("r") || ActionInput.GetAction("RightJab")) {
				animator.SetTrigger("PunchRight");
			}
			else if (Input.GetKeyDown("f") || ActionInput.GetAction("RightHook")) {
				animator.SetTrigger("HookRight");
			}
			else if (Input.GetKeyDown("g") || ActionInput.GetAction("LeftHook")) {
				animator.SetTrigger("HookLeft");
			}
			else if (Input.GetKeyDown("q")) {
				animator.SetBool("Block", true);
				movement.multiplier = blockSpeedMultiplier;
				animator.SetFloat("movementMultiplier", blockSpeedMultiplier);
			}
			else if (Input.GetKeyUp("q")) {
				animator.SetBool("Block", false);
				movement.multiplier = 1f;
				animator.SetFloat("movementMultiplier", 1);
			}
		}
		IFrames = IFrames > 0 ? IFrames-1 : 0;
    }
	
	// queried by hitboxes to see whether or not a hit should go through 
	public bool HasIFrames()
	{
		return IFrames > 0;
	}
	
	// exits out of the current attack animation, and disables hitboxes related to the attack
	public void StopAttack()
	{
		hitboxManager.ExitAttack();
		animator.SetTrigger("ExitAttack");
	}
	
	// animation callback, determines when to enter the damage phase of an attack
	void DamageStart(string bodyPart)
	{
		switch (bodyPart) {
			case "LeftHand":
				hitboxManager.EnterLeftAttack(); break;
			case "RightHand":
				hitboxManager.EnterRightAttack(); break;
		}
	}
	
	// animation callback, determines when to exit the damage phase of an attack
	void DamageEnd(string bodyPart)
	{
		hitboxManager.ExitAttack();
	}
}
