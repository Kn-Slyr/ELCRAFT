using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject cam;
	public float speed;		// set 20 by unity3d inspector
	private bool nowDrag;
	private Vector3 offset;
	private float cameraLimit = 6.5f;

	private void Start()
	{
		nowDrag = false;
	}

	private void LateUpdate()
	{
		if(nowDrag)
		{
			Vector3 now = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			Vector3 t = now;
			now -= offset;
			now.y = now.z = 0;
			cam.transform.position -= now * speed;
			offset = t;

			OutOfRangeForCamera();
		}
	}

	private void OutOfRangeForCamera()
	{
		if (cam.transform.position.x < -cameraLimit)
			cam.transform.position = new Vector3(-cameraLimit, 0, -10);
		else if (cam.transform.position.x > cameraLimit)
			cam.transform.position = new Vector3(cameraLimit, 0, -10);
	}

	private void OnMouseDown()
	{
		nowDrag = true;
		offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
	}

	private void OnMouseUp()
	{
		nowDrag = false;
	}
}
