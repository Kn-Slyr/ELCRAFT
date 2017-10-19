using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePageLoader : MonoBehaviour
{

	void Start ()
	{
		BattleManager.instance.turn = Turn.PLAY;
	}
	
	void Update ()
	{
		
	}
}
