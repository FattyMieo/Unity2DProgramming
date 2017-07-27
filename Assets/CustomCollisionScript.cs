using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollisionScript : MonoBehaviour
{
    SpriteRenderer spr;
    public CustomCollisionScript other;

    public bool isPixelCollision = false;
    private bool isColliding = false;
    public bool debugMode = false;

	// Use this for initialization
	void Start ()
    {
        this.spr = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!isPixelCollision)
        {
            if(this.spr.bounds.Intersects(other.spr.bounds))
            {
                isColliding = true;
            }
            else
            {
                isColliding = false;
            }
        }

        DebugRenderer();
    }

    //Render sprite red if collison is detected
    void DebugRenderer()
    {
        if(debugMode)
        {
            if(isColliding)
            {
                spr.color = Color.red;
            }
            else
            {
                spr.color = Color.white;
            }
        }
    }
}
