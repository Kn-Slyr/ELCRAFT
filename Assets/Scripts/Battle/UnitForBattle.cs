using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player : int { USER = 1, ENEMY = -1 };

public abstract class UnitForBattle : MonoBehaviour //, IComparer, System.IComparable
{
	public ActionCode actionCode;
	public int unitNumber;
	protected int skillStack;
	protected int nowHp, maxHp;
	protected int atk;
	protected int shield;
	protected int speed;
	protected int moveRange;
	public int randValue;
	protected float damageReduce;
	public Player player;
	public int boardX, boardY;
	protected BattleManager battleManager = BattleManager.instance;

	public int GetSpeed() { return speed; }
	public int GetRandValue() { return randValue; }
	public string GetName() { return gameObject.name + "(" + unitNumber + ")"; }

	protected virtual void Awake()
	{
		transform.SetParent(GameObject.Find("UnitsInField").transform, false);
		skillStack = 0;
		unitNumber = battleManager.GetUnitNumber();
		nowHp = maxHp;
		// one turn shield for 20% of max hp
		if(player == Player.USER)
			transform.localScale = new Vector3(1, 1, 1);
		else if(player == Player.ENEMY)
			transform.localScale = new Vector3(-1, 1, 1);
		SetPosition();

		battleManager.spawnUnitQueue.Add(this.GetComponent<UnitForBattle>());
		battleManager.liveUnitList.Add(this.GetComponent<UnitForBattle>());
	}

	public virtual void SpawnInField()
	{
		// animation
		BattleCry();
	}

	public virtual void BattleCry()
	{
		// ex) shield buff
		// shield = maxHp / 5;
	}

	public void TurnProcess()
	{
		Debug.Log(GetName() + "\'s turn process");
		if (!AttackLogic())
			MoveLogic();
	}

	// If unit has special move logic, it will implement with override
	protected virtual void MoveLogic()
	{
		Debug.Log(GetName() + " Normal Move!");
		// simple moving, have to change
		RemovePosition();

		int count = 0;
		while (count < moveRange && battleManager.IsEmptyInBoard((int)player + boardX, boardY))
		{
			count++;
			boardX = (int)player + boardX;
		}

		SetPosition();
	}

	protected abstract bool AttackLogic();

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

	public void SetRandomValue()
	{
		randValue = Random.Range(0, 1000);
	}

	protected void RemovePosition()
	{
		battleManager.liveUnitListInBoard[boardX, boardY] = null;
	}
	protected void SetPosition()
	{
		Vector3 position = new Vector3();
		position.x = BoardInfo.boardHorMin + BoardInfo.boardOneBlockSize * boardX + BoardInfo.boardHalfBlockSize;
		position.y = BoardInfo.boardVerMin + BoardInfo.boardOneBlockSize * boardY + BoardInfo.boardHalfBlockSize;
		position.z = -1;

		transform.position = position;
		battleManager.liveUnitListInBoard[boardX, boardY] = this.GetComponent<UnitForBattle>();
	}
}
