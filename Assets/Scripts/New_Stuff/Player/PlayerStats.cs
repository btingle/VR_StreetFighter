using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health, posture;
	public bool recovering;
	
	void Awake()
	{
		posture = 1.0f;
		health = 1.0f;
	}
	
	void Update()
	{
		posture += recovering ? 0.0015f : 0.0005f;
		health += 0.0001f;
		if (posture >= 1f) {
			posture = 1f;
			recovering = false;
		}
		if (health >= 1f) {
			health = 1f;
		}
	}
	
	public void blockHit()
	{
		posture -= 0.25f;
		if (posture <= 0f) {
			posture = 0.001f;
			recovering = true;
		}
	}
	
	public void takeHit()
	{
		health -= 0.1f;
	}
	
	public bool blockBroken()
	{
		return posture <= 0f || recovering;
	}
}
