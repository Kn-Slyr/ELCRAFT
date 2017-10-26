using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BattleStatus
{
	[NonSerialized] private int[] manaMaxValue = { 0, 500, 800, 1200, 1800 };
	[NonSerialized] private int[] requirToAssimilate = { 0, 200, 300, 500 };
	private int maxLevel = 4;
	private float manaRegenPercent = .15f;

	private int userMana;
	public int UserMana { get { return userMana; } }

	private int userManaLevel;
	public int UserManaLevel { get { return userManaLevel; } }

	public int enemyMana;
	public int enemyManaLevel;

	public int userCoreHP;
	public int enemyCoreHP;

	public int turnCount = 1;

	public Text userManaUI;
	public Text userManaLevelUI;
	public Text turnCounterUI;
	public Button userAssimilateButton;

	public void Init()
	{
		userMana = 0;
		userManaLevel = 1;
		userCoreHP = 100;
		SetupManaUI();
		SetupManaLevelUI();

		userAssimilateButton.onClick.AddListener(Assimilate);
		// @@enemy's stat will be called from db
	}

	public void UseUserMana(int cost)
	{
		userMana -= cost;
		SetupManaUI();
	}

	public void Assimilate()
	{
		if (userManaLevel < maxLevel && userMana > requirToAssimilate[userManaLevel])
		{
			UseUserMana(requirToAssimilate[userManaLevel]);
			userManaLevel++;
			SetupManaUI();
			SetupManaLevelUI();
		}
		else
		{
			Debug.Log("Not enough to assimilate");
		}
	}

	public void RefreshMana()
	{
		userMana += (int)(manaMaxValue[userManaLevel] * manaRegenPercent);
		if (userMana > manaMaxValue[userManaLevel])
			userMana = manaMaxValue[userManaLevel];
		SetupManaUI();
	}

	private void SetupManaUI()
	{
		userManaUI.text = userMana + " / " + manaMaxValue[userManaLevel];
	}

	private void SetupManaLevelUI()
	{
		userManaLevelUI.text = userManaLevel.ToString();
	}

	public void SetupTurnUI(int nowTurn)
	{
		turnCounterUI.text = nowTurn.ToString();
	}

	public void TurnCountPlus()
	{
		turnCount++;
	}
}