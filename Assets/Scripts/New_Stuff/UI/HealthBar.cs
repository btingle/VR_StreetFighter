using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : StatusBar
{
    public PlayerStats player;
	
	protected override void Awake()
	{
		base.Awake();
	}
	
	protected override void Update()
	{
		fillValue = player.health;
		base.Update();
	}
}
