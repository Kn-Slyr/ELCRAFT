﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragStart : MonoBehaviour
{
	public int cost;
	public GameObject unit;
	private BattleManager battleManager = BattleManager.instance;

	void Start ()
	{
		battleManager = BattleManager.instance;	// it have to be deleted
	}

	private void OnMouseDown()
	{
		if (battleManager.battleStat.UserMana < cost)
		{
			Debug.Log("No mana for spawn");
		}
		else if(battleManager.phase != Phase.PLAY_ING)
		{
			Debug.Log("It's not your turn");
		}
		else 
			Instantiate(unit);
	}
}
