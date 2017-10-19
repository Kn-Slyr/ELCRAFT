using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// have to delete, it's sample, only for has BattleCry or special summon condition
public class GladiatorForSpawn : UnitForSpawn
{
	protected override void BattleCry()
	{

	}

	protected override bool CheckCondition()
	{
		return base.CheckCondition();
	}
}
