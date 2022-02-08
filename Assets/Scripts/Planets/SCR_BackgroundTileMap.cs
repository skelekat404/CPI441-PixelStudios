using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SCR_BackgroundTileMap : MonoBehaviour
{
    public Tile backgroundTile;

    int backgroundWidth = 10;
    int backgroundHeight = 10;

    int[,] backgroundTileGrid;

    void Start()
    {
        GenerateBackgroundTiles();
    }
    void GenerateBackgroundTiles()
    {
        backgroundTileGrid = new int[backgroundWidth, backgroundHeight];


    }
    /*** Creates a background tile using a given ID at specified [x, y] coordinates ***/
    void CreateTerrainTile(int tileID, int x, int y)
    {
        //GameObject tileObject = terrainTiles[tileID];
        //GameObject terrainTile = Instantiate(tileObject);

        //terrainTile.transform.localPosition = new Vector3(x, y, 0);
    }
}
