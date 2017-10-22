using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManager : MonoBehaviour
{
	private BattleManager battleManager = BattleManager.instance;
	private static readonly string[] UnitNames = {
		"None", "UnitForFire/Gladiator"
	};

	protected abstract List<ActionData> LoadActionData(int turnCount);

	public void SetEnemyAction(int turnCount)
	{
		List<ActionData> actionList = LoadActionData(turnCount);
		foreach(ActionData action in actionList)
		{
			if (action.actionCode > 100)
				AddUnitSpawnQueue(action);
			else if (action.actionCode > 10)
				AddCommanderSkillQueue(action);
			else if(action.actionCode == 1)
			{
				// @@ implement levelup 
			}
		}
	}

	protected void AddUnitSpawnQueue (ActionData action)
	{
		string path = "Prefabs/" + UnitNames[action.actionCode];
		GameObject unit = Resources.Load(path) as GameObject;
		UnitForBattle unitClass = unit.GetComponent<UnitForBattle>();
		unitClass.boardX = 14 - action.boardX;
		unitClass.boardY = action.boardY;
		unitClass.player = Player.ENEMY;

		// @@ check and change position when already exists
		Instantiate(unit);
		battleManager.spawnUnitQueue.Add(unitClass);
	}

	protected void AddCommanderSkillQueue(ActionData action)
	{

	}
}
