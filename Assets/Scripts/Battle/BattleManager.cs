using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStat
{
	int playerMana;
	int playerMaxMana;
	int playerManaLevel;
	int enemyMana;
	int enemyMaxMana;
	int enemyManaLevel;

	int playerCoreHP;
	int enemyCoreHP;

	public void Init()
	{
		playerMana = 100;
		playerMaxMana = 300;
		playerManaLevel = 1;
		playerCoreHP = 1000;

		// enemy's stat will be called from db
	}
}

public class BattleManager : MonoBehaviour
{
	public static BattleManager instance = null;

	public enum Turn { PLAYER, SPAWN, SKILL, PROCEDURE, NONE };
	public Turn turn;

	public UnitInField[] unitsInField;
	public UnitInField[] spawnQueue;
	public BattleStat battleStat;

	public float playerSpawnTimeLimit;   // not specfied, maybe 5s
	private float playerSpawnTimer;

	private float spawnInteval;     // intervals between from spawn effect
	private float skillInterval;    // intervals between from skill effect
	private float battleInterval;    // intervals between from skill effect

	// Use this for initialization
	void Start()
	{
		battleStat.Init();
		playerSpawnTimer = Time.time + playerSpawnTimeLimit;
	}

	// Update is called once per frame
	void Update()
	{
		if (turn == Turn.NONE)
			return;
		else if (turn == Turn.PLAYER)
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
	void UnitSpawn()
	{
		turn = Turn.NONE;

		// enemy spawn data will be loaded from db
		// spawnQueue.add(loaded enemy's queue);
		for (int i = 0; i < spawnQueue.Length; i++)
		{
			// spawn for FIFO, Battlecry
			// yield return new WaitForSeconds(spawnInterval);
		}

		turn = Turn.SKILL;
	}

	// commander's skill procedure, random order for enemy and player
	void SkillProcedure()
	{
		turn = Turn.NONE;


	}

	// unit's indivisual skill, move procedure
	void BattleProcedure()
	{
		turn = Turn.NONE;

		// unitsInField sorted by unit's "speed" variables

		for (int i = 0; i < unitsInField.Length; i++) 
		{
			// do unit's own work
			// yield return new WaitForSeconds(battleInterval);
		}

		// setting for player's turn time
		playerSpawnTimer = Time.time + playerSpawnTimeLimit;
		turn = Turn.PLAYER;
	}
}
