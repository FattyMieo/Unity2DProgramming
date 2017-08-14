using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	public AudioClipID hitSound;
	
	// Update is called once per frame
	public void PlayHitSound ()
	{
		SoundManagerScript.Instance.PlaySFX(hitSound);
	}

	void Update()
	{
		transform.Translate(Vector3.down * Time.deltaTime);

		if(transform.position.y < -Camera.main.orthographicSize - transform.localScale.y) Destroy(gameObject);
	}
}
