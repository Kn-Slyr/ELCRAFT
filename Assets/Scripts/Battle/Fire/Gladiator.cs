using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gladiator : UnitForBattle
{
	protected override void Start()
	{
		//unit stat load
		maxHp = 123;
		atk = 10;

		base.Start();
	}

	protected override bool MoveLogic()
	{		
		return false;
	}

	protected override bool AttackLogic()
	{
		UnitForBattle target;
		if (battleManager.InBoardSearch(boardX + (int)player, boardY, player, out target)) 
		{
			target.TakeDamage(atk);
			// play attack animation
			return true;
		}

		return false;
	}

	protected override void BattleCry()
	{

	}

	protected override void DeathAttle()
	{

	}
}
