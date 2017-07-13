using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Important:
//	Make class serializable
//	Make variables public

[System.Serializable]
public class EntryList
{
	public List<Entry> entries;

	public EntryList()
	{
		entries = new List<Entry>();
	}
}
