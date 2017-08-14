using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEngineScript : MonoBehaviour
{
    //Y = Rows // X = Columns
    public int mapWidth = 1;
    public int mapHeight = 1;

    public Sprite[] tileSprites;
    private GameObject [,] tileRefs = new GameObject[0, 0];
    private int [,] tileMap = new int[0, 0];
    private Vector3 offset = Vector3.zero;
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            RandomizeTiles();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            HardcodeTiles();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadTilesFromText();
        }
	}

    void ResetTilemap()
    {
        tileMap = new int[0,0];

        for(int row = 0; row < tileRefs.GetLength(0); row++)
        {
            for(int col = 0; col < tileRefs.GetLength(1); col++)
            {
                Destroy(tileRefs[row, col]);
            }
        }
    }

    void RecalculateOffsets()
    {
        offset = Vector3.zero;
        offset.x = -(tileRefs.GetLength(1) - 1) / 2.0f;
        offset.y = (tileRefs.GetLength(0) - 1) / 2.0f;
    }

    void RenderTiles()
    {
        //Determine size of map
        tileRefs = new GameObject[tileMap.GetLength(0), tileMap.GetLength(1)];

        //Recalculate offsets
        RecalculateOffsets();

        //Load a tile
        for(int row = 0; row < tileRefs.GetLength(0); row++)
        {
            for(int col = 0; col < tileRefs.GetLength(1); col++)
            {
                GameObject tile = Instantiate(Resources.Load("Tile")) as GameObject;
                //Assign tile sprite
                tile.GetComponent<TileScript>().spriteRenderer.sprite = tileSprites[tileMap[row, col]];
                tile.transform.position = new Vector3(col, -row, 0) + offset;
                tileRefs[row, col] = tile;
            }
        }
    }

    [ContextMenu("Randomize Tiles")]
    void RandomizeTiles()
    {
        //Reset the tilemap
        ResetTilemap();

        //Define size
        tileMap = new int[mapHeight, mapWidth];

        //Assign random sprite index for each cell in the TileMap
        for(int row = 0; row < tileMap.GetLength(0); row++)
        {
            for(int col = 0; col < tileMap.GetLength(1); col++)
            {
                tileMap[row, col] = Random.Range(0, tileSprites.Length);
            }
        }

        //Render the tiles
        RenderTiles();
    }

    [ContextMenu("Hardcode Tiles")]
    void HardcodeTiles()
    {
        //Reset the tilemap
        ResetTilemap();

        //Define tiles
        tileMap = new int[3, 3]
        {
            {0, 1, 2},
            {11, 12, 13},
            {22, 23, 24}
        };

        mapWidth = tileMap.GetLength(1);
        mapHeight = tileMap.GetLength(0);

        //Render the tiles
        RenderTiles();
    }

    [ContextMenu("Load Tiles From Text")]
    void LoadTilesFromText()
    {
        //Reset the tilemap
        ResetTilemap();

        //Read from file
        TextAsset textFile = Resources.Load("Tilemap") as TextAsset;

        string data = textFile.text;

        //get width
        mapWidth = int.Parse(FindData(data, "width"));

        //get height
        mapHeight = int.Parse(FindData(data, "height"));

        //Define size
        tileMap = new int[mapHeight, mapWidth];

        //get data
        Debug.Log(data.Substring(data.IndexOf("data") + 7));
        string[] mapDataRow = data.Substring(data.IndexOf("data") + 7).Split('\n');

        for(int row = mapHeight - 1; row >= 0; row--)
        {
            Debug.Log(mapDataRow[row]);
            string[] mapDataCol = mapDataRow[row].Split(',');

            for(int col = mapWidth - 1; col >= 0; col--)
            {
                Debug.Log(mapDataCol[col]);
                //populate tilemap with data
                tileMap[row, col] = int.Parse(mapDataCol[col]) - 1;
            }
        }
        
        //Render the tiles
        RenderTiles();
    }

    string FindData(string data, string findingData)
    {
        data = data.Substring(data.IndexOf(findingData) + findingData.Length + 1);
        data = data.Substring(0, data.IndexOf("\n"));

        return data;
    }
}
