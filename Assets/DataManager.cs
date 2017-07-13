using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Save and Load data

public class DataManager : MonoBehaviour
{
    private static DataManager mInstance;
    public static DataManager Instance
    {
        get
        {
            return mInstance;
        }
    }

    void Awake()
    {
        if(mInstance == null)
        {
            mInstance = this;
        }
        else if(mInstance != this)
        {
            Destroy(gameObject);
        }
    }

	[Header("Test Interface")]
	public string testName;
	public int testScore;

	[Header("Settings")]
	string filePath = "leaderboard1";
	public EntryList previewData;

	[ContextMenu("Test Save Data")] //Test without going to play mode, right click the component at the gameobject and run it
	void SavaData ()
	{
		//Get Data Path
		//string dataPath = Application.dataPath; //Unconsistent path depending on platforms
		//string dataPath = Application.persistentDataPath; //Best way
		string dataPath = Path.Combine(Application.streamingAssetsPath, filePath + ".json"); //Streaming Assets (Develop only)

		EntryList entryList = new EntryList();

		//Populate existing data if available
		if(File.Exists(dataPath))
		{
			entryList = LoadData();
		}

		entryList.entries.Add(new Entry(testName, testScore)); //Add a new entry

		string jsonString = JsonUtility.ToJson (entryList, true); //Convert EntryListdata to json format

		//Write all data to a text/json file
		File.WriteAllText (dataPath, jsonString);

		//Update Preview
		previewData = entryList;

		//Refreshs the asset database to bring in our new json file, shortcut is Ctrl + R
		UnityEditor.AssetDatabase.Refresh ();
	}

	[ContextMenu("Test Load Data")] //Test without going to play mode, right click the component at the gameobject and run it
    public EntryList LoadData ()
	{
		string dataPath = Path.Combine(Application.streamingAssetsPath, filePath + ".json"); //Streaming Assets (Develop only)

		string jsonString = File.ReadAllText (dataPath);

		previewData = JsonUtility.FromJson<EntryList>(jsonString);
		return previewData;
	}

	[ContextMenu("Test Clear Data")] //Test without going to play mode, right click the component at the gameobject and run it
	void ResetLeaderboard ()
	{
		string dataPath = Path.Combine(Application.streamingAssetsPath, filePath + ".json"); //Streaming Assets (Develop only)

		EntryList entryList = new EntryList();

		//
		if(File.Exists(dataPath))
		{
			string jsonString = JsonUtility.ToJson(entryList);
			File.WriteAllText(dataPath, jsonString);
			previewData = entryList;
		}
	}

    [ContextMenu("Delete Last Entry")]
    void DeleteLastEntry()
    {
        //Get Data Path
        //string dataPath = Application.dataPath; //Unconsistent path depending on platforms
        //string dataPath = Application.persistentDataPath; //Best way
        string dataPath = Path.Combine(Application.streamingAssetsPath, filePath + ".json"); //Streaming Assets (Develop only)

        EntryList entryList = new EntryList();

        //Populate existing data if available
        if(File.Exists(dataPath))
        {
            entryList = LoadData();
        }

        entryList.entries.RemoveAt(entryList.entries.Count - 1); //Add a new entry

        string jsonString = JsonUtility.ToJson (entryList, true); //Convert EntryListdata to json format

        //Write all data to a text/json file
        File.WriteAllText (dataPath, jsonString);

        //Update Preview
        previewData = entryList;

        //Refreshs the asset database to bring in our new json file, shortcut is Ctrl + R
        UnityEditor.AssetDatabase.Refresh ();
    }
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
