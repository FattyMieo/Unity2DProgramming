using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{
	[Header("Sprite Setup")]
	public int colSize;
	public int rowSize;
	public int pixelPerUnit;

	[Header("Animation Control")]
	public int currentCol;
	public int currentRow;

	[Header("Time")]
	public float timer;
	public int framesPerSecond;

	// Use this for initialization
	void Start ()
	{
		Reslice();
	}
	
	void Reslice()
	{
		Vector2 tiling = new Vector2 (1.0f / colSize, 1.0f / rowSize);
		//Or MeshRenderer
		GetComponent<Renderer> ().material.SetTextureScale ("_MainTex", tiling);

		float sizeX = GetComponent<Renderer> ().material.mainTexture.width / colSize;
		float sizeY = GetComponent<Renderer> ().material.mainTexture.height / rowSize;

		transform.localScale = new Vector3 (sizeX / (float)pixelPerUnit, sizeY / (float)pixelPerUnit, 1.0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 offset = new Vector2 (1.0f / colSize * (colSize - currentCol - 1), 1.0f / rowSize * (rowSize - currentRow - 1));

		if (timer < 1.0f / framesPerSecond)
		{
			timer += Time.deltaTime;
		}
		else
		{
			timer = 0.0f;
			currentCol++;
			currentCol %= (colSize - 1); // Iterate back to first column
		}
		GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", offset);
	}
}
