using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	public static BattleManager instance = null;

	public enum Turn { PLAYER, ENEMY, PROCEDURE };
	public Turn turn;

	public UnitInField[] unitsInField;
	public UnitInField[] spawnQueue;

	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
