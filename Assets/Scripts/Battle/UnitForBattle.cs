using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player { USER, ENEMY };

public abstract class UnitForBattle : MonoBehaviour
{
	private int unitCode;     // @@use enum
	protected int skillStack;
	private int hp;
	private int shield;
	private float damageReduce;
	public Player player
	{
		get { return player; }
		set { player = value; }
	}
	public int positionX
	{
		get { return positionX; }
		set { positionX = value; }
	}
	public int positionY
	{
		get { return positionY; }
		set { positionY = value; }
	}

	private void Start()
	{
		skillStack = 0;
	}

	public void TurnProcess()
	{
		if (!MoveLogic())
			AttackLogic();
	}

	public abstract bool MoveLogic();
	public abstract void AttackLogic();
	public abstract void BattleCry();
	public abstract void DeathAttle();
	
	// public special effect
}
