using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiatorForBattle : UnitForBattle
{
	protected override void Awake()
	{
		//unit stat load
		maxHp = 123;
		atk = 10;

		base.Awake();
	}

	protected override bool MoveLogic()
	{		
		return false;
	}

	protected override bool AttackLogic()
	{
		//if (battleManager)
		//	Debug.Log("null");
		//else
		//	Debug.Log("not null");

		//Debug.Log(boardX + ", " + boardY + ", " + player);
		//if (battleManager.SearchInBoard(boardX + (int)player, boardY, player)) 
		//{
		//	//target.TakeDamage(atk);
		//	// play attack animation
		//	return true;
		//}

		return false;
	}

	protected override void DeathAttle()
	{

	}

	
}
