using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabConstants : MonoBehaviour
{
	public GameObject hitParticle, blockParticle;
	
	private static PrefabConstants instance;
	void Awake()
	{
		instance = this;
	}
	
    public static GameObject GetHitParticle() { return instance.hitParticle; }
	public static GameObject GetBlockParticle() { return instance.blockParticle; }
}
