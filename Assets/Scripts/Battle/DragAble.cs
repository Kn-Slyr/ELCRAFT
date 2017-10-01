using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragAble : MonoBehaviour
{
	protected int cost;
	protected int boardX, boardY;
	private bool nowDrag;
	private float boardHorMin, boardHorMax;
	private float boardVerMin, boardVerMax;
	private float boardOneBlockSize, boardHalfBlockSize;

	public abstract void AddQueue();

	private void OnMouseDown()
	{
		nowDrag = true;
	}

	private void OnMouseUp()
	{
		nowDrag = false;
		if(boardX != -1 && boardY != -1)
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
		ret.z = 0;

		return ret;
	}

	bool IsInBoard(Vector3 position, out int x, out int y)
	{
		if (position.x < boardHorMin || position.x > boardHorMax || position.y < boardVerMin || position.y > boardVerMax)
		{
			x = y = -1;
			return false;
		}

		x = (int)((position.x - boardHorMin) / boardOneBlockSize);
		y = (int)((position.y - boardVerMin) / boardOneBlockSize);

		return true;
	}

	protected virtual void FixedUpdate()
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
