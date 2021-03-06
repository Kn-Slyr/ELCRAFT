﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct BoardInfo
{
	public const float boardHorMin = -17.5f, boardHorMax = 17.5f;
	public const float boardVerMin = -5.4f, boardVerMax = 5.4f;
	public const float boardOneBlockSize = 2.16f, boardHalfBlockSize = 1.08f;
}

public enum Phase { NONE, PLAY, PLAY_ING, SPAWN, SPAWN_ING, SKILL, SKILL_ING, PROCESS, PROCESS_ING };
public enum Mode { AI, PVP, MAKER }

public class BattleManager : MonoBehaviour
{
	public static BattleManager instance = null;

	public Mode mode;
	public Phase phase;
	
	public List<UnitForBattle> liveUnitList = new List<UnitForBattle>();
	public UnitForBattle[,] liveUnitListInBoard = new UnitForBattle[16, 5];
	public List<UnitForBattle> spawnUnitQueue = new List<UnitForBattle>();
	public List<SkillForBattle> commanderSkillQueue = new List<SkillForBattle>();
	public BattleStatus battleStat = new BattleStatus();
	private int uniqueUnitNumber = 0;

	private const float playerSpawnTimeLimit = 5f;   // not specfied, maybe 5s
	private const float spawnInterval = 0.5f;     // intervals between from spawn effect
	private const float skillInterval = 1f;    // intervals between from skill effect
	private const float battleInterval = 1f;    // intervals between from skill effect
	public float gameSpeed;

	private AIMaker aiMaker;
	private EnemyForAI enemyForAI;
	//private ServerPVP serverPVP;
	
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
		//battleStat = new BattleStat();
		battleStat.Init();

		if (mode == Mode.AI)
		{
			enemyForAI = new EnemyForAI();
			enemyForAI.DataLoadAndSetup("");
		}
		else if(mode == Mode.MAKER)
		{
			aiMaker = GetComponent<AIMaker>();
		}

		phase = Phase.PLAY;
	}

	// Update is called once per frame
	void Update()
	{
		switch(phase)
		{
			case Phase.PLAY:
				StartCoroutine(UserPlay());
				break;
			case Phase.SPAWN:
				StartCoroutine(UnitSpawn());
				break;
			case Phase.SKILL:
				StartCoroutine(SkillProcedure());
				break;
			case Phase.PROCESS:
				StartCoroutine(BattleProcedure());
				break;
			default:
				break;
		}
	}

	IEnumerator UserPlay()
	{
		// animation
		Debug.Log(battleStat.turnCount + " User Phase!");
		battleStat.SetupTurnUI(battleStat.turnCount);
		battleStat.RefreshMana();
		phase = Phase.PLAY_ING;
		yield return new WaitForSeconds(playerSpawnTimeLimit);

		phase = Phase.SPAWN;

		if (mode == Mode.AI)
		{
			enemyForAI.SetEnemyAction(battleStat.turnCount);
		}
		// if pvp, data to server
		// else if ai maker, save the values
		else if (mode != Mode.AI)
		{
			List<ActionData> doList = new List<ActionData>();
			foreach (UnitForBattle spawnUnit in spawnUnitQueue)
				doList.Add(new ActionData(battleStat.turnCount, (int)spawnUnit.actionCode, spawnUnit.boardX, spawnUnit.boardY));

			//if (mode == Mode.PVP)
			//	;
			//else 
			if (mode == Mode.MAKER)
				aiMaker.AddActionList(doList);
		}

	}

	// player's + enemy's units spawn for queue
	IEnumerator UnitSpawn()
	{
		Debug.Log(battleStat.turnCount + " Spawn Phase!");
		phase = Phase.SPAWN_ING;

		// enemy spawn data will be loaded from db
		// spawnQueue.add(loaded enemy's queue);
		yield return new WaitForSeconds(spawnInterval / gameSpeed);
		foreach (UnitForBattle spawnUnit in spawnUnitQueue)
		{
			spawnUnit.SpawnInField();
			yield return new WaitForSeconds(spawnInterval / gameSpeed);
		}

		spawnUnitQueue.Clear();
		phase = Phase.SKILL;
	}

	// commander's skill procedure, random order for enemy and player
	IEnumerator SkillProcedure()
	{
		Debug.Log(battleStat.turnCount + " Skill Phase!");
		phase = Phase.SKILL_ING;

		yield return new WaitForSeconds(skillInterval / gameSpeed);
		for (int i = 0; i < commanderSkillQueue.Count; i++)
		{
			// commanderSkillQueue[i].Operate();
			yield return new WaitForSeconds(skillInterval / gameSpeed);
		}

		commanderSkillQueue.Clear();
		phase = Phase.PROCESS;
	}

	// unit's indivisual skill, move procedure
	IEnumerator BattleProcedure()
	{
		Debug.Log(battleStat.turnCount + " Battle Phase!");
		phase = Phase.PROCESS_ING;
		// unitsInField sorted by unit's "speed" variables
		SetUnitRandValue();
		SortLiveUnitsBySpeed();	

		yield return new WaitForSeconds(battleInterval / gameSpeed);
		
		foreach (UnitForBattle unit in liveUnitList)
		{
			// do unit's own work
			unit.TurnProcess();
			yield return new WaitForSeconds(battleInterval / gameSpeed);
		}

		phase = Phase.PLAY;
		battleStat.TurnCountPlus();
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
