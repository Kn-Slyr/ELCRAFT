using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragStart : MonoBehaviour
{
	public int cost;
	public GameObject unit;
	private BattleManager battleManager;

	void Start ()
	{
		battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
	}

	private void OnMouseDown()
	{
		if (battleManager.battleStat.userMana < cost)
		{
			Debug.Log("No mana for spawn");
		}
		else if(battleManager.turn == Turn.USER)
		{
			Debug.Log("It's not your turn");
		}
		else 
			Instantiate(unit);
	}

	private void FixedUpdate()
	{
		// tracking camera move
	}
}
