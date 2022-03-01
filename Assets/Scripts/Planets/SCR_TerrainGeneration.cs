using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_TerrainGeneration : MonoBehaviour
{
    // *** Background Tiles ***
    Dictionary<int, GameObject> backgroundTiles;
    public GameObject grass;
    public GameObject riverVertical;
    public GameObject riverHorizontal;
    public GameObject riverLLCorner;
    public GameObject riverLRCorner;
    public GameObject riverTLCorner;
    public GameObject riverTRCorner;

    // *** Terrain Tiles ***
    Dictionary<int, GameObject> terrainTiles;
    public GameObject transparentTile;
    public GameObject rocks;
    public GameObject trees;

    // *** Important to match these values with the width and height in BackgroundTileMap script ***
    int mapWidth = 30;
    int mapHeight = 30;

    int[,] backgroundTileGrid;
    int[,] mapGrid;

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
        backgroundTiles.Add(0, grass);
        backgroundTiles.Add(1, riverVertical);
        backgroundTiles.Add(2, riverHorizontal);
        backgroundTiles.Add(3, riverLLCorner);
        backgroundTiles.Add(4, riverLRCorner);
        backgroundTiles.Add(5, riverTLCorner);
        backgroundTiles.Add(6, riverTRCorner);

        terrainTiles = new Dictionary<int, GameObject>();
        terrainTiles.Add(0, transparentTile);
        terrainTiles.Add(1, rocks);
        terrainTiles.Add(2, trees);
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

        // *** Manually setting the first tile as grass, position [0, 0] (bottom left corner of the grid) ***
        backgroundTileGrid[0, 0] = 0;
        CreateBackgroundTile(0, 0, 0); // grass at [0, 0]

        // *** First Generate the background in the first row ***
        int tileIDFirstRow = 0;
        for (int x = 1; x < mapWidth; x++) // starts at the second element in the first row since the first is already set
        {
            tileIDFirstRow = GenerateBackgroundNeighborID(backgroundTileGrid[0, x - 1], 5); // generates a new tile based on the previous one in the row
            backgroundTileGrid[0, x] = tileIDFirstRow; // adds the ID of the generated tile to the map grid
            CreateBackgroundTile(tileIDFirstRow, x, 0); // creates the tile for the given ID at the specified [x, y] position
        }

        // *** Generate the rest of the terrain after the first row ***
        int tileIDRest = 0;
        for (int y = 1; y < backgroundTileGrid.GetLength(0); y++) // starts at the 2nd row
        {
            for (int x = 0; x < backgroundTileGrid.GetLength(1); x++)
            {
                if (x == 0) // checks if the loop reaches the first item of a row (start of a new row)
                {
                    tileIDRest = GenerateBackgroundNeighborID(backgroundTileGrid[y - 1, x], 5);
                    backgroundTileGrid[y, x] = tileIDRest;
                    CreateBackgroundTile(tileIDRest, x, y);
                }
                else // if not, then the neighbor used to generate a new terrain tile is
                     // either the title one row below, or one column to the left
                {
                    int neighborChoice = Random.Range(1, 2); // 50/50 chance for either tile to be chosen
                    switch (neighborChoice)
                    {
                        case 1:
                            tileIDRest = GenerateBackgroundNeighborID(backgroundTileGrid[y - 1, x], 1); // selects the tile one row below
                            backgroundTileGrid[y, x] = tileIDRest;
                            CreateBackgroundTile(tileIDRest, x, y);
                            break;
                        case 2:
                            tileIDRest = GenerateBackgroundNeighborID(backgroundTileGrid[y, x - 1], 2); // selects the tile one column to the left
                            backgroundTileGrid[y, x] = tileIDRest;
                            CreateBackgroundTile(tileIDRest, x, y);
                            break;
                    }

/*
                   if (backgroundTileGrid[y - 1, x] == 1 || backgroundTileGrid[y - 1, x] == 3 || backgroundTileGrid[y - 1, x] == 4)
                    {
                        tileIDRest = GenerateBackgroundNeighborID(backgroundTileGrid[y - 1, x]);
                        backgroundTileGrid[y, x] = tileIDRest;
                        CreateBackgroundTile(tileIDRest, x, y);
                    }
                    else if (backgroundTileGrid[y, x - 1] == 2 || backgroundTileGrid[y, x - 1] == 3 || backgroundTileGrid[y, x - 1] == 5)
                    {
                        tileIDRest = GenerateBackgroundNeighborID(backgroundTileGrid[y, x - 1]);
                        backgroundTileGrid[y, x] = tileIDRest;
                        CreateBackgroundTile(tileIDRest, x, y);
                    }
                   else
                    {
                        backgroundTileGrid[y, x] = 0;
                        CreateBackgroundTile(0, x, y);
                    }    
*/
                }
            }
        }
    }

    /*** Handles generating the map of terrain tiles ***/
    void GenerateTerrainTiles()
    {
        // *** Resource Tile Generation ***
        mapGrid = new int[mapHeight, mapWidth];

        // *** Manually setting the first tile as grass, position [0, 0] (bottom left corner of the grid) ***
        mapGrid[0, 0] = 0;
        CreateTerrainTile(0, 0, 0); // grass at [0, 0]

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
                     // either the title one row below, or one column to the left
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

    /*** Generates and returns an ID for a neighboring background tile using defined probabilities for each type of terrain tile ***/
    int GenerateBackgroundNeighborID(int tileID, int bottomOrLeft)
    {
        int randomInt = 0;
        int bgNeighborID = 0; // stores the ID for the generated neighbor

        switch (tileID)
        {
            case 0: // Grass
                randomInt = Random.Range(1, 100);

                if (randomInt >= 0 && randomInt < 90) // 90% chance of generating Grass
                {
                    bgNeighborID = 0;
                }
                else if (randomInt >= 90 && randomInt < 95) // 5% chance of generating River(Vertical)
                {
                    bgNeighborID = 1;
                }
                else if (randomInt >= 95 && randomInt < 100) // 5% chance of generating River(Horizontal)
                {
                    bgNeighborID = 2;
                }
                else
                {
                    bgNeighborID = 0;
                }
                break;

            case 1: // River(Vertical) // START OF EDITS ***
                randomInt = Random.Range(1, 100);

                if (bottomOrLeft == 1) // checks if the row below was selected
                {
                    if (randomInt >= 0 && randomInt < 20) // 20% chance of generating Grass
                    {
                        bgNeighborID = 0;
                    }
                    else if (randomInt >= 20 && randomInt < 60) // 40% chance of generating River(Vertical)
                    {
                        bgNeighborID = 1;
                    }
                    else if (randomInt >= 60 && randomInt < 100) // 40% chance of generating River(TL Corner)
                    {
                        bgNeighborID = 5;
                    }
                    else
                    {
                        bgNeighborID = 0;
                    }
                }
                if (bottomOrLeft == 2) // checks if the column to the left was selected
                {
                    bgNeighborID = 0;
                }
                break;

            case 2: // River(Horizontal)
                randomInt = Random.Range(1, 100);

                if (bottomOrLeft == 1) // checks if the row below was selected
                {
                    bgNeighborID = 0;
                }
                if (bottomOrLeft == 2) // checks if the column to the left was selected
                {
                    if (randomInt >= 0 && randomInt < 20) // 20% chance of generating Grass
                    {
                        bgNeighborID = 0;
                    }
                    else if (randomInt >= 20 && randomInt < 60) // 40% chance of generating River(Horizontal)
                    {
                        bgNeighborID = 2;
                    }
                    else if (randomInt >= 60 && randomInt < 100) // 40% chance of generating River(LR Corner)
                    {
                        bgNeighborID = 4;
                    }
                    else
                    {
                        bgNeighborID = 0;
                    }
                }
                break;

/*            case 3: // River(Lower Left Corner)
                randomInt = Random.Range(1, 100);

                if (bottomOrLeft == 1) // checks if the row below was selected
                {
                    if (randomInt >= 0 && randomInt < 20) // 20% chance of generating Grass
                    {
                        bgNeighborID = 0;
                    }
                    else if (randomInt >= 20 && randomInt < 60) // 40% chance of generating River(Vertical)
                    {
                        bgNeighborID = 1;
                    }
                    else if (randomInt >= 60 && randomInt < 100) // 40% chance of generating River(TL Corner)
                    {
                        bgNeighborID = 5;
                    }
                    else
                    {
                        bgNeighborID = 0;
                    }
                }
                else if (bottomOrLeft == 2) // checks if the column to the left was selected
                {
                    if (randomInt >= 0 && randomInt < 20) // 20% chance of generating Grass
                    {
                        bgNeighborID = 0;
                    }
                    else if (randomInt >= 20 && randomInt < 60) // 40% chance of generating River(Horizontal)
                    {
                        bgNeighborID = 2;
                    }
                    else if (randomInt >= 60 && randomInt < 100) // 40% chance of generating River(LR Corner)
                    {
                        bgNeighborID = 4;
                    }
                    else
                    {
                        bgNeighborID = 0;
                    }
                }
                break;
*/
            case 4: // River(Lower Right Corner)
                randomInt = Random.Range(1, 100);

                if (bottomOrLeft == 1) // checks if the row below was selected
                {
                    if (randomInt >= 0 && randomInt < 100) // 100% chance of generating River(Vertical)
                    {
                        bgNeighborID = 1;
                    }
                    else
                    {
                        bgNeighborID = 0;
                    }
                }
                if (bottomOrLeft == 2) // checks if the column to the left was selected
                {
                    bgNeighborID = 0;
                }
                break;

            case 5: // River(Top Left Corner)
                randomInt = Random.Range(1, 100);

                if (bottomOrLeft == 1) // checks if the row below was selected
                {
                    bgNeighborID = 0;
                }
                if (bottomOrLeft == 2) // checks if the column to the left was selected
                {
                    if (randomInt >= 0 && randomInt < 100) // 100% chance of generating River(Horizontal)
                    {
                        bgNeighborID = 2;
                    }
                    else
                    {
                        bgNeighborID = 0;
                    }
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
            case 0: // Grass
                randomInt = Random.Range(1, 100);

                if (randomInt >= 0 && randomInt < 90) // 90% chance of generating Grass
                {
                    neighborID = 0;
                }
                else if (randomInt >= 90 && randomInt < 95) // 5% chance of generating Rocks
                {
                    neighborID = 1;
                }
                else if (randomInt >= 95 && randomInt < 100) // 5% chance of generating Trees
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

                if (randomInt >= 0 && randomInt < 80) // 80% chance of generating Grass
                {
                    neighborID = 0;
                }
                else if (randomInt >= 80 && randomInt < 100) // 20% chance of generating Rocks
                {
                    neighborID = 1;
                }
                //else if (randomInt >= 99 && randomInt < 100) // 0% chance of generating Trees
                //{
                //    bgNeighborID = 2;
                //}
                else
                {
                    neighborID = 0;
                }
                break;

            case 2: // Trees
                randomInt = Random.Range(1, 100);

                if (randomInt >= 0 && randomInt < 80) // 80% chance of generating Grass
                {
                    neighborID = 0;
                }
                //else if (randomInt >= 70 && randomInt < 71) // 0% chance of generating Rocks
                //{
                //    bgNeighborID = 1;
                //}
                else if (randomInt >= 80 && randomInt < 100) // 20% chance of generating Trees
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
