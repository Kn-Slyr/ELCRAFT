using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitForSpawn : DragAble
{
	public GameObject unitForBattle;

	protected void SetUnitData()
	{
		unitForBattle.GetComponent<UnitForBattle>().boardX = boardX;
		unitForBattle.GetComponent<UnitForBattle>().boardY = boardY;
		unitForBattle.GetComponent<UnitForBattle>().player = Player.USER;
	}

	protected override void AddQueue()
	{
		if(CheckCondition())
		{
			battleManager.battleStat.UseUserMana(manaCost);
			
			SetUnitData();
			Instantiate(unitForBattle);
			// alpha to 50%
		}
	}

	protected override bool CheckCondition()
	{
		if (boardX > 1)
		{
			Debug.Log("out of spawn zone.");
			return false;
		}
		else if (!battleManager.IsEmptyInBoard(boardX, boardY))
		{
			Debug.Log("Already other unit exists in there.");
			return false;
		}
		return true;
	}
}
