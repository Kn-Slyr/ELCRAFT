using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillForBattle : DragAble
{
	protected int leftTurn;
	protected int manaCost;

	protected override void AddQueue()
	{
		if (CheckCondition())
		{
			player = Player.USER;
			battleManager.battleStat.userMana -= manaCost;
			battleManager.commanderSkillQueue.Add(this);
		}
	}

	protected abstract void SkillProcess();
}
