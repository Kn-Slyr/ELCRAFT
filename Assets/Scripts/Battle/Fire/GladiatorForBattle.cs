using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiatorForBattle : UnitForBattle
{

	protected override void Awake()
	{
		//unit stat load
		actionCode = ActionCode.FireGladiator;
		maxHp = 123;
		atk = 10;
		moveRange = 2;

		base.Awake();
	}

	// It's implement for only special moving
	//protected override void MoveLogic()
	//{
	//	Debug.Log(GetName() + " Move!");
	//	// simple moving, have to change
	//	RemovePosition();

	//	int count = 0;
	//	while (count < 2 && battleManager.IsEmptyInBoard((int)player + boardX, boardY)) 
	//	{
	//		count++;
	//		boardX = (int)player + boardX;
	//	}

	//	SetPosition();
	//}

	protected override bool AttackLogic()
	{
		UnitForBattle target = null;
		if (battleManager.SearchInBoard((int)player + boardX, boardY, player, ref target))
		{
			Debug.Log(GetName() + " Attack!");
			target.TakeDamage(atk);
			// play attack animation
			return true;
		}

		return false;
	}

	protected override void DeathAttle()
	{

	}

	
}
