using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxNodeManager : MonoBehaviour
{
    public HitboxNode chest, shoulder_L, forearm_L, hand_L, shoulder_R, forearm_R, hand_R;
	public HurtboxNode hurt_Hand_L, hurt_Hand_R;
	public BlockNode block;
	
	public void EnterLeftAttack()
	{
		hurt_Hand_L.SetActive(true);
	}
	
	public void EnterRightAttack()
	{
		hurt_Hand_R.SetActive(true);
	}
	
	public void ExitAttack()
	{
		hurt_Hand_L.SetActive(false);
		hurt_Hand_R.SetActive(false);
	}
	
	public void EnterBlock()
	{
		forearm_L.SetActive(false);
		forearm_R.SetActive(false);
		shoulder_L.SetActive(false);
		shoulder_R.SetActive(false);
		hand_L.SetActive(false);
		hand_R.SetActive(false);
		block.SetActive(true);
	}
	
	public void ExitBlock()
	{
		shoulder_L.SetActive(true);
		shoulder_R.SetActive(true);
		forearm_L.SetActive(true);
		forearm_R.SetActive(true);
		hand_L.SetActive(true);
		hand_R.SetActive(true);
		block.SetActive(false);
	}
}
