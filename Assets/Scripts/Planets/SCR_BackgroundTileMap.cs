using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_BackgroundTileMap : MonoBehaviour
{
    public GameObject backgroundTile;

    int backgroundWidth = 10;
    int backgroundHeight = 10;

    int[,] backgroundTileGrid;

    void Start()
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
        GameObject terrainTile = Instantiate(backgroundTile);

        terrainTile.transform.localPosition = new Vector3(x, y);
    }
}