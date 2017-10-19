using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class BattleStat
{
	public int userMana;
	public int userMaxMana;
	public int userManaLevel;
	public int enemyMana;
	public int enemyMaxMana;
	public int enemyManaLevel;

	public int userCoreHP;
	public int enemyCoreHP;

	public void Init()
	{
		userMana = 100;
		userMaxMana = 300;
		userManaLevel = 1;
		userCoreHP = 1000;

		// @@enemy's stat will be called from db
	}
}

public class BoardInfo
{
	public const float boardHorMin = -17.5f, boardHorMax = 17.5f;
	public const float boardVerMin = -5.4f, boardVerMax = 5.4f;
	public const float boardOneBlockSize = 2.16f, boardHalfBlockSize = 1.08f;
}

public enum Turn { USER, SPAWN, SPAWNING, SKILL, SKILLING, PROCESS, PROCESSING, NONE };

public class BattleManager : MonoBehaviour
{
	public static BattleManager instance = null;

	public Turn turn = Turn.USER;
	public int turnCount = 0;

	public List<UnitForBattle> liveUnitList = new List<UnitForBattle>();
	public UnitForBattle[,] liveUnitListInBoard = new UnitForBattle[16, 5];
	public List<UnitForSpawn> spawnUnitQueue = new List<UnitForSpawn>();
	public List<SkillForBattle> commanderSkillQueue = new List<SkillForBattle>();
	public BattleStat battleStat;
	private int uniqueUnitNumber = 0;

	public float playerSpawnTimeLimit;   // not specfied, maybe 5s
	private float playerSpawnTimer;

	private const float spawnInterval = 1f;     // intervals between from spawn effect
	private const float skillInterval = 1f;    // intervals between from skill effect
	private const float battleInterval = 1f;    // intervals between from skill effect
	public float gameSpeed;

	// Use this for initialization
	private void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		InitBattle();
	}

	void InitBattle()
	{
		playerSpawnTimer = Time.time + playerSpawnTimeLimit;

		battleStat = new BattleStat();
		battleStat.Init();
	}

	// Update is called once per frame
	void Update()
	{
		if (turn == Turn.NONE)
			return;
		else if (turn == Turn.USER)
		{
			if (playerSpawnTimer < Time.time)
				turn = Turn.SPAWN;
		}
		else if (turn == Turn.SPAWN)
			StartCoroutine(UnitSpawn());
		else if (turn == Turn.SKILL)
			StartCoroutine(SkillProcedure());
		else if (turn == Turn.PROCESS)
			StartCoroutine(BattleProcedure());
	}


	// player's + enemy's units spawn for queue
	IEnumerator UnitSpawn()
	{
		Debug.Log(turnCount + " Spawn Phase!");
		turn = Turn.SPAWNING;

		// enemy spawn data will be loaded from db
		// spawnQueue.add(loaded enemy's queue);
		yield return new WaitForSeconds(spawnInterval / gameSpeed);
		foreach (UnitForSpawn spawnUnit in spawnUnitQueue)
		{
			spawnUnit.SpawnInField();
			yield return new WaitForSeconds(spawnInterval / gameSpeed);
		}

		spawnUnitQueue.Clear();
		turn = Turn.SKILL;
	}

	// commander's skill procedure, random order for enemy and player
	IEnumerator SkillProcedure()
	{
		Debug.Log(turnCount + " Skill Phase!");
		turn = Turn.SKILLING;

		yield return new WaitForSeconds(skillInterval / gameSpeed);
		for (int i = 0; i < commanderSkillQueue.Count; i++)
		{
			// commanderSkillQueue[i].Operate();
			yield return new WaitForSeconds(skillInterval / gameSpeed);
		}

		commanderSkillQueue.Clear();
		turn = Turn.PROCESS;
	}

	// unit's indivisual skill, move procedure
	IEnumerator BattleProcedure()
	{
		Debug.Log(turnCount + " Battle Phase!");
		turn = Turn.PROCESSING;
		// unitsInField sorted by unit's "speed" variables
		SetUnitRandValue();
		SortLiveUnitsBySpeed();	//@@@@whyyyyyyyyyyyyy

		yield return new WaitForSeconds(battleInterval / gameSpeed);
		
		foreach (UnitForBattle unit in liveUnitList)
		{
			// do unit's own work
			unit.TurnProcess();
			yield return new WaitForSeconds(battleInterval / gameSpeed);
		}

		// setting for player's turn time
		playerSpawnTimer = Time.time + playerSpawnTimeLimit;
		turn = Turn.USER;
		turnCount++;
	}

	void SetUnitRandValue()
	{
		foreach (UnitForBattle unit in liveUnitList)
			unit.SetRandomValue();
	}

	void SortLiveUnitsBySpeed()
	{
		liveUnitList.Sort(delegate(UnitForBattle a, UnitForBattle b)
		{
			if (a.GetSpeed() == b.GetSpeed())
			{
				if (a.GetRandValue() > b.GetRandValue())
					return 1;
				else if (a.GetRandValue() < b.GetRandValue())
					return -1;
			}
			else if (a.GetSpeed() > b.GetSpeed())
				return 1;
			else if (a.GetSpeed() < b.GetSpeed())
				return -1;

			return 0;
		});
	}

	public bool SearchInBoard(int x, int y, Player me, ref UnitForBattle target)
	{
		Debug.Log("search and destroy!!");
		if (x < 0 || x > 15 || y < 0 || y > 4)
			return false;

		if (liveUnitListInBoard[x, y] && liveUnitListInBoard[x, y].player != me)
		{
			target = liveUnitListInBoard[x, y];
			return true;
		}

		return false;
	}

	public bool IsEmptyInBoard(int x, int y)
	{
		return liveUnitListInBoard[x, y] == null;
	}

	public int GetUnitNumber()
	{
		return uniqueUnitNumber++;
	}
}
