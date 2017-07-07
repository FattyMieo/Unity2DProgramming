using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public float speed;
	public float limitY;

	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector3.up * Time.deltaTime * speed);

		if (this.transform.position.y > limitY)
			SpawnPoolManager.Instance.Despawn (gameObject);
	}
}
