using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxClassicScript : MonoBehaviour
{
    public GameObject spriteA;
    public GameObject spriteB;
    Vector3 velocity = Vector3.zero;
    public float parallaxFactor = 1.0f;
    Vector3 threshold;

	// Use this for initialization
	void Start ()
    {
        //Calculate threshold
        threshold = Vector3.zero;
        threshold.x = spriteA.GetComponent<SpriteRenderer>().bounds.size.x;

		//Create a copy of sprite A and assign as a reference into sprite B
        spriteB = Instantiate(spriteA, this.transform);
        spriteB.transform.Translate(threshold);
	}
	
	// Update is called once per frame
	void Update ()
    {
        ParallaxMovement();
        ParallaxOffset();
    }

    void ParallaxMovement()
    {
        velocity.x = Input.GetAxis("Horizontal") * Time.deltaTime * parallaxFactor;
        spriteA.transform.Translate(velocity);
        spriteB.transform.Translate(velocity);
    }

    void ParallaxOffset()
    {
        if(spriteA.transform.position.x < -threshold.x)
        {
            spriteA.transform.Translate(threshold * 2);
        }
        else if(spriteA.transform.position.x > threshold.x)
        {
            spriteA.transform.Translate(-threshold * 2);
        }
        else if(spriteB.transform.position.x < -threshold.x)
        {
            spriteB.transform.Translate(threshold * 2);
        }
        else if(spriteB.transform.position.x > threshold.x)
        {
            spriteB.transform.Translate(-threshold * 2);
        }
    }
}
