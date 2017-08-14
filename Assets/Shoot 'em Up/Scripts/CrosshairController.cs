using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
	public Vector3 rawMousePos;
	public Vector3 worldMousePos;
	// Update is called once per frame
	void Update ()
	{
		rawMousePos = Input.mousePosition;
		rawMousePos.z = 0;
		worldMousePos = Camera.main.ScreenToWorldPoint (rawMousePos);
		worldMousePos.z = 0;

		this.transform.position = worldMousePos;
		Cursor.visible = false;
	}
}
