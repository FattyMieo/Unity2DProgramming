using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax3DScript : MonoBehaviour {

    Vector3 velocity = Vector3.zero;
	public float camSpeed;
    public Camera mgCam;
	public Camera bgCam;
	public Camera fgCam;
	public Camera skyCam;
	public Vector2 minClamp;
	public Vector2 maxClamp;

	// Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        velocity.x = Input.GetAxis("Horizontal");
        velocity.y = Input.GetAxis("Vertical");

		if(velocity.x < 0 && this.transform.position.x <= minClamp.x) velocity.x = 0;
		if(velocity.x > 0 && this.transform.position.x >= maxClamp.x) velocity.x = 0;
		if(velocity.y < 0 && this.transform.position.y <= minClamp.y) velocity.y = 0;
		if(velocity.y > 0 && this.transform.position.y >= maxClamp.y) velocity.y = 0;

		this.transform.Translate(velocity * camSpeed * Time.deltaTime);  

        //Zooming
        if(Input.GetKey(KeyCode.Space))
        {
            mgCam.orthographicSize = Mathf.MoveTowards(mgCam.orthographicSize, 3.0f, Time.deltaTime * 2.0f);
        }
        else
        {
            mgCam.orthographicSize = Mathf.MoveTowards(mgCam.orthographicSize, 5.0f, Time.deltaTime * 2.0f);
        }

		skyCam.fieldOfView = Mathf.Atan2(mgCam.orthographicSize, 10.0f) * Mathf.Rad2Deg * 2.0f;
		bgCam.fieldOfView = Mathf.Atan2(mgCam.orthographicSize, 10.0f) * Mathf.Rad2Deg * 2.0f;
		fgCam.fieldOfView = Mathf.Atan2(mgCam.orthographicSize, 10.0f) * Mathf.Rad2Deg * 2.0f;
	}
}
