using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitForSpawn : MonoBehaviour
{
	DragNDrop drag;

	bool CanSpawn()
	{
		return true;
	}

	void Spawn()
	{

	}
}
