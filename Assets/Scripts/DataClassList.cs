using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct StageData
{
	List<UnitSpawnData> doList;
}

[Serializable]
public struct UnitSpawnData
{
	int turn;
	int unitCode;
	int boardX, boardY;
}

[Serializable]
public struct CommanderSkillData
{
	int turn;
	int skillCode;
	int boardX, boardY;		// it is better for target unit?
}

