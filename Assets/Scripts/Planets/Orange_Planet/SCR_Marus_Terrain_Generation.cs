using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Marus_Terrain_Generation : MonoBehaviour
{
// *** Background Tiles ***
    Dictionary<int, GameObject> backgroundTiles;
    public GameObject charcoalFloor;
    public GameObject lavaWhole;

    // *** Terrain Tiles ***
    Dictionary<int, GameObject> terrainTiles;
    public GameObject transparentTile;
    public GameObject tempRocks;
    public GameObject lavaRocks;

    // *** Map Size ***
    int mapWidth = 30;
    int mapHeight = 30;

    int[,] backgroundTileGrid;
    int[,] mapGrid;

    int coordinateOffest = 100; // used so the tilemaps of each planet don't overlap each other with the new scene management/network stuff

    void Awake()
    {
        DefineTerrainTiles();
        GenerateBackgroundTiles();
        GenerateTerrainTiles();
    }
    /*** Organizes the different terrain tiles in a dictionary for easy referencing ***/
    void DefineTerrainTiles()
    {
        backgroundTiles = new Dictionary<int, GameObject>();
        backgroundTiles.Add(0, charcoalFloor);
        backgroundTiles.Add(1, lavaWhole);

        terrainTiles = new Dictionary<int, GameObject>();
        terrainTiles.Add(0, transparentTile);
        terrainTiles.Add(1, tempRocks);
        terrainTiles.Add(2, lavaRocks);
    }

    /*** Handles generating the map of background tiles ***/
    void GenerateBackgroundTiles()
    {
        // *** Background Tile Generation ***
        backgroundTileGrid = new int[mapHeight, mapWidth];

        /*
                for (int y = 0; y < backgroundTileGrid.GetLength(0); y++)
                {
                    for (int x = 0; x < backgroundTileGrid.GetLength(1); x++)
                    {
                        CreateBackgroundTile(0, x, y);
                    }
                }
        */

        // *** Manually setting the first tile as lavanderFloor, position [0, 0] (bottom left corner of the grid) ***
        backgroundTileGrid[0, 0] = 0;
        CreateBackgroundTile(0, (0 + coordinateOffest), (0 + coordinateOffest)); // lavanderFloor at [0, 0]

        // Extra space for spaceship
        backgroundTileGrid[1, 0] = 0;
        CreateBackgroundTile(0, (1 + coordinateOffest), (0 + coordinateOffest)); // lavanderFloor at [1, 0]

        // *** First Generate the background in the first row ***
        int tileIDFirstRow = 0;
        for (int x = 2; x < mapWidth; x++) // starts at the second element in the first row since the first is already set
        {
            tileIDFirstRow = GenerateBackgroundNeighborID(backgroundTileGrid[0, x - 1]); // generates a new tile based on the previous one in the row
            backgroundTileGrid[0, x] = tileIDFirstRow; // adds the ID of the generated tile to the map grid
            CreateBackgroundTile(tileIDFirstRow, (x + coordinateOffest), (0 + coordinateOffest)); // creates the tile for the given ID at the specified [x, y] position
        }

        // *** Generate the rest of the terrain after the first row ***
        int tileIDRest = 0;
        for (int y = 1; y < backgroundTileGrid.GetLength(0); y++) // starts at the 2nd row
        {
            for (int x = 0; x < backgroundTileGrid.GetLength(1); x++)
            {
                if (x == 0) // checks if the loop reaches the first item of a row (start of a new row)
                {
                    tileIDRest = GenerateBackgroundNeighborID(backgroundTileGrid[y - 1, x]);
                    backgroundTileGrid[y, x] = tileIDRest;
                    CreateBackgroundTile(tileIDRest, (x + coordinateOffest), (y + coordinateOffest));
                }
                else // if not, then the neighbor used to generate a new terrain tile is
                     // either the title one row below, or one column to the left
                {
                    int neighborChoice = Random.Range(1, 2); // 50/50 chance for either tile to be chosen

                    switch (neighborChoice)
                    {
                        case 1:
                            tileIDRest = GenerateBackgroundNeighborID(backgroundTileGrid[y - 1, x]); // selects the tile one row below
                            backgroundTileGrid[y, x] = tileIDRest;
                            CreateBackgroundTile(tileIDRest, (x + coordinateOffest), (y + coordinateOffest));
                            break;
                        case 2:
                            tileIDRest = GenerateBackgroundNeighborID(backgroundTileGrid[y, x - 1]); // selects the tile one column to the left
                            backgroundTileGrid[y, x] = tileIDRest;
                            CreateBackgroundTile(tileIDRest, (x + coordinateOffest), (y + coordinateOffest));
                            break;
                    }
                }
            }
        }
    }

    /*** Handles generating the map of terrain tiles ***/
    void GenerateTerrainTiles()
    {
        // *** Resource Tile Generation ***
        mapGrid = new int[mapHeight, mapWidth];

        // *** Manually setting the first tile as lavanderFloor, position [0, 0] (bottom left corner of the grid) ***
        mapGrid[0, 0] = 0;
        CreateTerrainTile(0, (0 + coordinateOffest), (0 + coordinateOffest)); // lavanderFloor at [0, 0]

        // Extra space for spaceship
        backgroundTileGrid[1, 0] = 0;
        CreateBackgroundTile(0, (1 + coordinateOffest), (0 + coordinateOffest)); // lavanderFloor at [1, 0]

        // *** First Generate the terrain in the first row ***
        int tileIDFirstRow = 0;
        for (int x = 2; x < mapWidth; x++) // starts at the second element in the first row since the first is already set
        {
            if (backgroundTileGrid[0, x] == 0) // checks if the background tile is a lavanderFloor tile
            {
                tileIDFirstRow = GenerateNeighborID(mapGrid[0, x - 1]); // generates a new tile based on the previous one in the row
                mapGrid[0, x] = tileIDFirstRow; // adds the ID of the generated tile to the map grid
                CreateTerrainTile(tileIDFirstRow, (x + coordinateOffest), (0 + coordinateOffest)); // creates the tile for the given ID at the specified [x, y] position
            }
        }

        // *** Generate the rest of the terrain after the first row ***
        int tileIDRest = 0;
        for (int y = 1; y < mapGrid.GetLength(0); y++) // starts at the 2nd row
        {
            for (int x = 0; x < mapGrid.GetLength(1); x++)
            {
                if (backgroundTileGrid[y, x] == 0) // checks if the background tile is a lavanderFloor tile
                {
                    if (x == 0) // checks if the loop reaches the first item of a row (start of a new row)
                    {
                        tileIDRest = GenerateNeighborID(mapGrid[y - 1, x]);
                        mapGrid[y, x] = tileIDRest;
                        CreateTerrainTile(tileIDRest, (x + coordinateOffest), (y + coordinateOffest));
                    }
                    else // if not, then the neighbor used to generate a new terrain tile is
                         // either the title one row below, or one column to the left
                    {
                        int neighborChoice = Random.Range(1, 2); // 50/50 chance for either tile to be chosen

                        switch (neighborChoice)
                        {
                            case 1:
                                tileIDRest = GenerateNeighborID(mapGrid[y - 1, x]); // selects the tile one row below
                                mapGrid[y, x] = tileIDRest;
                                CreateTerrainTile(tileIDRest, (x + coordinateOffest), (y + coordinateOffest));
                                break;
                            case 2:
                                tileIDRest = GenerateNeighborID(mapGrid[y, x - 1]); // selects the tile one column to the left
                                mapGrid[y, x] = tileIDRest;
                                CreateTerrainTile(tileIDRest, (x + coordinateOffest), (y + coordinateOffest));
                                break;
                        }
                    }
                }
            }
        }
    }

    /*** Generates and returns an ID for a neighboring background tile using defined probabilities for each type of terrain tile ***/
    int GenerateBackgroundNeighborID(int tileID)
    {
        int randomInt = 0;
        int bgNeighborID = 0; // stores the ID for the generated neighbor

        switch (tileID)
        {
            case 0: // lavanderFloor
                randomInt = Random.Range(1, 100);

                if (randomInt >= 0 && randomInt < 95) // 95% chance of generating lavanderFloor
                {
                    bgNeighborID = 0;
                }
                else if (randomInt >= 95 && randomInt < 100) // 5% chance of generating cloudWhole
                {
                    bgNeighborID = 1;
                }
                else
                {
                    bgNeighborID = 0;
                }
                break;

            case 1: // cloudWhole
                randomInt = Random.Range(1, 100);
                
                if (randomInt >= 0 && randomInt < 40) // 40% chance of generating lavanderFloor
                {
                    bgNeighborID = 0;
                }
                else if (randomInt >= 40 && randomInt < 100) // 60% chance of generating cloudWhole
                {
                    bgNeighborID = 1;
                }
                else
                {
                    bgNeighborID = 0;
                }
                break;
        }
        return bgNeighborID; // returns the ID for the generated neighbor based on the spawn percentages
    }

    /*** Generates and returns an ID for a neighboring terrain tile using defined probabilities for each type of terrain tile ***/
    int GenerateNeighborID(int tileID)
    {
        int randomInt = 0;
        int neighborID = 0; // stores the ID for the generated neighbor

        switch (tileID)
        {
            case 0: // transparent
                randomInt = Random.Range(1, 100);

                if (randomInt >= 0 && randomInt < 90) // 90% chance of generating transparent
                {
                    neighborID = 0;
                }
                else if (randomInt >= 90 && randomInt < 95) // 5% chance of generating tempRock
                {
                    neighborID = 1;
                }
                else if (randomInt >= 95 && randomInt < 100) // 5% chance of generating lavaRock
                {
                    neighborID = 2;
                }
                else
                {
                    neighborID = 0;
                }
                break;

            case 1: // tempRock
                randomInt = Random.Range(1, 100);

                if (randomInt >= 0 && randomInt < 80) // 80% chance of generating transparent
                {
                    neighborID = 0;
                }
                else if (randomInt >= 80 && randomInt < 100) // 20% chance of generating tempRock
                {
                    neighborID = 1;
                }
                else
                {
                    neighborID = 0;
                }
                break;

            case 2: // lavaRock
                randomInt = Random.Range(1, 100);

                if (randomInt >= 0 && randomInt < 80) // 80% chance of generating transparent
                {
                    neighborID = 0;
                }
                else if (randomInt >= 80 && randomInt < 100) // 20% chance of generating lavaRock
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

    /*** Creates a background tile using a given ID at specified [x, y] coordinates ***/
    void CreateBackgroundTile(int tileID, int x, int y)
    {
        GameObject tileObject = backgroundTiles[tileID];
        GameObject bgTile = Instantiate(tileObject);

        bgTile.transform.localPosition = new Vector3(x, y, 1);
    }

    /*** Creates a terrain tile using a given ID at specified [x, y] coordinates ***/
    void CreateTerrainTile(int tileID, int x, int y)
    {
        GameObject tileObject = terrainTiles[tileID];
        GameObject terrainTile = Instantiate(tileObject);

        terrainTile.transform.localPosition = new Vector3(x, y, 0);
    }
}
