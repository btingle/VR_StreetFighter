using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockNode : CollisionNode
{
    protected override void Awake()
	{
		base.Awake();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (!active) return;
		
		var collider = other.GetComponent<CollisionNode>();
		
		if (collider is HurtboxNode && collider.IsActive()) {
			Debug.Log("Blocked! Object " + collider.getParentName() + ", limb: " + collider.name + " >>>>> Object: " + controller.name + ", limb: " + name + ":::" + Time.time);
			
			var hurtbox = collider as HurtboxNode;
			hurtbox.SendExitAttack();
			
			if (!controller.HasIFrames()) {
				controller.blockHit();
				var hitPosition = other.ClosestPoint(transform.position);
				var hitParticle = Instantiate(PrefabConstants.GetBlockParticle(), hitPosition, Quaternion.identity);
				Destroy(hitParticle, 1.0f);
			}
		}
	}
}
