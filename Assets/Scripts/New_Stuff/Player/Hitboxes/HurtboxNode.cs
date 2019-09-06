using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxNode : CollisionNode
{
    protected override void Awake()
	{
		base.Awake();
	}
	
	public void SendExitAttack()
	{
		controller.StopAttack();
	}
}
