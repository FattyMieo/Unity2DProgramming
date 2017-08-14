using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
	KEYBOARD,
	MOUSE,
	GAMEPAD,

	TOTAL
}

public class Spaceship : MonoBehaviour
{
	public InputType inputType;
	public GameObject bullet; // Bullet Prefab Object
	public CrosshairController crosshair;

	private float countdown = 0;
	public float cooldown = 1f;

	public float speed = 3f;
	public float rotSpeed = 15f;

	public Sprite[] inputIndicatorSprites;
	public SpriteRenderer rend;

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
		if (countdown < cooldown)
		{
			countdown += Time.deltaTime;
		}

		if(Input.GetKeyDown(KeyCode.Return))
		{
			int newType = (int)inputType + 1;
			if(newType >= (int)InputType.TOTAL) newType = 0;
			inputType = (InputType)newType;
		}

		rend.sprite = inputIndicatorSprites[(int)inputType];

		switch(inputType)
		{
			case InputType.KEYBOARD:

				MovementWithVelocity(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

				if(Input.GetButton("Space"))
				{
					Firing();
				}
				break;
			case InputType.GAMEPAD:
				
				MovementWithVelocity(Input.GetAxis("HorizontalGamepad"), Input.GetAxis("VerticalGamepad"));

				if(Input.GetButton("Fire1Gamepad"))
				{
					Firing();
				}
				break;
			case InputType.MOUSE:

				MovementWithPosition(crosshair.transform.position.x, crosshair.transform.position.y);

				if(Input.GetButton("Fire1"))
				{
					Firing();
				}
				break;
		}
	}

//	void Orientation(float velX)
//	{
//		/*
//		Vector3 direction = crosshair.worldMousePos - this.transform.position;
//		direction.z = 0;
//		transform.LookAt(direction, Vector3.up);
//		*/
//
//		Vector3 direction = crosshair.transform.position - this.transform.position;
//		direction.Normalize ();
//		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
//		this.transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(0f, 0f, angle - 90), Time.deltaTime * rotSpeed);
//	}

	void MovementWithPosition(float posX, float posY)
	{
		Vector3 position = Vector3.zero;
		position.x = posX;
		position.y = posY;

		this.transform.position = position;
	}

	void MovementWithVelocity(float velX, float velY)
	{

		Vector3 velocity = Vector3.zero;
		velocity.x = velX;
		velocity.y = velY;

		this.transform.Translate(velocity * Time.deltaTime * speed, Space.World);
	}

	void Firing()
	{
		if (countdown >= cooldown)
		{
			Instantiate (bullet, this.transform.position, this.transform.rotation);
			countdown = 0;
		}
	}
}
