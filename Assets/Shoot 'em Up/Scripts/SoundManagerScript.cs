using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClipID
{
	BLUE = 0,
	RED,
	GREEN,
	BOSS,

	BGM,

	TOTAL
}

[System.Serializable]
public class AudioClipInfo
{
	public AudioClipID id;
	public AudioClip clip;
}

public class SoundManagerScript : MonoBehaviour
{
	public static SoundManagerScript Instance;

	public AudioSource bgmSource;
	public AudioSource sfxSource;

	public AudioClipInfo[] clips;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		PlayBGM(AudioClipID.BGM);
	}

	public AudioClip FindClip(AudioClipID id)
	{
		for(int i = 0; i < (int)AudioClipID.TOTAL; i++)
		{
			if(id == clips[i].id)
			{
				return clips[i].clip;
			}
		}
		return null;
	}

	public void PlaySFX(AudioClipID id)
	{
		sfxSource.PlayOneShot(FindClip(id));
	}

	public void PlayBGM(AudioClipID id)
	{
		bgmSource.clip = FindClip(id);
		bgmSource.Play();
	}
}
