using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNode : MonoBehaviour
{
    protected AttackController controller;
	protected bool active;
	
	public bool startActive = true;
	
	protected virtual void Awake()
	{
		// get the controller's gameObject	
        var current = transform;
		while (current.parent != null)
			current = current.parent;
		controller = current.GetComponent<AttackController>();
		
		active = startActive;
	}
	
	public string getParentName()
	{
		return controller.name;
	}
	
	public void SetActive(bool state) 
	{
		active = state;
	}
	
	public bool IsActive()
	{
		return active;
	}
}
