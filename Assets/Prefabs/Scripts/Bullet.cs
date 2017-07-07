using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float bulletSpeed = 1f;

	// Use this for initialization
	void Start ()
	{
		Destroy (gameObject, 3f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//transform.Translate (new Vector3 (0f, Time.deltaTime * bulletSpeed, 0f));
		transform.Translate (Vector3.up * Time.deltaTime * bulletSpeed);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//if (!other.CompareTag("Player"))
		if (other.CompareTag ("Enemy"))
		{
			other.transform.localScale *= 0.99f;

			if (other.transform.localScale.x < 0.05f)
				Destroy (other.gameObject);
		}
	}
}
