using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ActionTracker : MonoBehaviour
{
	public Transform head;
	public float speed_threshold;
	public string currentMove;
	
	Vector3 last;
	
	int[] recording; // counts how many times a particular movement direction has been recorded
	int[] timeLeft; // counts how many timesteps are left for a particular recording action
	string[] dirNames;
	const int recordingSamples = 5; // number of samples taken for a directional action input
	const int requiredSamples = 3; // number of directional action samples required to register that action as having been taken
	
	public bool GetAction(string type)
	{
		if (currentMove == type) {
			currentMove = "None";
			return true;
		}
		return false;
	}
	
    void Start()
    {
        last = transform.position;
		currentMove = "None";
		recording = new int[6];
		timeLeft = new int[6];
		dirNames = new string[6] {"up", "down", "right", "left", "forward", "back"};
    }

    void FixedUpdate()
    {
        var current = transform.position;
		var difference = current - last;
		var speed = difference.magnitude;
		var direction = difference.normalized;
		
		if (speed > speed_threshold) {
			
			// determine which of the 6 3D vector directions (relative to our headset's transform) the controller movement is closest to
			float[] scores = {
				Vector3.Dot(direction, head.up),
				Vector3.Dot(direction, -head.up),
				Vector3.Dot(direction, head.right),
				Vector3.Dot(direction, -head.right),
				Vector3.Dot(direction, head.forward),
				Vector3.Dot(direction, -head.forward)
			};
			
			float best_score = scores.Max();
			int dir_index = Array.IndexOf(scores, best_score);
			UpdateRecording(dir_index);
		}
		
		UpdateAllRecordings();
		
		last = current;
    }
	
	// Controller movement can be noisy, so instead of relying on single samples of controller movement to determine the user's action, we take multiple samples of controller movement over a fixed time period
	// This way, small bumps in controller movement can be ignored
	void UpdateRecording(int direction)
	{
		if (timeLeft[direction] == 0) {
			//Debug.Log("Started Recording! " + dirNames[direction]);
			timeLeft[direction] = recordingSamples;
			recording[direction] = 1;
		}
		else {
			//Debug.Log(dirNames[direction]);
			recording[direction] += 1;
		}
	}
	
	// decrements time counter on all active action recordings
	// if a currently active action counter has ticked down to 0 and has the required number of movement samples, register that action as having been made
	void UpdateAllRecordings()
	{
		for (int dir = 0; dir < 6; dir++) {
			if (timeLeft[dir] > 0) {
				timeLeft[dir] -= 1;
				if (timeLeft[dir] == 0) {
					if (HasRequiredSamples(dir)) {
						currentMove = dirNames[dir];
						Debug.Log("Movement Detected! " + currentMove);
						ResetRecordings();
						//StartCoroutine(waitForCooldown());
					}
					recording[dir] = 0;
					//Debug.Log("Stopped Recording! " + dirNames[dir]);
				}
			}
		}
	}
	
	void ResetRecordings()
	{
		for (int dir = 0; dir < 6; dir++) {
			recording[dir] = 0;
			timeLeft[dir] = 0;
		}
	}
	
	bool HasRequiredSamples(int direction)
	{
		return recording[direction] >= requiredSamples;
	}
	
	IEnumerator waitForCooldown()
	{
		yield return new WaitForSeconds(1.0f);
		currentMove = "None";
	}
}
