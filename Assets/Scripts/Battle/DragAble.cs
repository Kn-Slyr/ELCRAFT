using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragAble : MonoBehaviour
{
	public int boardX, boardY;
	private bool nowDrag;

	protected BattleManager battleManager = BattleManager.instance;
	protected abstract void AddQueue();
	protected abstract bool CheckCondition();
	protected Player player;

	protected void Awake()
	{
		nowDrag = true;
		transform.position = FindMousePosition();
	}

	protected void Update()
	{
		if (Input.GetMouseButtonUp(0))
			PlaceObject();
	}
	
	private void PlaceObject()
	{
		nowDrag = false;
		if (battleManager.turn != Turn.PLAY_ING)
		{
			Debug.Log("Too late to drop");
		}
		else if(boardX == -1)
		{
			Debug.Log("Not proper position");
		}
		else
			AddQueue();

		DestroyObject(gameObject);
	}

	// return mouse position at the screen
	private Vector3 FindMousePosition()
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

	private bool IsInBoard(Vector3 board, out int x, out int y)
	{
		if (board.x < BoardInfo.boardHorMin || board.x > BoardInfo.boardHorMax || 
			board.y < BoardInfo.boardVerMin || board.y > BoardInfo.boardVerMax)
		{
			x = y = -1;
			return false;
		}

		x = (int)((board.x - BoardInfo.boardHorMin) / BoardInfo.boardOneBlockSize);
		y = (int)((board.y - BoardInfo.boardVerMin) / BoardInfo.boardOneBlockSize);

		return true;
	}

	protected void FixedUpdate()
	{
		if (nowDrag)
		{
			Vector3 mousePosition = FindMousePosition();
			if(IsInBoard(mousePosition, out boardX, out boardY))
			{
				mousePosition.x = BoardInfo.boardHorMin + boardX * BoardInfo.boardOneBlockSize + BoardInfo.boardHalfBlockSize;
				mousePosition.y = BoardInfo.boardVerMin + boardY * BoardInfo.boardOneBlockSize + BoardInfo.boardHalfBlockSize;
			}

			transform.position = mousePosition;
		}
	}
}
