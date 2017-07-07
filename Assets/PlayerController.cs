using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.GetComponent<Animator> ().speed = Mathf.Abs(Input.GetAxis ("Horizontal")) + 0.5f;
		if (Input.GetAxis ("Horizontal") < 0)
		{
			this.GetComponent<Animator> ().Play ("RunLeft");
		}
		else if (Input.GetAxis ("Horizontal") > 0)
		{
			this.GetComponent<Animator> ().Play ("RunRight");
		}
	}
}
