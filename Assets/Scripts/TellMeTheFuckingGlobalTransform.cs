using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellMeTheFuckingGlobalTransform : MonoBehaviour
{
    public Vector3 position, rotation;
	void OnDrawGizmos()
	{
		position = transform.position;
		rotation = transform.rotation.eulerAngles;
	}
}
