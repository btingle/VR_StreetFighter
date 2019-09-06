using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFixedPerspective : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var euler = transform.rotation.eulerAngles;
		euler.y = 0;
		transform.rotation = Quaternion.Euler(euler);
		
		Debug.Log(Input.GetAxis("Horizontal_VR_Left"));
    }
}
