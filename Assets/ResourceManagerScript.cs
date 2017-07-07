using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		string path = "Managers/SpawnPoolManager";
		GameObject origin = Resources.Load (path) as GameObject;
		GameObject go = Instantiate (origin);
		go.name = origin.name;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.L))
		{
			SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 2);
		}
	}
}
