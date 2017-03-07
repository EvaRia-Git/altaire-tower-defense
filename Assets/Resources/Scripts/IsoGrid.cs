using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IsoGrid : MonoBehaviour {

    // Scale of this Iso Grid
    [Serializable]
    public class IsoScale
    {
        public float xYFactor;      // The size of one isometric square in Unity Space
        public float heightFactor;  // The size of one unit of height in Unity Space
    }

    // Length and width of the grid.
    [Serializable]
    public class GridSize
    {
        // Default to one
        public int x = 1;
        public int y = 1;
        
    }

    // Old length and width of the grid.
    // Stored to keep track of indices when iterating through the refresh loops
    [Serializable]
    public class OldGridSize
    {
        // Default to one
        public int x = 1;
        public int y = 1;
    }

    // Declare fields
    public IsoScale isoScale = new IsoScale();
    public GridSize gridSize = new GridSize();
    [HideInInspector]
    public OldGridSize oldGridSize = new OldGridSize();

    // Used as a button in the editor. Simple but hacky.
    // Defaults to true for editor-only initialization.
    public bool refreshGrid = true;             

    // Contains a reference to every tile in the grid.
    [HideInInspector]
    public GameObject[] Tiles;

    void Update()
    {
        // Boolean "Button" to call Refresh.
        if(refreshGrid)
        {
            RefreshGrid(gridSize.x, gridSize.y);
        }
        refreshGrid = false;
    }

    // Adjusts the size of the grid, position of all tiles, and refreshes sprites.
    public void RefreshGrid(int X, int Y)
    {
        // First time initialization.
        if (Tiles == null)
        {
            Tiles = new GameObject[1];
        }

        // We create a temporary grid with the new size.
        // This is so we can move existing tiles to the new grid, since 2D Arrays are not expandable.
        GameObject[] newGrid = new GameObject[X*Y];

        // First loop destroys any old tiles that are outside the border of the new grid.
        for (int i = 0; i < oldGridSize.x; i++)
        {
            for (int j = 0; j < oldGridSize.y; j++)
            {
                if (i >= X || j >= Y)
                {
                    DestroyImmediate(Tiles[j * oldGridSize.x + i]);
                }
            }
        }
        // Second loop moves remaining existing tiles from the old grid to the new grid.
        for (int i = 0; i < Math.Min(X, oldGridSize.x); i++)
        {
            for (int j = 0; j < Math.Min(Y, oldGridSize.y); j++)
            {
                    newGrid[j * X + i] = Tiles[j * oldGridSize.x + i];
            }
        }

        // Third loop constructs new tiles and adjusts positioning.
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                // We put new tiles in any empty space on the new grid.
                if (newGrid[j * X + i] == null)
                {
                    newGrid[j * X + i] = new GameObject("Tile-" + i + "-" + j);
                    newGrid[j * X + i].transform.SetParent(transform);
                    newGrid[j * X + i].AddComponent<IsoTile>();
                    newGrid[j * X + i].GetComponent<IsometricObject>().isoPosition.drawDepth += i + j;

                    // Since we are controlling the position of tiles using the grid, 
                    // we hide its manual controls in the inspector.
                    newGrid[j * X + i].GetComponent<IsometricObject>().hideFlags = HideFlags.HideInInspector;
                }

                // All the tiles in the grid are set to the scale defined by the grid.
                // Then each tile is moved to its correct spot in the isometric world and its position updated.
                newGrid[j * X + i].GetComponent<IsometricObject>().isoScale.xYFactor = isoScale.xYFactor;
                newGrid[j * X + i].GetComponent<IsometricObject>().isoScale.heightFactor = isoScale.heightFactor;
                newGrid[j * X + i].GetComponent<IsometricObject>().isoPosition.isoX = i;
                newGrid[j * X + i].GetComponent<IsometricObject>().isoPosition.isoY = j;
                newGrid[j * X + i].GetComponent<IsometricObject>().UpdateRealPosition();
            }
        }

        // Now that we are done reconstructing the grid, we assign it back to the original reference.
        oldGridSize.x = X;
        oldGridSize.y = Y;
        Tiles = newGrid;
    }

}
