using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_TerrainGeneration : MonoBehaviour
{
    Dictionary<int, GameObject> terrainTiles;

    public GameObject grass;
    public GameObject rocks;
    public GameObject trees;

    int mapWidth = 10;
    int mapHeight = 10;

    int[,] mapGrid;

    void Start()
    {
        DefineTerrainTiles();
        GenerateTerrainTiles();
    }
    /*** Organizes the different terrain tiles in a dictionary for easy referencing ***/
    void DefineTerrainTiles()
    {
        terrainTiles = new Dictionary<int, GameObject>();
        terrainTiles.Add(0, grass);
        terrainTiles.Add(1, rocks);
        terrainTiles.Add(2, trees);
    }

    /*** Handles generating the map of terrain tiles ***/
    void GenerateTerrainTiles()
    {
        mapGrid = new int[mapHeight, mapWidth];

        int randomInt = Random.Range(1, 3); // used to decide the first terrainTile generated

        switch (randomInt)
        {
            case 1:
                mapGrid[0, 0] = 0;
                CreateTerrainTile(0, 0, 0); // grass at [0, 0]
                break;
            case 2:
                mapGrid[0, 0] = 1;
                CreateTerrainTile(1, 0, 0);
                break;
            case 3:
                mapGrid[0, 0] = 2;
                CreateTerrainTile(2, 0, 0);
                break;
        }

        // *** First Generate the terrain in the first row ***
        int tileIDFirstRow = 0;
        for (int x = 1; x < mapWidth; x++) // starts at the second element in the first row since the first is already set
        {
            tileIDFirstRow = GenerateNeighborID(mapGrid[0, x - 1]); // generates a new tile based on the previous one in the row
            mapGrid[0, x] = tileIDFirstRow; // adds the ID of the generated tile to the map grid
            CreateTerrainTile(tileIDFirstRow, x, 0); // creates the tile for the given ID at the specified [x, y] position
        }

        // *** Generate the rest of the terrain after the first row ***
        int tileIDRest = 0;
        for (int y = 1; y < mapGrid.GetLength(0); y++) // starts at the 2nd row
        {
            for (int x = 0; x < mapGrid.GetLength(1); x++)
            {
                if (x == 0) // checks if the loop reaches the first item of a row (start of a new row)
                {
                    tileIDRest = GenerateNeighborID(mapGrid[y - 1, x]);
                    mapGrid[y, x] = tileIDRest;
                    CreateTerrainTile(tileIDRest, x, y);
                }
                else // if not, then the neighbor used to generate a new terrain tile is
                     // either the title one row above, or one column to the left
                {
                    int neighborChoice = Random.Range(1, 2); // 50/50 chance for either tile to be chosen

                    switch (neighborChoice)
                    {
                        case 1:
                            tileIDRest = GenerateNeighborID(mapGrid[y - 1, x]);
                            mapGrid[y, x] = tileIDRest;
                            CreateTerrainTile(tileIDRest, x, y);
                            break;
                        case 2:
                            tileIDRest = GenerateNeighborID(mapGrid[y, x - 1]);
                            mapGrid[y, x] = tileIDRest;
                            CreateTerrainTile(tileIDRest, x, y);
                            break;
                    }
                }
            }
        }
    }

    /*** Generates and returns an ID for a neighboring terrain tile using defined probabilities for each type of terrain tile ***/
    int GenerateNeighborID(int tileID)
    {
        int randomInt = 0;
        int neighborID = 0; // stores the ID for the generated neighbor

        switch (tileID)
        {
            case 0: // Grass
                randomInt = Random.Range(1, 100);

                if (randomInt >= 0 && randomInt < 80) // 80% chance of generating Grass
                {
                    neighborID = 0;
                }
                else if (randomInt >= 80 && randomInt < 90) // 10% chance of generating Rocks
                {
                    neighborID = 1;
                }
                else if (randomInt >= 90 && randomInt < 100) // 10% chance of generating Trees
                {
                    neighborID = 2;
                }
                else
                {
                    neighborID = 0;
                }
                break;

            case 1: // Rocks
                randomInt = Random.Range(1, 100);

                if (randomInt >= 0 && randomInt < 70) // 70% chance of generating Grass
                {
                    neighborID = 0;
                }
                else if (randomInt >= 70 && randomInt < 99) // 29% chance of generating Rocks
                {
                    neighborID = 1;
                }
                else if (randomInt >= 99 && randomInt < 100) // 1% chance of generating Trees
                {
                    neighborID = 2;
                }
                else
                {
                    neighborID = 0;
                }
                break;

            case 2: // Trees
                randomInt = Random.Range(1, 100);

                if (randomInt >= 0 && randomInt < 70) // 80% chance of generating Grass
                {
                    neighborID = 0;
                }
                else if (randomInt >= 70 && randomInt < 71) // 1% chance of generating Rocks
                {
                    neighborID = 1;
                }
                else if (randomInt >= 71 && randomInt < 100) // 29% chance of generating Trees
                {
                    neighborID = 2;
                }
                else
                {
                    neighborID = 0;
                }
                break;
        }
        return neighborID; // returns the ID for the generated neighbor based on the spawn percentages
    }

    /*** Creates a terrain tile using a given ID at specified [x, y] coordinates ***/
    void CreateTerrainTile(int tileID, int x, int y)
    {
        GameObject tileObject = terrainTiles[tileID];
        GameObject terrainTile = Instantiate(tileObject);

        terrainTile.transform.localPosition = new Vector3(x, y, 0);
    }
}