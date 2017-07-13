using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public Transform contentRoot;
    public LeaderboardEntryRenderer templateRenderer;
    public List<GameObject> entryRenderers; //Holds newly duplicated entries
	
    void Start()
    {
        entryRenderers = new List<GameObject>();
    }

    public void LoadLeaderboard ()
    {
        //Refresh / Delete existing entries
        for(int e = 0; e < entryRenderers.Count; e++)
        {
            Destroy(entryRenderers[e]);
        }

        entryRenderers = new List<GameObject>();

        //Load new data
        EntryList entryList = DataManager.Instance.LoadData();

        //Sort List to descending order
        entryList.entries.Sort();

        //Iterate through the list
        for(int i = 0; i < entryList.entries.Count; i++)
        {
            //Duplicate templateRenderer
            GameObject go = Instantiate(templateRenderer.gameObject, contentRoot); //Reparent renderer

            entryRenderers.Add(go);

            //Rename duplicates
            go.name = "LeaderboardEntry (" + (i + 1) + ")";

            // Assign Variables to renderer
            go.GetComponent<LeaderboardEntryRenderer>().RenderEntry(i + 1, entryList.entries[i].name, entryList.entries[i].score);

            //Activate GOs
            go.SetActive(true);
        }

        //Anchor the content just in case
        contentRoot.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        templateRenderer.gameObject.SetActive(false);
	}
}
