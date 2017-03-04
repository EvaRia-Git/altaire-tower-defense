using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IsoGrid : MonoBehaviour {

    //Scale of this Iso Grid
    [Serializable]
    public class IsoScale
    {
        public float xYFactor;      // The size of one isometric square in Unity Space
        public float heightFactor;  // The size of one unit of height in Unity Space
    }

    //Length and width of the grid.
    [Serializable]
    public class GridSize
    {
        //Default to one
        public int x = 1;
        public int y = 1;
    }

    //Declare fields
    public IsoScale isoScale = new IsoScale();
    public GridSize gridSize = new GridSize();
    public bool refreshGrid = true;             // Used as a button in the editor. Simple but hacky.
                                                // Defaults to true for editor-only initialization.

    private GameObject[,] Tiles;                // Contains a reference to every tile in the grid.

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
            Tiles = new GameObject[1, 1];
        }

        // We create a temporary grid with the new size.
        // This is so we can move existing tiles to the new grid, since 2D Arrays are not expandable.
        GameObject[,] tempGrid = new GameObject[X, Y];

        // First loop destroys any old tiles that are outside the border of the new grid.
        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                if (i >= X || j >= Y)
                {
                    DestroyImmediate(Tiles[i, j]);
                }
            }
        }

        // Second loop moves remaining existing tiles from the old grid to the new grid.
        for (int i = 0; i < Math.Min(X, Tiles.GetLength(0)); i++)
        {
            for (int j = 0; j < Math.Min(Y, Tiles.GetLength(1)); j++)
            {
                tempGrid[i, j] = Tiles[i, j];
            }
        }

        // Third loop constructs new tiles and adjusts positioning.
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                // We put new tiles in any empty space on the new grid.
                if (tempGrid[i, j] == null)
                {
                    tempGrid[i, j] = new GameObject("Tile-" + i + "-" + j);
                    tempGrid[i, j].transform.SetParent(transform);
                    tempGrid[i, j].AddComponent<IsoTile>();
                    tempGrid[i, j].GetComponent<IsometricObject>().isoPosition.drawDepth += i + j;

                    // Since we are controlling the position of tiles using the grid, 
                    // we hide its manual controls in the inspector.
                    tempGrid[i, j].GetComponent<IsometricObject>().hideFlags = HideFlags.HideInInspector;
                }

                // All the tiles in the grid are set to the scale defined by the grid.
                // Then each tile is moved to its correct spot in the isometric world and its position updated.
                tempGrid[i, j].GetComponent<IsometricObject>().isoScale.xYFactor = isoScale.xYFactor;
                tempGrid[i, j].GetComponent<IsometricObject>().isoScale.heightFactor = isoScale.heightFactor;
                tempGrid[i, j].GetComponent<IsometricObject>().isoPosition.isoX = i;
                tempGrid[i, j].GetComponent<IsometricObject>().isoPosition.isoY = j;
                tempGrid[i, j].GetComponent<IsometricObject>().UpdateRealPosition();
            }
        }

        // Now that we are done reconstructing the grid, we assign it back to the original reference.
        Tiles = tempGrid;
    }

}
