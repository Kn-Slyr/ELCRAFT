using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitForSpawn : DragAble
{
	protected int manaCost;
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
			battleManager.battleStat.userMana -= manaCost;
			battleManager.spawnUnitQueue.Add(this);

			SetUnitData();
			Instantiate(unitForBattle);
			// alpha to 50%
			Debug.Log("AddQueue : (" + boardX + ", " + boardY + ")");
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

	public void SpawnInField()
	{
		// spawn animation, alpha to 100%
		BattleCry();	// abstract method
	}

	// None for normal units
	protected virtual void BattleCry()
	{

	}
}
