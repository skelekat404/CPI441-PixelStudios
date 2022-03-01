using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_BackgroundTileMap : MonoBehaviour
{
    public GameObject backgroundTile;
    
    // *** Important to match these values with the width and height in TerrainGeneration script ***
    int backgroundWidth = 30;
    int backgroundHeight = 30;

    int[,] backgroundTileGrid;

    void Awake()
    {
        GenerateBackgroundTiles();
    }

    /*** Generates the background tiles for the grid ***/
    void GenerateBackgroundTiles()
    {
        backgroundTileGrid = new int[backgroundWidth, backgroundHeight];

        for (int y = 0; y < backgroundTileGrid.GetLength(0); y++)
        {
            for (int x = 0; x < backgroundTileGrid.GetLength(1); x++)
            {
                CreateBackgroundTile(x, y);
            }
        }
    }
    /*** Creates a background tile at specified [x, y] coordinates ***/
    void CreateBackgroundTile(int x, int y)
    {
        GameObject bgTile = Instantiate(backgroundTile);

        bgTile.transform.localPosition = new Vector3(x, y, 1);
    }
}