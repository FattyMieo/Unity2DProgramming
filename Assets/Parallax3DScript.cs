using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax3DScript : MonoBehaviour {

    Vector3 velocity = Vector3.zero;
    public Camera mgCam;
    public Camera bgCam;
    public Camera fgCam;

	// Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        velocity.x = Input.GetAxis("Horizontal");
        velocity.y = Input.GetAxis("Vertical");

        this.transform.Translate(velocity * Time.deltaTime * 3.0f);

        //Zooming
        if(Input.GetKey(KeyCode.Space))
        {
            mgCam.orthographicSize = Mathf.MoveTowards(mgCam.orthographicSize, 3.0f, Time.deltaTime * 2.0f);
        }
        else
        {
            mgCam.orthographicSize = Mathf.MoveTowards(mgCam.orthographicSize, 5.0f, Time.deltaTime * 2.0f);
        }

        bgCam.fieldOfView = Mathf.Atan2(mgCam.orthographicSize, 10.0f) * Mathf.Rad2Deg * 2.0f;
        fgCam.fieldOfView = Mathf.Atan2(mgCam.orthographicSize, 10.0f) * Mathf.Rad2Deg * 2.0f;
	}
}
