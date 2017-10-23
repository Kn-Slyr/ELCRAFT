using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForAI : EnemyManager
{
	private List<ActionData> actionList;
	private int actionListIdx = 0; 

	public void DataLoadAndSetup(string fileName)
	{
		DataManager dataManager = new DataManager();
		dataManager.BinaryLoad<List<ActionData>>(fileName, out actionList);

		Debug.Log("Data Test : ");
		foreach(ActionData data  in actionList)
		{
			Debug.Log("in turn " + data.turnCount + " : " + data.actionCode + "(" + data.boardX + ", " + data.boardY + ")");
		}
	}

	protected override void LoadActionData(int turnCount, out List<ActionData> actions)
	{
		actions = new List<ActionData>();
		for (; actionListIdx < actionList.Count && actionList[actionListIdx].turnCount <= turnCount; actionListIdx++)
			actions.Add(actionList[actionListIdx]);
	}
	
}
