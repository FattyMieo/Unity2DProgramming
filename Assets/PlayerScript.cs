using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public Vector2 speed;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		this.transform.Translate
		(
			new Vector3
			(
				Input.GetAxis ("Horizontal") * speedX,
				Input.GetAxis ("Vertical") * speedY,
				0.0f
			)
			* Time.deltaTime
		);
		*/
		this.transform.Translate(new Vector3(Input.GetAxis ("Horizontal") * speed.x, Input.GetAxis ("Vertical") * speed.y, 0.0f) * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.Space))
		{
			SpawnPoolManager.Instance.Spawn ("Bullet", this.transform.position, this.transform.rotation);
		}
	}
}
