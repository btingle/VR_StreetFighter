using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBonerRenderer : MonoBehaviour
{
    // Update is called once per frame
    void OnDrawGizmos()
    {
        Stack<Transform> skeleton = new Stack<Transform>();
		skeleton.Push(transform);
		while (skeleton.Count > 0) {
			Transform current = skeleton.Pop();
			if (current != transform) {
				Debug.DrawLine(current.position, current.parent.position, Color.red);
				Gizmos.color = Color.green;
				Gizmos.DrawWireSphere(current.position, 0.008f);
			}
			foreach (Transform bone in current) {
				skeleton.Push(bone);
			}
		}
    }
}
