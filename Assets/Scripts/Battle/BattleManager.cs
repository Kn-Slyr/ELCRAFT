﻿using System.Collections;
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

public enum Turn { USER, SPAWN, SKILL, PROCEDURE, NONE };

public class BattleManager : MonoBehaviour
{
	public static BattleManager instance = null;

	public Turn turn;

	public List<UnitForBattle> liveUnitList;
	public UnitForBattle[,] liveUnitListInBoard;
	public List<UnitForBattle> spawnUnitQueue;
	public List<SkillForBattle> commanderSkillQueue;
	public BattleStat battleStat;

	public float playerSpawnTimeLimit;   // not specfied, maybe 5s
	private float playerSpawnTimer;

	private float spawnInterval;     // intervals between from spawn effect
	private float skillInterval;    // intervals between from skill effect
	private float battleInterval;    // intervals between from skill effect

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
		battleStat = new BattleStat();
		battleStat.Init();
		playerSpawnTimer = Time.time + playerSpawnTimeLimit;
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
			UnitSpawn();
		else if (turn == Turn.SKILL)
			SkillProcedure();
		else if (turn == Turn.PROCEDURE)
			BattleProcedure();
	}


	// player's + enemy's units spawn for queue
	IEnumerator UnitSpawn()
	{
		turn = Turn.NONE;

		// enemy spawn data will be loaded from db
		// spawnQueue.add(loaded enemy's queue);
		for (int i = 0; i < spawnUnitQueue.Count; i++)
		{
			// spawn for FIFO, Battlecry
			yield return new WaitForSeconds(spawnInterval);
		}

		turn = Turn.SKILL;
	}

	// commander's skill procedure, random order for enemy and player
	void SkillProcedure()
	{
		turn = Turn.NONE;

		for (int i = 0; i < commanderSkillQueue.Count; i++)
		{

		}
	}

	// unit's indivisual skill, move procedure
	void BattleProcedure()
	{
		turn = Turn.NONE;

		// unitsInField sorted by unit's "speed" variables

		for (int i = 0; i < liveUnitList.Count; i++) 
		{
			// do unit's own work
			liveUnitList[i].TurnProcess();
			//yield return new WaitForSeconds(battleInterval);
		}

		// setting for player's turn time
		playerSpawnTimer = Time.time + playerSpawnTimeLimit;
		turn = Turn.USER;
	}

	public bool InBoardSearch(int x, int y, Player who, out UnitForBattle target)
	{
		if (liveUnitListInBoard[x, y] != null && liveUnitListInBoard[x, y].player != who)
		{
			target = liveUnitListInBoard[x, y];
			return true;
		}

		target = null;
		return false;
	}
}
