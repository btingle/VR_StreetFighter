using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionInput : MonoBehaviour
{
    public ActionTracker leftController, rightController;
	
	static ActionInput instance;
	
	void Awake()
	{
		instance = this;
	}
	
	public static bool GetAction(string name)
	{
		switch (name) {
			case "RightJab":
				return instance.rightController.GetAction("forward");
			case "LeftJab":
				return instance.leftController.GetAction("forward");
			case "RightHook":
				return instance.rightController.GetAction("left");
			case "LeftHook":
				return instance.leftController.GetAction("right");
		}
		return false;
	}
}
