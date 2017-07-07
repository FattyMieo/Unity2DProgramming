using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
	public GameObject bullet; // Bullet Prefab Object
	public CrosshairController crosshair;

	private float countdown = 0;
	public float cooldown = 1f;

	public float speed = 3f;
	public Vector3 velocity;
	public float rotSpeed = 15f;

	void Awake()
	{
		Debug.Log ("System AWAKENED!");
	}

	void OnEnable()
	{
		Debug.Log ("ENABLING system...");
	}

	// Use this for initialization
	void Start()
	{
		Debug.Log ("System ONLINE!");
	}
	
	// Update is called once per frame
	void Update()
	{
		Orientation();
		Movement();
		Firing();
	}

	void Orientation()
	{
		/*
		Vector3 direction = crosshair.worldMousePos - this.transform.position;
		direction.z = 0;
		transform.LookAt(direction, Vector3.up);
		*/

		Vector3 direction = crosshair.transform.position - this.transform.position;
		direction.Normalize ();
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
		this.transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(0f, 0f, angle - 90), Time.deltaTime * rotSpeed);
	}

	void Movement()
	{
		/*
		if (Input.GetKey (KeyCode.A))
		{
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		}

		if (Input.GetKey (KeyCode.D))
		{
			transform.Translate (Vector3.right * Time.deltaTime * speed);
		}
		*/

		velocity.x = Input.GetAxis("Horizontal");
		velocity.y = Input.GetAxis("Vertical");

		//this.transform.Translate(velocity * Time.deltaTime * speed);
		this.transform.Translate(velocity * Time.deltaTime * speed, Space.World);
	}

	void Firing()
	{
		if (countdown < cooldown)
		{
			countdown += Time.deltaTime;
		}

		if (Input.GetButton("Fire1") && countdown >= cooldown)
		{
			Instantiate (bullet, this.transform.position, this.transform.rotation);
			countdown = 0;
		}
	}
}
