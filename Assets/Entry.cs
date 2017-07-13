using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Important:
//	Make class serializable
//	Make variables public

[System.Serializable]
public class Entry : IComparable<Entry>
{
	public string name;
	public int score;

	public Entry (string _name, int _score)
	{
		name = _name;
		score = _score;
	}

    public int CompareTo(Entry other)
    {
        if(other == null) return -1;
        return this.name.CompareTo(other.name); //Alphabetical Order
        //return other.score - this.score; //Descending Order
    }
}
