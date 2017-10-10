using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player { USER = 1, ENEMY = -1 };

public abstract class UnitForBattle : MonoBehaviour, IComparer<UnitForBattle>
{
	private int unitCode;     // @@use enum
	protected int skillStack;
	protected int nowHp, maxHp;
	protected int atk;
	protected int shield;
	protected int speed;
	protected int randValue;
	protected float damageReduce;
	public Player player;
	public int boardX, boardY;
	protected BattleManager battleManager;
	//public static Random rand = new Random();

	protected virtual void Start()
	{
		// is this good? or i have to public and add by inspector?
		battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();	
		skillStack = 0;
		nowHp = maxHp;
		shield = maxHp / 5;
		// one turn shield for 20% of max hp
	}

	public void TurnProcess()
	{
		if (!AttackLogic())
			MoveLogic();
	}

	public void SpawnInField()
	{
		// animation
		BattleCry();
	}

	protected abstract bool MoveLogic();
	protected abstract bool AttackLogic();
	protected abstract void BattleCry();

	protected virtual void DeathAttle()
	{
		// delete live queue
		// play animation
	}
	
	// return (true : dead, false : live)
	public bool TakeDamage(int damage)
	{
		damage = (int)(damage * (1 - damageReduce));

		if (shield > 0)
		{
			if (shield > damage)
			{
				shield -= damage;
				damage = 0;
			}
			else
			{
				damage -= shield;
				shield = 0;
			}
			// shield animation
		}

		if (damage > 0)
		{
			if (nowHp > damage)
			{
				nowHp -= damage;
				// hp reduce animation
			}
			else
			{
				nowHp = 0;
				DeathAttle();
				return true;
			}
		}
		return false;	
	}
	// public special effect

	public void RandomValueSetUp()
	{
		randValue = Random.Range(0, 1000);
	}

	public int Compare(UnitForBattle a, UnitForBattle b)
	{
		if (a.speed == b.speed)
		{
			if (a.randValue > b.randValue)
				return 1;
			else if (a.randValue < b.randValue)
				return -1;
		}
		else if (a.speed > b.speed)
			return 1;
		else if (a.speed < b.speed)
			return -1;

		return 0;
	}
	
}
