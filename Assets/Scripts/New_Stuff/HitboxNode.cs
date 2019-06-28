using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxNode : MonoBehaviour
{
	AttackController controller;
	public bool attack = false;
	public string orientation;
	
    // Start is called before the first frame update
    void Awake()
    {
		// get the controller's gameObject
        var current = transform;
		while (current.parent != null)
			current = current.parent;
		controller = current.GetComponent<AttackController>();
    }
	
	void SendStopAttackSignal()
	{
		controller.StopAttack();
	}
	
    void OnTriggerEnter(Collider other)
	{
		var hitbox = other.GetComponent<HitboxNode>();
		
		if (hitbox != null && hitbox.attack) {
			hitbox.SendStopAttackSignal();
			if (!controller.HasIFrames())
				controller.getHit(orientation);
		}
	}
}
