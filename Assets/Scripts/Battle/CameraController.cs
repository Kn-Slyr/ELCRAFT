using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject cam;
	public float dragSpeed;		// set 20 by unity3d inspector
	private bool nowDrag;
	private Vector3 offset;
	private float cameraLimit = 6.5f;
	public GameObject spawnIcons, skillIcons;

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
			now.x *= dragSpeed;
			now.y = now.z = 0;
			cam.transform.position -= now;
			offset = t;

			OutOfCameraView();
			spawnIcons.transform.position = cam.transform.position + new Vector3(cameraLimit, 0, 10);	// 10 is the value of camera's z position
			skillIcons.transform.position = cam.transform.position + new Vector3(cameraLimit, 0, 10);
		}
	}

	private void OutOfCameraView()
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
