using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigherDummy : MonoBehaviour
{
	Animator animator;
	
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
		StartCoroutine(CombatCycle());
    }

    IEnumerator CombatCycle()
	{
		while (true) {
			animator.SetTrigger("PunchLeft");
			yield return new WaitForSeconds(3.0f);
			animator.SetTrigger("PunchRight");
			yield return new WaitForSeconds(3.0f);
			animator.SetBool("Block", true);
			yield return new WaitForSeconds(1.0f);
			animator.SetBool("Block", false);
		}
	}
}
