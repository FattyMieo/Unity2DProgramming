using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
	public float cooldownTimer;
	public float cooldownDuration;

	public GameObject[] spawnedObjects;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		cooldownTimer += Time.deltaTime;
		if(cooldownTimer >= cooldownDuration)
		{
			cooldownTimer = 0.0f;
			RandomSpawn();
		}
	}

	public void RandomSpawn()
	{
		int rand = Random.Range(0, spawnedObjects.Length);

		float screenHeight = Camera.main.orthographicSize;
		float screenWidth = Camera.main.orthographicSize / Screen.height * Screen.width;

		float posX = Random.Range(-screenWidth + 1, screenWidth - 1);

		Instantiate(spawnedObjects[rand], new Vector3(posX, screenHeight + transform.localScale.y, 0.0f), Quaternion.identity);
	}
}
