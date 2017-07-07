using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoolManager : MonoBehaviour
{
	//Setup as a singleton
	#region Singleton // Just define what this block of code is about

	private static SpawnPoolManager _instance;
	public static SpawnPoolManager Instance
	{
		get
		{
			//CheckSingleton (); //Cannot refer to "this" script in static, has no instances found initially
			return _instance;
		}
	}

	private void CheckSingleton ()
	{
		if (_instance == null) //Assign this object to this reference
			_instance = this;
		else if (_instance == this) //Existed two or more instances, destroy duplicates
			Destroy(this.gameObject);
	}

	#endregion Singleton

	void Awake()
	{
		CheckSingleton ();
		DontDestroyOnLoad (gameObject); //Avoid destroying when switching to another scene
		InitializePoolManager (); // Run before any Start() runs in any other scripts
	}

	public float efficiency;
	public List<GameObject> objectsToPool;
	public List<int> numberOfObjectsToPool;

	private Dictionary<string, Stack<GameObject>> pool;

	//Initialize Pool Manager
	public void InitializePoolManager()
	{
		pool = new Dictionary<string, Stack<GameObject>> (); //Initialize the pool
		for (int i = 0; i < objectsToPool.Count; i++)
		{
			pool.Add (objectsToPool[i].name, new Stack<GameObject> ());
			for (int j = 0; j < numberOfObjectsToPool[i]; j++)
			{
				GameObject go = Instantiate (objectsToPool [i]) as GameObject;
				go.name = objectsToPool [i].name; //Rename it so that no "(Clone)" is behind newly instantiated gameobjects
				go.transform.SetParent (this.transform);
				go.SetActive (false);
				pool [objectsToPool [i].name].Push (go);
			}
		}
	}

	public void CalculateEfficiency()
	{
		int remaining = 0;
		int total = 0;
		for (int i = 0; i < objectsToPool.Count; i++)
		{
			total += numberOfObjectsToPool[i];
			remaining += pool [objectsToPool [i].name].Count;
		}
		efficiency = ((float)remaining / (float)total) * 100.0f;
	}

	//Spawn
	public void Spawn(string objectName, Vector3 newPos, Quaternion newRot)
	{
		if (!pool.ContainsKey (objectName))
		{
			Debug.LogWarning ("No pool for " + objectName + " exists!");
			return;
		}

		if (pool [objectName].Count > 0)
		{
			GameObject go = pool [objectName].Pop ();
			go.transform.position = newPos;
			go.transform.rotation = newRot;
			go.SetActive (true);
		}
		else
		{
			Debug.LogWarning ("Reached Pool Limit for " + objectName);
		}
		CalculateEfficiency ();
	}

	//Despawn
	public void Despawn(GameObject go)
	{
		go.SetActive (false);
		go.transform.position = Vector3.zero;
		go.transform.rotation = Quaternion.identity;
		pool [go.name].Push (go);

		CalculateEfficiency ();
	}
}
