using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct StageData
{
	List<ActionData> doList;
}

public enum ActionCode : int
{
	NONE = 0, 
	LEVEL = 1,
	FireGladiator = 101
};

[Serializable]
public struct ActionData
{
	[NonSerialized] public static readonly int UnitStart = 100;     // unit code will be start from 101~ 
	[NonSerialized] public static readonly int SkillStart = 10;		// skill code will be start from 10~ 99
	public int turnCount;
	public int actionCode;
	public int boardX, boardY;

	public ActionData(int _turnCount, int _actionCode, int _boardX, int _boardY)
	{
		turnCount = _turnCount;
		actionCode = _actionCode;
		boardX = _boardX;
		boardY = _boardY;
	}
}

