using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxNode : CollisionNode
{
	public string orientation;
	
	protected override void Awake()
	{
		base.Awake();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (!active) return;
		
		var collider = other.GetComponent<CollisionNode>();
		
		if (collider is HurtboxNode && collider.IsActive()) {
			Debug.Log("Hurt! Object " + collider.getParentName() + ", limb: " + collider.name + " >>>>> Object: " + controller.name + ", limb: " + name + ":::" + Time.time);
			
			var hurtbox = collider as HurtboxNode;
			
			if (!controller.HasIFrames()) {
				controller.getHit(orientation);
				var hitPosition = other.ClosestPoint(transform.position);
				var hitParticle = Instantiate(PrefabConstants.GetHitParticle(), hitPosition, Quaternion.identity);
				Destroy(hitParticle, 1.0f);
			}
			
			hurtbox.SendExitAttack();
		}
	}
}
