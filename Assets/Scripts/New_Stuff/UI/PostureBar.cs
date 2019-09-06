using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostureBar : StatusBar
{
    public PlayerStats player;
	public Color recoveryColor;
	
	protected override void Awake()
	{
		base.Awake();
	}
	
	protected override void Update()
	{
		fillValue = player.posture;
		if (player.recovering) {
			fillImage.color = recoveryColor;
		}
		else {
			fillImage.color = fillColor;
		}
		base.Update();
	}
}
