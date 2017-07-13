using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntryRenderer : MonoBehaviour
{
    public Text rankLabel;
    public Text nameLabel;
    public Text scoreLabel;

    public void RenderEntry(int _rank, string _name, int _score)
    {
        rankLabel.text = _rank.ToString();
        nameLabel.text = _name;
        scoreLabel.text = _score.ToString();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
