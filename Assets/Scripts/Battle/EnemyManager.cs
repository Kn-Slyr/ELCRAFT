using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManager 
{
	private BattleManager battleManager = BattleManager.instance;
	private static readonly string[] UnitNames = {
		"None", "UnitForFire/Gladiator"
	};

	protected abstract void LoadActionData(int turnCount, out List<ActionData> actions);

	public void SetEnemyAction(int turnCount)
	{
		List<ActionData> actionList;
		LoadActionData(turnCount, out actionList);

		foreach(ActionData action in actionList)
		{
			if (action.actionCode > ActionData.UnitStart)
				AddUnitSpawnQueue(action);
			else if (action.actionCode > ActionData.SkillStart)
				AddCommanderSkillQueue(action);
			else if(action.actionCode == 1)
			{
				// @@ implement levelup 
			}
		}
	}

	protected void AddUnitSpawnQueue (ActionData action)
	{
		string path = "Prefabs/" + UnitNames[action.actionCode - ActionData.UnitStart] + "ForBattle";
		//Debug.Log("load path : " + path);
		GameObject unit = Resources.Load(path) as GameObject;
		UnitForBattle unitClass = unit.GetComponent<UnitForBattle>();
		unitClass.boardX = 15 - action.boardX;
		unitClass.boardY = action.boardY;
		unitClass.player = Player.ENEMY;

		// @@ check and change position when already exists
		MonoBehaviour.Instantiate(unit);
		battleManager.spawnUnitQueue.Add(unitClass);
	}

	protected void AddCommanderSkillQueue(ActionData action)
	{

	}
}
