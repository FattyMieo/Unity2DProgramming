using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowScript : MonoBehaviour
{
	Camera cam;
	Color[] rainbowColor = new Color[]
	{
			Color.red,
			Color.yellow,
			Color.green,
			Color.cyan,
			Color.blue,
			Color.magenta
	};
	int currentIndex = 0;
	int nextIndex = 1;
	public float colorChangeTimer;
	public float colorChangeDuration;

	// Use this for initialization
	void Start ()
	{
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		colorChangeTimer += Time.deltaTime;

		if (colorChangeTimer >= colorChangeDuration)
		{
			currentIndex = nextIndex;
			nextIndex = (nextIndex + 1) % rainbowColor.Length;
			colorChangeTimer = 0.0f;
		}

		cam.backgroundColor = Color.Lerp (rainbowColor[currentIndex], rainbowColor[nextIndex], colorChangeTimer / colorChangeDuration);
	}
}
