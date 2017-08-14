using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{
	public enum AnimatorPlayMode
	{
		ForwardLoop,
		ReverseLoop,
		PlayOneShot,
		PingPong,
		MultiRow,

		Total
	}

	public enum LoopMode
	{
		OneShot,
		Loop,
		PingPong,

		Total
	}

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

	[Header("Play Mode")]
	[ContextMenuItem( "Update settings to this preset", "ChangeMode" )]
	public AnimatorPlayMode mode;
	public bool reversed;
	public LoopMode loopMode;
	public bool useMultiRow;

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
		if(Input.GetKey(KeyCode.Alpha1)) SwitchMode(AnimatorPlayMode.ForwardLoop);
		if(Input.GetKey(KeyCode.Alpha2)) SwitchMode(AnimatorPlayMode.ReverseLoop);
		if(Input.GetKey(KeyCode.Alpha3)) SwitchMode(AnimatorPlayMode.PlayOneShot);
		if(Input.GetKey(KeyCode.Alpha4)) SwitchMode(AnimatorPlayMode.PingPong);
		if(Input.GetKey(KeyCode.Alpha5)) SwitchMode(AnimatorPlayMode.MultiRow);

		RenderFrame();
	}

	void ChangeMode()
	{
		SwitchMode(mode);
	}

	void SwitchMode()
	{
		SwitchMode((AnimatorPlayMode)(((int)mode + 1) % (int)AnimatorPlayMode.Total));
	}

	void SwitchMode(AnimatorPlayMode newMode)
	{
		mode = newMode;

		timer = 0.0f;

		switch(mode)
		{
			case AnimatorPlayMode.ForwardLoop:
				reversed = false;
				loopMode = LoopMode.Loop;
				useMultiRow = false;
				break;
			case AnimatorPlayMode.ReverseLoop:
				reversed = true;
				loopMode = LoopMode.Loop;
				useMultiRow = false;
				break;
			case AnimatorPlayMode.PlayOneShot:
				reversed = false;
				loopMode = LoopMode.OneShot;
				useMultiRow = false;
				currentCol = 0;
				break;
			case AnimatorPlayMode.PingPong:
				reversed = false;
				loopMode = LoopMode.PingPong;
				useMultiRow = false;
				break;
			case AnimatorPlayMode.MultiRow:
				reversed = false;
				loopMode = LoopMode.Loop;
				useMultiRow = true;
				break;
		}
	}

	void RenderFrame()
	{
		Vector2 offset = new Vector2 (1.0f / colSize * (colSize - currentCol - 1), 1.0f / rowSize * (rowSize - currentRow - 1));

		if (timer < 1.0f / framesPerSecond)
		{
			timer += Time.deltaTime;
		}
		else
		{
			timer = 0.0f;

			if(reversed) currentCol--;
			else currentCol++;

			bool goNextRow = true;

			if(loopMode == LoopMode.Loop)
			{
				if(currentCol < 0)
				{
					currentCol = (colSize - 1); // Iterate back to last column
				}
				else if(currentCol >= colSize)
				{
					currentCol = 0; // Iterate back to first column
				}
				else
				{
					goNextRow = false;
				}
			}
			else
			{
				if(currentCol <= 0)
				{
					if(loopMode == LoopMode.PingPong && !useMultiRow) reversed = !reversed;
					currentCol = 0; // Stay at the first column
				}
				else if(currentCol >= (colSize - 1))
				{
					if(loopMode == LoopMode.PingPong && !useMultiRow) reversed = !reversed;
					currentCol = (colSize - 1); // Stay at the last column
				}
				else
				{
					goNextRow = false;
				}
			}

			if(useMultiRow)
			{
				if(goNextRow)
				{
					if(reversed) currentRow--;
					else currentRow++;
				}

				if(loopMode == LoopMode.Loop)
				{
					if(currentRow < 0)
					{
						currentRow = (rowSize - 1); // Iterate back to last row
					}
					else if(currentRow >= rowSize)
					{
						currentRow = 0; // Iterate back to first row
					}
				}
				else
				{
					if(currentRow <= 0)
					{
						if(loopMode == LoopMode.PingPong) reversed = !reversed;
						currentRow = 0; // Stay at the first row
					}
					else if(currentRow >= (rowSize - 1))
					{
						if(loopMode == LoopMode.PingPong) reversed = !reversed;
						currentRow = (rowSize - 1); // Stay at the last row
					}
				}
			}
		}

		GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", offset);
	}
}
