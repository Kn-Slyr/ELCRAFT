using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Kind { UNIT, SKILL };

public class DragAble : MonoBehaviour
{
	protected int boardX, boardY;
	private bool nowDrag;

	// this values have to be fixed for more specific finding with real images
	private const float boardHorMin = -17.5f, boardHorMax = 17.5f;
	private const float boardVerMin = -5.4f, boardVerMax = 5.4f;
	private const float boardOneBlockSize = 2.16f, boardHalfBlockSize = 1.08f;

	public UnitForBattle unit;
	public SkillForBattle skill;
	public Kind kind;
	private BattleManager battleManager;

	public void AddQueue()
	{
		Debug.Log("Place at ( " + boardX + ", " + boardY + " )");
		if(kind == Kind.SKILL)
		{

			skill.boardX = boardX;
			skill.boardY = boardY;
			battleManager.commanderSkillQueue.Add(skill);
		}
		else if(kind == Kind.UNIT)
		{
			unit.player = Player.USER;
			unit.boardX = boardX;
			unit.boardY = boardY;
			battleManager.spawnUnitQueue.Add(unit);
		}
	}

	private void Awake()
	{
		nowDrag = true;
		battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
		transform.position = FindMousePosition();
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
			PlaceObject();
	}
	
	private void PlaceObject()
	{
		nowDrag = false;
		if(battleManager.turn == Turn.USER && boardX != -1 && boardY != -1)
			AddQueue();

		DestroyObject(gameObject);
	}

	// return mouse position at the screen
	Vector3 FindMousePosition()
	{
		Vector3 ret = new Vector3();
		Camera camera = Camera.main;
		Vector2 mousePos = new Vector2();

		mousePos.x = Input.mousePosition.x;
		mousePos.y = camera.pixelHeight - Input.mousePosition.y;

		ret = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, camera.nearClipPlane));
		ret.y *= -1;
		ret.z = -1;

		return ret;
	}

	bool IsInBoard(Vector3 board, out int x, out int y)
	{
		if (board.x < boardHorMin || board.x > boardHorMax || board.y < boardVerMin || board.y > boardVerMax)
		{
			x = y = -1;
			return false;
		}

		x = (int)((board.x - boardHorMin) / boardOneBlockSize);
		y = (int)((board.y - boardVerMin) / boardOneBlockSize);

		return true;
	}

	protected void FixedUpdate()
	{
		if (nowDrag)
		{
			Vector3 mousePosition = FindMousePosition();
			if(IsInBoard(mousePosition, out boardX, out boardY))
			{
				mousePosition.x = boardHorMin + boardX * boardOneBlockSize + boardHalfBlockSize;
				mousePosition.y = boardVerMin + boardY * boardOneBlockSize + boardHalfBlockSize;
			}

			transform.position = mousePosition;
		}
	}
}
