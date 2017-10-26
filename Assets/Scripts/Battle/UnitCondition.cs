using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitConditionKind : int
{
	NONE = 0,
	BUFF = 1, 
	DEBUFF = 2,
	SHIELD = 3,
	DAMAGE = 4,
	STACK = 5
}

public abstract class UnitCondition
{
	protected int leftTurn;
	protected UnitConditionKind kind;
	private UnitForBattle host;

	public UnitCondition(UnitForBattle _host, int _leftTurn)
	{
		host = _host;
		leftTurn = _leftTurn;
	}

	public virtual bool TurnProcess(ref int value)
	{
		leftTurn--;
		return leftTurn != 0;
	}

}
