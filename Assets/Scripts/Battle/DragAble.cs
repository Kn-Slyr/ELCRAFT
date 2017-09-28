using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragAble : MonoBehaviour
{
	protected int positionX
	{
		get { return positionX; }
		set { positionX = value; }
	}
	protected int positionY
	{
		get { return positionY; }
		set { positionY = value; }
	}
	protected int cost;

	public abstract void AddQueue();
}
